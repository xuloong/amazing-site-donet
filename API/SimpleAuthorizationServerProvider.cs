using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using BLL;

namespace API
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Factory.StartNew(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Factory.StartNew(() => context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }));
            
            UserBLL userBLL = new UserBLL();
            UserDto userDto = userBLL.login(context.UserName, context.Password);
            if (userDto == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return; 
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, userDto.Id.ToString()));
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            context.Validated(ticket);

            await base.GrantResourceOwnerCredentials(context);

        }
    }
}