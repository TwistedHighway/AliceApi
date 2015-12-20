using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using AliceApi.ViewModels;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;

namespace AliceApi
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

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
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: "x",
                clientSecret: "x-x-x");
            //https://account.live.com/developers/applications/index
            

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "x",
                ConsumerSecret = "x",
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
            //   consumerKey: "cOprXij5qbJzVYWrN9fiWwBnh",
            //   consumerSecret: "6Co9A3IwRD4VI87qz7jzQkmAXBbpNZYajE0RnET5p8qucVWhiE");
            // Access Token: 155943454-XcqLd8FTup2gQSjxQgF3YVqh7K8XkhrGJMhYRt13
            // Access token secret: Cc8LT13svFYgXb830uUcGhH0TfjAOKj5oYqeBPNj2rmXo

            
            //app.MapSignalR();

            //app.UseWebApi()

            app.UseFacebookAuthentication(
               appId: "x",
               appSecret: "x");
            
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "x-x.apps.googleusercontent.com",
                ClientSecret = "x-x"
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


//OAuthWebSecurity.RegisterMicrosoftClient(
//    clientId: "0000000048087436",
//    clientSecret: "mNWf6KsPMdG5UCA9nbZQzsIae11aS118");

//OAuthWebSecurity.RegisterTwitterClient(
//    consumerKey: "cOprXij5qbJzVYWrN9fiWwBnh",
//    consumerSecret: "6Co9A3IwRD4VI87qz7jzQkmAXBbpNZYajE0RnET5p8qucVWhiE");

//// Twitter Access Token:  155943454-XcqLd8FTup2gQSjxQgF3YVqh7K8XkhrGJMhYRt13
//// Access Token Secret Cc8LT13svFYgXb830uUcGhH0TfjAOKj5oYqeBPNj2rmXo 
//// Owner Member2199 
//// Owner ID 155943454 

//OAuthWebSecurity.RegisterFacebookClient(
//    appId: "1147876445241589",
//    appSecret: "616ba90a6b74798f33b74ea1904451ed");

//OAuthWebSecurity.RegisterGoogleClient(); // google caused the whole upgrade but for good reason.  
// Below could be used in an AJAX call. AnguayrJS or straight up jQ.  
// Project Number: 650993449785
// Client Id: 650993449785-8f3jtqmjeigs91vlpkmdn6io0db338n2.apps.googleusercontent.com
// Project Id: aliceapi-1138
//{"web":{
//    "client_id":"650993449785-8f3jtqmjeigs91vlpkmdn6io0db338n2.apps.googleusercontent.com",
//    "auth_uri":"https://accounts.google.com/o/oauth2/auth",
//    "token_uri":"https://accounts.google.com/o/oauth2/token",
//    "auth_provider_x509_cert_url":"https://www.googleapis.com/oauth2/v1/certs",
//    "client_secret":"9paKxlvWAF4-6O491bNEBU54"

//OAuthWebSecurity.RegisterLinkedInClient("key", "secret", "Name");
//OAuthWebSecurity.RegisterYahooClient();