using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web;
//using Owin.Security.Providers.Untappd;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AliceApi.Logic.Movies
{
    //http://www.programmableweb.com/category/movies/apis?page=3&category=20114
    //http://www.programmableweb.com/api/internet-video-archive
    //http://www.internetvideoarchive.com/documentation/getting-started/

    //http://www.omdbapi.com/

    public class InternetVideoArchive
    {
        public class DataObject
        {
            public string Value { get; set; }
        }


        public class ChuckNorrisFact
        {
            public string Joke { get; set; }
            public string Id { get; set; }
            public string Categories { get; set; }
        }
        


        private const string URL = "http://api.icndb.com/jokes/latest";
        private string urlParameters = "?api_key=123";

        //(from v in VideoAssets.expand("EntertainmentProgram, EntertainmentProgram/AlternateIds/AlternateIdType,VideoAssetScreenCapture, CountryTarget,LanguageSpoken, LanguageSubtitled, MediaType,Encodes, RegionRestrictions, Descriptions") _
        //where(v.ExpirationDate is nothing or v.ExpirationDate > datetime.now) and v.RequiresIVAPlayer = false and v.OkToEncodeAndServe = true and _
        //v.WarningFlag = false and v.AllowAds = true and ((v.LanguageSpokenId = 0 and v.LanguageSubtitledId = -1) or _
        //(v.LanguageSubtitledId = 0 and v.LanguageSpokenId > 0))  _
        //select v).skip(0).take(500)
        
        //http://api.internetvideoarchive.com/2.0/DataService/VideoAssets()?$filter=cast((cast(ExpirationDate eq null,'Edm.Boolean') or ExpirationDate gt datetime'2015-06-18T21:18:04.6407898-04:00') and cast(RequiresIvaPlayer eq false,'Edm.Boolean') and cast(OkToEncodeAndServe eq true,'Edm.Boolean') and cast(WarningFlag eq false,'Edm.Boolean') and cast(AllowAds eq true,'Edm.Boolean') and cast(cast(LanguageSpokenId,'Edm.Int32') eq 0 and cast(LanguageSubtitledId,'Edm.Int32') eq -1 or cast(LanguageSubtitledId,'Edm.Int32') eq 0 and cast(LanguageSpokenId,'Edm.Int32') gt 0,'Edm.Boolean'),'Edm.Boolean')&$skip=0&$top=500&$expand=EntertainmentProgram, EntertainmentProgram/AlternateIds/AlternateIdType,VideoAssetScreenCapture, CountryTarget,LanguageSpoken, LanguageSubtitled, MediaType,Encodes, RegionRestrictions, Descriptions
        //&developerid=dd9a9172-4fbd-469c-bc92-a0955e9bdd6b&format=json
        
        //http://api.internetvideoarchive.com/2.0/DataService

        //http://api.internetvideoarchive.com/2.0/DataService/EntertainmentPrograms()
        //?$filter=Publishedid gt 0
        //&format=json
        //&developerid=2019c857-85a2-4655-abe4-b1487d0ef4a4
        //http://api.internetvideoarchive.com/2.0/DataService/ReleaseEvents()?$top=1&format=json&$callback=cb&developerid=2019c857-85a2-4655-abe4-b1487d0ef4a4
        //Access Keys
        //•	Entertainment Express AppID: 0726801ca881 
        //•	DeveloperId for oDATA API : 2019c857-85a2-4655-abe4-b1487d0ef4a4
        public async Task<string> FetchData()
        {
            //var remoteUrl = @"http://api.internetvideoarchive.com/2.0/DataService/ReleaseEvents()?$top=1&format=json&$callback=cb&developerid=2019c857-85a2-4655-abe4-b1487d0ef4a4";
            //var remoteUrl0 = @"https://ee.internetvideoarchive.net/api/expressstandard/actions/search?appid=0726801ca881&term='13th Warrior'";
            //var remoteUrl1 = @"https://api.icndb.com";

            var appId = "0726801ca881";
            var searchTerm = "Star Wars";
            var remoteUrl = @"https://ee.internetvideoarchive.net/api/expressstandard/";//?appid=&term";

            var chuckNorris = @"http://api.icndb.com/jokes/";




            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(remoteUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
                HttpResponseMessage response = await client.GetAsync("actions/search?appid=" + appId + "&term=" + searchTerm);
                //resp.EnsureSuccessStatusCode();    // Throw if not a success code.
                if (response.IsSuccessStatusCode)
                {
                    var foo = response;

                    var cont = response.Content;
                    var headr = response.Headers;
                    var succes = response.IsSuccessStatusCode;
                    var rephraz = response.ReasonPhrase;
                    var reqmsg = response.RequestMessage;
                    var statcod = response.StatusCode;
                    var vver = response.Version;
                    var s = response.EnsureSuccessStatusCode();

                    //ChuckNorrisFact product = await response.Content.ReadAsAsync > ChuckNorrisFact > ();
                    //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);


                    var formatters = new List<MediaTypeFormatter>() {
                        //new MyCustomFormatter(),
                        new JsonMediaTypeFormatter(),
                        new XmlMediaTypeFormatter()};


                    var sr = response.Content.ReadAsStringAsync();

                    //response.Content.ReadAsAsync<IEnumerable<ChuckNorrisFact>>(formatters);


                }

            }
                

            //// Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            //// List data response.
            //HttpResponseMessage response = client.GetAsync(URL).Result;  // Blocking call!
            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse the response body. Blocking!
            //    //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
            //    //foreach (var d in dataObjects)
            //    //{
            //    //    Console.WriteLine("{0}", d.Value);
            //    //}
            //}
            //else
            //{
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}
        

        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(chuckNorris);
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    //request.ContentLength = 

        //    WebResponse response = request.GetResponse();



        //    using (HttpClient client = new HttpClient())
        //    {


        //        //var response = client.GetAsync(new Uri(remoteUrl1));
        //        //using (var responseStream = response.Content.ReadAsStreamAsync())
        //        //{
        //        //    var OrdersList = new DataContractJsonSerializer(typeof(List<object>));
        //        //    //Orders = new ObservableCollection<Order>((IEnumerable<Order>)OrdersList.ReadObject(responseStream));

        //        //    var foo = ":";

        //        //}
        //    }

            return "";
        }



        //public class Class1
        //{
        //    private const string URL = "http://api.icndb.com/jokes/latest";
        //    private string urlParameters = "?api_key=123";

        //    static void Main(string[] args)
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri(URL);

        //        // Add an Accept header for JSON format.
        //        client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //        // List data response.
        //        HttpResponseMessage response = client.GetAsync(URL).Result;  // Blocking call!
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Parse the response body. Blocking!
        //            var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
        //            foreach (var d in dataObjects)
        //            {
        //                Console.WriteLine("{0}", d.Name);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        //        }
        //    }
        //}


        //static void Main()
        //{
        //    RunAsync().Wait();
        //}

        //static async Task RunAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        // TODO - Send HTTP requests
        //    }
        //}

    }
}
