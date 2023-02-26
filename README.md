# Azure AD Temporary Access Pass (TAP) Onboarding

[![.NET](https://github.com/damienbod/AzureAdTapOnboarding/actions/workflows/dotnet.yml/badge.svg)](https://github.com/damienbod/AzureAdTapOnboarding/actions/workflows/dotnet.yml)

## Test Cases

- [good] A member flow with TAP and FIDO2 auth 
- [good] A member flow with password using email/password auth
- [good] A member flow with password setup and a phone auth
- [good] A guest flow with federated login
- [good] A guest flow with Microsoft account
- [good] A guest flow with email code
- A guest flow with phone login (is this possible?)

### Open tasks

- Add documentation of the different onboarding flows
- Switch to delegated Graph permissions
- Evaluate this with a Multi-tenant App Registration using delegated flows

## Using Temporary Access Pass (TAP) with members

Note: TAP only works with members and a passwordless authentication once setup

https://aka.ms/mysecurityinfo

## Creating AAD member users

Users are created on the tenant with a known or registered domain. The member user can use a TAP to onboard or a password.

When using TAP, no password is returned and the user must register a FIDO2 key, etc.

## Creating AAD guest users

And email with a domain unknown or not registered on the tenant will be created using an invite.

TAP cannot be used for guests.

Note for live or Microsoft accounts the security info is at:

https://account.microsoft.com/security

## Graph permissions

Application

- User.EnableDisableAccount.All
- User.ReadWrite.All
- UserAuthenticationMethod.ReadWrite.All

## Setup secrets:

Add this to the user secrets for local development with the values from your Azure App registration. Use an Azure Key vault for deployments and move the certificates or Managed identities.

```
  "AzureAd": {
    "ClientSecret": "--your-secret--"
  },
  "AzureAdGraph": {
    "ClientSecret": "--your-secret--"
  }
```

## Setup app.settings

Replace the configurations with the data from your Azure App registrations.

# Links

https://entra.microsoft.com/

https://learn.microsoft.com/en-us/azure/active-directory/authentication/howto-authentication-temporary-access-pass

https://learn.microsoft.com/en-us/graph/api/authentication-post-temporaryaccesspassmethods?view=graph-rest-1.0&tabs=csharp

https://learn.microsoft.com/en-us/graph/authenticationmethods-get-started

https://learn.microsoft.com/en-us/azure/active-directory/authentication/howto-authentication-passwordless-security-key-on-premises

https://damienbod.com/2022/03/11/create-azure-b2c-users-with-microsoft-graph-and-asp-net-core/

https://damienbod.com/2022/03/24/onboarding-new-users-in-an-asp-net-core-application-using-azure-b2c/

https://damienbod.com/2022/08/02/workarounds-to-disable-azure-ad-user-using-microsoft-graph-and-an-application-scope/

https://damienbod.com/2022/07/11/invite-external-users-to-azure-ad-using-microsoft-graph-and-asp-net-core/

https://www.youtube.com/watch?v=SuBeZ9VH8dI&t=1207s

https://learn.microsoft.com/en-us/azure/active-directory/external-identities/external-identities-overview

https://learn.microsoft.com/en-us/azure/active-directory/external-identities/b2b-quickstart-add-guest-users-portal

## Used packages

Polly

https://github.com/App-vNext/Polly