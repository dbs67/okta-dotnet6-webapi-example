using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Okta_ClientFlowDotNetSix.Okta
{
    public class OktaJwtValidation : IJwtValidator
    {
        private readonly string _issuer;
        private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

        public OktaJwtValidation(IOptions<OktaJwtVerificationOptions> options)
        {
            _issuer = options.Value.Issuer;

            _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                _issuer + "/.well-known/oauth-authorization-server",
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());
        }

        public async Task<JwtSecurityToken> ValidateToken(string token, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (string.IsNullOrEmpty(_issuer))
            {
                throw new ArgumentNullException(nameof(_issuer));
            }

            var discoveryDocument = await _configurationManager.GetConfigurationAsync(ct);
            var signingKeys = discoveryDocument.SigningKeys;

            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = signingKeys,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2),
                ValidateAudience = false,
            };

            try
            {
                var principal = new JwtSecurityTokenHandler()
                    .ValidateToken(token, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
            }
            catch (Exception e)
            {
                // Logging, etc.

                return null;
            }
        }

    }
}
