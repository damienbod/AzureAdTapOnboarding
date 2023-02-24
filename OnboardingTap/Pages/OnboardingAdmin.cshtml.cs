﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnboardingTap.Pages;

public class OnboardingAdminModel : PageModel
{
    private readonly AadGraphSdkManagedIdentityAppClient _aadGraphSdkManagedIdentityAppClient;

    public OnboardingAdminModel(AadGraphSdkManagedIdentityAppClient aadGraphSdkManagedIdentityAppClient)
    {
        _aadGraphSdkManagedIdentityAppClient = aadGraphSdkManagedIdentityAppClient;
    }

    [BindProperty]
    public UserModel? UserData { get; set; } = new UserModel();

    [BindProperty]
    public TapDataModel? Tap { get; set; } = new TapDataModel();

    public void OnGet()
    {
        UserData = new UserModel
        {
            Email = "tst4@damienbodsharepoint.onmicrosoft.com",
            UserName = "tst4",
            LastName = "last-tst4",
            FirstName = "first-tst4"
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(UserData != null)
        {
            var createdUser = await _aadGraphSdkManagedIdentityAppClient.CreateUser(
                UserData, false);

            if (createdUser!.Id != null)
            {
                var tap = await _aadGraphSdkManagedIdentityAppClient.AddTapForUserAsync(createdUser.Id);

                Tap = new TapDataModel
                {
                    Email = createdUser.Upn,
                    AccessCode = tap!.TemporaryAccessPass
                };
            }
        }

        return Page();
    }

}