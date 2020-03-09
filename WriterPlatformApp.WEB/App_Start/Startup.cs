using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using WriterPlatformApp.BLL.Implementatiton;

[assembly: OwinStartup(typeof(WriterPlatformApp.WEB.App_Start.Startup))]

namespace WriterPlatformApp.WEB.App_Start
{
    public class Startup
    {
        public readonly int minutesForLogout = 5;
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserBOImpl>(NinjectConfig.GetUserBO);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = TimeSpan.FromMinutes(minutesForLogout),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

        }
    }
}