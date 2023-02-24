﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnboardingTap.Pages;

public class OnboardingAdminModel : PageModel
{
    private readonly AadGraphSdkManagedIdentityAppClient _aadGraphSdkManagedIdentityAppClient;
    private readonly string _aadIssuerDomain = "damienbodsharepoint.onmicrosoft.com";

    public OnboardingAdminModel(AadGraphSdkManagedIdentityAppClient aadGraphSdkManagedIdentityAppClient,
        IConfiguration configuration)
    {
        _aadGraphSdkManagedIdentityAppClient = aadGraphSdkManagedIdentityAppClient;
        var aadDomain = configuration.GetValue<string>("AadIssuerDomain");
        if (aadDomain != null)
        {
            _aadIssuerDomain = aadDomain;
        }
    }

    [BindProperty]
    public UserModel? UserData { get; set; } = new UserModel();

    [BindProperty]
    public CreatedAccessModel? AccessInfo { get; set; } = new CreatedAccessModel();

    public void OnGet()
    {
        UserData = new UserModel
        {
            Email = $"tst5@{_aadIssuerDomain}",
            UserName = "tst5",
            LastName = "last-tst5",
            FirstName = "first-tst5"
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (UserData == null) return Page();
        
        var createdUser = await _aadGraphSdkManagedIdentityAppClient.CreateGraphUserAsync(UserData);

        // member user, can use a TAP
        if (UserData.Email.ToLower().EndsWith(_aadIssuerDomain.ToLower()) && createdUser!.Id != null)
        {
            // Graph needs a pause here...
            Thread.Sleep(5000);

            var tap = await _aadGraphSdkManagedIdentityAppClient.AddTapForUserAsync(createdUser.Id);

            AccessInfo = new CreatedAccessModel
            {
                Email = createdUser.Email,
                TemporaryAccessPass = tap!.TemporaryAccessPass
            };
        }
        else if (createdUser!.Id != null) // guest user, stuck with passwords
        {
            AccessInfo = new CreatedAccessModel
            {
                Email = createdUser.Email,
                Password = createdUser.Password
            };
        }

        return Page();
    }
}
