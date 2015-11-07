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

            //http://www.codeproject.com/Articles/990492/RESTful-Day-sharp-Enterprise-Level-Application

            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "cOprXij5qbJzVYWrN9fiWwBnh",
                consumerSecret: "6Co9A3IwRD4VI87qz7jzQkmAXBbpNZYajE0RnET5p8qucVWhiE");

            // Twitter Access Token:  155943454-XcqLd8FTup2gQSjxQgF3YVqh7K8XkhrGJMhYRt13
            // Access Token Secret Cc8LT13svFYgXb830uUcGhH0TfjAOKj5oYqeBPNj2rmXo 
            // Owner Member2199 
            // Owner ID 155943454 

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "1147876445241589",
                appSecret: "616ba90a6b74798f33b74ea1904451ed");

             OAuthWebSecurity.RegisterGoogleClient();
            //650993449785-8f3jtqmjeigs91vlpkmdn6io0db338n2.apps.googleusercontent.com
            // aliceapi-1138
        }
    }
}
