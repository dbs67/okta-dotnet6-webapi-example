using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Okta_ClientFlowDotNetSix.Okta
{
    public interface IJwtValidator
    {
        Task<JwtSecurityToken> ValidateToken(string token, CancellationToken ct = default(CancellationToken));
    }
}
