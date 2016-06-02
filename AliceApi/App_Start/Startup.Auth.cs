using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using AliceApi.Models;
using AliceApi.Providers;
using AliceApi.Repository;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Twitter;
//using WebMatrix.WebData;

namespace AliceApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Api keys
            var uow = new UnitOfWork();
            var appConfig = uow.AppConfigRepository.Get().ToList();
            var fb = appConfig.FirstOrDefault(w => w.ApiClient == "Facebook");
            var ms = appConfig.FirstOrDefault(w => w.ApiClient == "Microsoft");
            var go = appConfig.FirstOrDefault(w => w.ApiClient == "Google");
            var tw = appConfig.FirstOrDefault(w => w.ApiClient == "Twitter");


            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);


            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);



            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                    //    validateInterval: TimeSpan.FromMinutes(30),
                    //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);


            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: ms.ApiClientId, 
                clientSecret: ms.ApiClientSecret);
            //https://account.live.com/developers/applications/index
            

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = tw.ApiClientId, 
                ConsumerSecret = tw.ApiClientSecret, 
                //BackchannelCertificateValidator = null
                BackchannelCertificateValidator = new Microsoft.Owin.Security.CertificateSubjectKeyIdentifierValidator(new[]
                    {
                        "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                        "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                        "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                        "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                        "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                        "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                        "B13EC36903F8BF4701D498261A0802EF63642BC3", // DigiCert High Assurance EV Root CA
                        "add53f6680fe66e383cbac3e60922e3b4c412bed" // Symantec Class 3 EV SSL CA - G3
                    })
            });

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            // Access Token: 
            // Access token secret: 
            
            app.MapSignalR();

            //app.UseWebApi() //?

            app.UseFacebookAuthentication(
                appId: fb.ApiClientId, 
                appSecret: fb.ApiClientSecret); 
            
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = go.ApiClientId,
                ClientSecret = go.ApiClientSecret
            });


            // Custom implementation example
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ExternalCookie, //  == "ExternalCookie",
            //    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
            //});

            //app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);
            //app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions()
            //{
            //    ClientId = "...", // per your setup through http://go.microsoft.com/fwlink/?LinkID=144070.
            //    ClientSecret = "...",
            //    CallbackPath = new PathString("/signin-microsoft"), // default
            //    Provider = new MicrosoftAccountAuthenticationProvider()
            //    {
            //        OnAuthenticated = (ctx) =>
            //        Task.Run(() =>
            //        {
            //            ctx.OwinContext.Environment["server.User"] = new ClaimsPrincipal(ctx.Identity);
            //        })
            //    }
            //});
            //app.Run(context =>
            //{
            //    if (!ClaimsPrincipal.Current.Identity.IsAuthenticated)
            //    {
            //        context.Authentication.Challenge(new AuthenticationProperties
            //        {
            //            //RedirectUri = "http://owinsamples.com:51723/" // seems to be ignored
            //        }, "Microsoft");
            //        context.Set<int>("owin.ResponseStatusCode", 401);
            //        return context.Response.WriteAsync("Redirecting...");
            //    }
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("Hello " + ClaimsPrincipal.Current.Identity.Name + " from my OWIN App: " + DateTime.Now);
            //});

        }

    }
}
