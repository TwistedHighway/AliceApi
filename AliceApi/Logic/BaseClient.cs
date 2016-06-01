using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

//http://johnnycode.com/2012/02/23/consuming-your-own-asp-net-web-api-rest-service/


namespace AliceApi.Logic
{
    public class BaseClient
    {
        protected readonly string _endpoint;

        public BaseClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public T Get<t>(int top = 0, int skip = 0)
        {
            using (var httpClient = new HttpClient())
            {
                var endpoint = _endpoint + "?";
                var parameters = new List<string>();

                if (top > 0)
                    parameters.Add(string.Concat("$top=", top));

                if (skip > 0)
                    parameters.Add(string.Concat("$skip=", skip));

                endpoint += string.Join("&", parameters);

                var response = httpClient.GetAsync(endpoint).Result;

                return JsonConvert.DeserializeObject<t>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public T Get<t>(int id)
        {
            using (var httpClient = NewHttpClient())
            {
                var response = httpClient.GetAsync(_endpoint + id).Result;

                return JsonConvert.DeserializeObject<t>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public string Post<t>(T data)
        {
            using (var httpClient = NewHttpClient())
            {
                var requestMessage = GetHttpRequestMessage<t>(data);

                var result = httpClient.PostAsync(_endpoint, requestMessage.Content).Result;

                return result.Content.ToString();
            }
        }

        public string Put<t>(int id, T data)
        {
            using (var httpClient = NewHttpClient())
            {
                var requestMessage = GetHttpRequestMessage<t>(data);

                var result = httpClient.PutAsync(_endpoint + id, requestMessage.Content).Result;

                return result.Content.ReadAsStringAsync().Result;
            }
        }

        public string Delete(int id)
        {
            using (var httpClient = NewHttpClient())
            {
                var result = httpClient.DeleteAsync(_endpoint + id).Result;

                return result.Content.ToString();
            }
        }

        protected HttpRequestMessage GetHttpRequestMessage<t>(T data)
        {
            var mediaType = new MediaTypeHeaderValue("application/json");
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());

            var jsonFormatter = new JsonNetFormatter(jsonSerializerSettings);

            var requestMessage = new HttpRequestMessage<t>(data, mediaType, new MediaTypeFormatter[] { jsonFormatter });

            return requestMessage;
        }

        protected HttpClient NewHttpClient()
        {
            return new HttpClient();
        }
    }
}