using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using AliceApi.ViewModels;
using WebMatrix.WebData;

namespace AliceApi
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // Active Directory
            //http://stackoverflow.com/questions/4342271/asp-net-mvc-forms-authorization-with-active-directory-groups

            // Repository pattern and Unit of work
            //http://www.codeproject.com/Articles/990492/RESTful-Day-sharp-Enterprise-Level-Application

            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            //https://msdn.microsoft.com/en-us/library/hh243647.aspx
            //client_id=CLIENT_ID&scope=SCOPES&response_type=code&redirect_uri=REDIRECT_URI

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //// Twitter Access Token:  -
            //// Access Token Secret  
            //// Owner  
            //// Owner ID  

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");


            //OAuthWebSecurity.RegisterGoogleClient();

            //Owin.IAppBuilder app;
            //app.New().Properties.Add()
            //Owin.GoogleAuthenticationExtensions.UseGoogleAuthentication()

            //AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new


            //OAuthWebSecurity.RegisterLinkedInClient("key", "secret", "Name");
            //OAuthWebSecurity.RegisterYahooClient();

            // Project Number: 
            // Client Id: -.apps..com
            // Project Id: aliceapi-
            //{"web":{
            //    "client_id":"-.am",
            //    "auth_uri":"https://accounts.google.com/o/oauth2/auth",
            //    "token_uri":"https://accounts.google.com/o/oauth2/token",
            //    "":"https://www.googleapis.com/oauth2/v1/certs",
            //    "client_secret":"-"
            //}}
        }
    }
}
