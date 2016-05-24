using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Facebook;

namespace FacebookingTest
{
    public partial class Authorize 
    {
        public Authorize()
        {
            InitializeComponent();

        }

        public string ApplicationId 
        { 
            get 
            { 
                return ConfigurationManager.AppSettings["ApplicationId"]; 
            } 
        }

        public string ExtendedPermissions 
        { 
            get
            {
                return ConfigurationManager.AppSettings["ExtendedPermissions"];
            } 
        }

        public string AppSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationSecret"];
            }
        }

        public string AccessToken { get; set; }

        private void LoadAuthorize(object sender, EventArgs e)
        {
            var destinationURL = String.Format(
                @"https://www.facebook.com/dialog/oauth?client_id={0}&scope={1}&redirect_uri=http://www.facebook.com/connect/login_success.html&response_type=token",
                this.ApplicationId,
                this.ExtendedPermissions);
            webBrowser.Navigated += WebBrowserNavigated;
            webBrowser.Navigate(destinationURL);
        }

        private void WebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // get token
            var url = e.Url.Fragment;
            if (url.Contains("access_token") && url.Contains("#"))
            {
                this.Hide();
                url = (new Regex("#")).Replace(url, "?", 1);
                this.AccessToken = System.Web.HttpUtility.ParseQueryString(url).Get("access_token");
                //MessageBox.Show(facebookCore.AccessToken);
                try
                {
                    //var facebooking = new FacebookingTest(facebookCore.AccessToken);
                    //facebooking.UpdateStatus();
                    var fb = new FacebookClient(this.AccessToken);
                    dynamic result = fb.Post("me/feed", new { message = "My second wall post using Facebook C# SDK" });
                    var newPostId = result.id;
                }
                catch (Exception exception)
                {
                    Console.Write(exception);
                }
            }

        }
    }
}
