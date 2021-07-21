using IBoxs.Data.Context;
using IBoxs.Util;
using Microsoft.Owin.Security.OAuth;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IBoxs.Web
{
    public class ProviderDeTokensDeAcesso: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (Context c = new Context())
            {
                var fezLogin = await c.Lojas.AnyAsync(user => user.CNPJCPF == context.UserName.RemoverMascaras() && user.Senha == context.Password);
                if (fezLogin)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", "user"));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("acesso inválido", "As credenciais do usuário não conferem....");
                    return;
                }
            }
        }
    }
}