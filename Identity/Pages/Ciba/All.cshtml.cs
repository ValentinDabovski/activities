// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages.Ciba;

[SecurityHeaders]
[Authorize]
public class AllModel : PageModel
{
    private readonly IBackchannelAuthenticationInteractionService backchannelAuthenticationInteraction;

    public AllModel(IBackchannelAuthenticationInteractionService backchannelAuthenticationInteractionService)
    {
        backchannelAuthenticationInteraction = backchannelAuthenticationInteractionService;
    }

    public IEnumerable<BackchannelUserLoginRequest> Logins { get; set; }

    [BindProperty] [Required] public string Id { get; set; }
    [BindProperty] [Required] public string Button { get; set; }

    public async Task OnGet()
    {
        Logins = await backchannelAuthenticationInteraction.GetPendingLoginRequestsForCurrentUserAsync();
    }
}