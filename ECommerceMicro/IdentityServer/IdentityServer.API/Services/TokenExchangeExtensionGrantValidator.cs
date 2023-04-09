using IdentityServer4.Validation;

namespace IdentityServer.API.Services;
public class TokenExchangeExtensionGrantValidator : IExtensionGrantValidator
{
    public string GrantType => "urn:ietf:params:oauth:grant-type:token-exchange";
    private readonly ITokenValidator _tokenValidator;
    public TokenExchangeExtensionGrantValidator(ITokenValidator tokenValidator)
    {
        this._tokenValidator = tokenValidator;
    }

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var requestRaw = context.Request.Raw.ToString();

        var token = context.Request.Raw.Get("subject_token");

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidRequest, "Token is missing");
            return;
        }

        var tokenValidateResult = await _tokenValidator.ValidateAccessTokenAsync(token);

        if (tokenValidateResult.IsError)
        {
            context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "Token is invalid");

            return;
        }

        var subjectClaim = tokenValidateResult.Claims.FirstOrDefault(c => c.Type == "sub");

        if (subjectClaim == null)
        {
            context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "Token must be contain sub value");

            return;
        }
    }
}
