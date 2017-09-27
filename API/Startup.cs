using System;
using System.Web.Http;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using WebApiContrib.Formatting.Jsonp;

[assembly: OwinStartup(typeof(API.Startup))]
namespace API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //ConfigureOAuth(app);

            //WebApi默认xml转json
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            //添加对jsonP格式的支持
            config.AddJsonpFormatter();

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/users/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}