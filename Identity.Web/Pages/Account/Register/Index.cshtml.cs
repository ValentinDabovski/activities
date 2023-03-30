using Identity.Web.Events;
using Identity.Web.Infrastructure.EventDispatcher;
using Identity.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Web.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IEventSink eventSink;


    public Index(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEventSink eventSink)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.eventSink = eventSink;
    }

    [BindProperty] public InputModel Input { get; set; }

    public IActionResult OnGet(string returnUrl)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
            var result = await userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                
                var userRegisteredEvent = new UserRegistered(new Guid(user.Id), user.Email, DateTime.UtcNow);
                
                this.eventSink.Send(userRegisteredEvent);

                return Redirect(Input.ReturnUrl ?? "/");
            }

            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}