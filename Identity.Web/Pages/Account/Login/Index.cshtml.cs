using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Identity.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Web.Pages.Account.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly IEventService events;
    private readonly IIdentityProviderStore identityProviderStore;
    private readonly IIdentityServerInteractionService interaction;
    private readonly IAuthenticationSchemeProvider schemeProvider;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public Index(
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IIdentityProviderStore identityProviderStore,
        IEventService events,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.interaction = interaction;
        this.schemeProvider = schemeProvider;
        this.identityProviderStore = identityProviderStore;
        this.events = events;
    }

    public ViewModel View { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        await BuildModelAsync(returnUrl);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        var context = await interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        // the user clicked the "cancel" button
        if (Input.Button != "login")
        {
            if (context != null)
            {
                // if the user cancels, send a result back into IdentityServer as if they 
                // denied the consent (even if this client does not require consent).
                // this will send back an access denied OIDC error response to the client.
                await interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                if (context.IsNativeClient())
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage(Input.ReturnUrl);

                return Redirect(Input.ReturnUrl);
            }

            // since we don't have a valid context, then we just go back to the home page
            return Redirect("~/");
        }

        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberLogin,
                true);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(Input.Username);
                await events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName,
                    clientId: context?.Client.ClientId));

                if (context != null)
                {
                    if (context.IsNativeClient())
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage(Input.ReturnUrl);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(Input.ReturnUrl);
                }

                // request for a local page
                if (Url.IsLocalUrl(Input.ReturnUrl))
                    return Redirect(Input.ReturnUrl);
                if (string.IsNullOrEmpty(Input.ReturnUrl))
                    return Redirect("~/");
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            await events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials",
                clientId: context?.Client.ClientId));
            ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
        }

        // something went wrong, show form with error
        await BuildModelAsync(Input.ReturnUrl);
        return Page();
    }

    private async Task BuildModelAsync(string returnUrl)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl
        };

        var context = await interaction.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP != null && await schemeProvider.GetSchemeAsync(context.IdP) != null)
        {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

            // this is meant to short circuit the UI and only trigger the one external IdP
            View = new ViewModel
            {
                EnableLocalLogin = local
            };

            Input.Username = context?.LoginHint;


            return;
        }

        var schemes = await schemeProvider.GetAllSchemesAsync();

        var allowLocal = true;
        var client = context?.Client;
        if (client != null)
        {
            allowLocal = client.EnableLocalLogin;
        }

        View = new ViewModel
        {
            AllowRememberLogin = LoginOptions.AllowRememberLogin,
            EnableLocalLogin = allowLocal && LoginOptions.AllowLocalLogin
        };
    }
}