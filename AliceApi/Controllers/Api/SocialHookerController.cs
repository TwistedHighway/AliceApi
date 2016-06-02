using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using AliceApi.ViewModels.Movie;
using DotNetOpenAuth.AspNet.Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Facebook;
using Microsoft.AspNet.Facebook.Client;

namespace AliceApi.Controllers.Api
{
    [RoutePrefix("api/SocialHooker")]
    public class SocialHookerController : ApiController
    {
        private const string AppId = "0726801ca881";

        //private string _remoteUrl = @"https://ee.internetvideoarchive.net/api/expressstandard/";//?appid=&term";
        private string _remoteUrl = @"http://connect.facebook.net/en_US/sdk.js";

        private string _chuckNorris = @"http://api.icndb.com/jokes/";

        private string _searchTerm = "Star Wars";

        private const string newAppId = "1f6bacb56ac3d49c742d9f8d58df3d74";
        private string newMovieDvdUrl = @"https://api.themoviedb.org/3/movie/550?api_key=";
        //https://www.themoviedb.org/documentation/api/discover

        // FANDANGO
        //http://api.fandango.com/<version>?op=<operation>&<parameter list>&apikey=<apikey>&sig=<sig>
        private const string FandangoUrl = "http://api.fandango.com/v1/";
        private const string FandangoApiKey = "";
        private const string FandangoSig = "";
        //private string utcStamp = DateTime.UtcNow;
        private string fandagoUrlParams = FandangoUrl + "?op=x&<params>&apikey=" + FandangoApiKey + "&sig=" + FandangoSig;

        public SocialHookerController()
        {
           
        }


        public HttpResponseMessage Get()
        {

            var fbp = new Microsoft.Web.Helpers.Facebook.UserProfile();
            var nam = fbp.Name;
            var foo1 = fbp.Bio;
            var fo2 = fbp.Email;
            var fo3 = fbp.First_Name;
            var fo4 = fbp.Gender;
            var fo5 = fbp.Id;
            var fo6 = fbp.Last_Name;
            var fo7 = fbp.Link;
            var fo8 = fbp.Timezone;
            var fo9 = fbp.Updated_Time;
            
            var f = new Facebook.FacebookClient();
            f.AppId = "";
            f.AccessToken = "";
            f.AppSecret = "";
            var friends = f.GetCurrentUserFriendsAsync();
            //f.GetCurrentUserPhotosAsync<t>();

            var t = new TwitterClient("key", "sec");
            var g = new GoogleOpenIdClient();
            

            var movie = new MovieViewModel.Movie();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_remoteUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ////http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
                //HttpResponseMessage response = await client.GetAsync("actions/search?appid=" + AppId + "&term=" + _searchTerm);
                ////resp.EnsureSuccessStatusCode();    // Throw if not a success code.
                //if (response.IsSuccessStatusCode)
                //{
                //    var foo = response;

                //    var cont = response.Content;
                //    var headr = response.Headers;
                //    var succes = response.IsSuccessStatusCode;
                //    var rephraz = response.ReasonPhrase;
                //    var reqmsg = response.RequestMessage;
                //    var statcod = response.StatusCode;
                //    var vver = response.Version;
                //    var s = response.EnsureSuccessStatusCode();

                    return new HttpResponseMessage() {};
            }
        }



        // GET api/values
        public IEnumerable<string> Get(string id)
        {


            return new string[] { id, "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public async Task<IHttpActionResult> GetExcited()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var result = null;// await someTask;

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            return Ok();
        }

        //// POST api/Account/ChangePassword
        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //        model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}


        //// POST api/Account/Logout
        //[Route("Logout")]
        //public IHttpActionResult Logout()
        //{
        //    Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //    return Ok();
        //}


        //[Route("getSome")]
        //// GET: api/MovieApi
        //public async Task<object> GetSome()
        //{
        //    var movie = new MovieViewModel.Movie();


        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(_remoteUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
        //        HttpResponseMessage response = await client.GetAsync("actions/search?appid=" + AppId + "&term=" + _searchTerm);
        //        //resp.EnsureSuccessStatusCode();    // Throw if not a success code.
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var foo = response;

        //            var cont = response.Content;
        //            var headr = response.Headers;
        //            var succes = response.IsSuccessStatusCode;
        //            var rephraz = response.ReasonPhrase;
        //            var reqmsg = response.RequestMessage;
        //            var statcod = response.StatusCode;
        //            var vver = response.Version;
        //            var s = response.EnsureSuccessStatusCode();

        //            //ChuckNorrisFact product = await response.Content.ReadAsAsync > ChuckNorrisFact > ();
        //            //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);


        //            //var formatters = new List<MediaTypeFormatter>() {
        //            //    //new MyCustomFormatter(),
        //            //    new JsonMediaTypeFormatter(),
        //            //    new XmlMediaTypeFormatter()};

        //            try
        //            {
        //                var mov = "";
        //                //var des = Newtonsoft.Json.JsonConvert.DeserializeObject(response, new JsonSerializerSettings().Context);
        //                //JObject obj = new JObject();
        //                //return des.data.Count.ToString();
        //                var sr = response.Content.ReadAsStringAsync();
        //                //var oMycustomclassname = JsonConvert.DeserializeObject<dynamic>(sr.Result);
        //                dynamic jobj = JsonConvert.DeserializeObject(sr.Result);
        //                JArray obj = (JArray) JsonConvert.DeserializeObject(sr.Result);
        //                foreach (var j in jobj)
        //                {
        //                    movie.MovieTitle = j["Title"];
        //                    var actors = j["Performers"];
        //                    var relYear = j["FirstReleasedYear"];
        //                    var normalTitle = j["NormalizedTitles"];
        //                    var assets = j["Assets"];
        //                    var assetss = j["Assets"][0]["Images"][0]["URL"];
        //                }

        //                //var demo = new InternetVideoArchiveViewModel();
        //                //JsonConvert.PopulateObject(sr.Result, demo);

        //                //InternetVideoArchiveViewModel.RootObject oMycustomclassname = JsonConvert.DeserializeObject<InternetVideoArchiveViewModel.RootObject>(sr.Result);
        //                //var doesitwork = oMycustomclassname;


        //            }
        //            catch (Exception ex)
        //            {
        //                var msg = ex.Message;
        //            }

        //            //Mycustomclassname oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<Mycustomclassname>(Json Object);
        //            //var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(Json Object);
        //            //User user = JsonConvert.DeserializeObject<User>(jsonString);

        //        }
        //        return response;

        //    }
        //    //return null;
        //}

        //public HttpResponseMessage GetFacts()
        //{
        //    //http://codeforcoffee.org/asp-net-mvc-intro-to-mvc-using-binding-json-objects-to-models/
        //}

    }
}
