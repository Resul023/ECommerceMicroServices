﻿using IdentityModel;
using IdentityServer.API.Model;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.API.Services;
public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var existUser = await _userManager.FindByEmailAsync(context.UserName);

        if (existUser == null)
        {
            var errors = new Dictionary<string, object>();
            errors.Add("errors", new List<string> { "Email or password is not true" });
            context.Result.CustomResponse = errors;

            return;
        }
        var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);

        if (passwordCheck == false)
        {
            var errors = new Dictionary<string, object>();
            errors.Add("errors", new List<string> { "Email or password is not true" });
            context.Result.CustomResponse = errors;

            return;
        }

        context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
    }
}
