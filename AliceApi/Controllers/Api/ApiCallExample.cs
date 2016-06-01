using System;
using System.IO;
using System.Net;
using System.Text;

namespace AliceApi.Controllers.Api
{
    class Program
    {

        static void Main(string[] args)
        {
            /*
             * Set baseUrl, responderUrl, and responderParameters properties
             */

            string baseUrl = "http://app32mlm1/SG757108NOKSQL_1_1";
            string responderUrl = "/~api/search/room?action=GET";
            string responderParameters = "fields=RowNumber%2CId%2CRoomName%2CRoomDescription%2CRoomNumber%2CRoomTypeName%2CBuildingCode%2CBuildingName%2CCampusName%2CCapacity%2CBuildingRoomNumberRoomName%2CCanEdit%2CCanDelete&sortOrder=%2BBuildingRoomNumberRoomName";

            /*
             * Create CookieContainer to contain session cookie
             */
            var cookieJar = new CookieContainer();
            /*
             * Open HttpWeb Request. Pass credentials to Logon.ashx page
             */
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + "/Logon.ashx");
            request.CookieContainer = cookieJar;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes("{'username': 'sysadmin', 'password':'apple'}");
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            /*
             * Read HttpWeb Response
             */
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string Response = reader.ReadToEnd();
            response.Close();


            HttpWebRequest ApiResponder = (HttpWebRequest)WebRequest.Create(baseUrl + responderUrl);
            ApiResponder.CookieContainer = cookieJar;
            ApiResponder.ContentType = "application/x-www-form-urlencoded";
            ApiResponder.Method = "POST";
            byte[] postBody = Encoding.UTF8.GetBytes(responderParameters);
            ApiResponder.ContentLength = postBody.Length;
            Stream postStream = ApiResponder.GetRequestStream();
            postStream.Write(postBody, 0, postBody.Length);
            postStream.Close();

            HttpWebResponse ApiResponse = (HttpWebResponse)ApiResponder.GetResponse();
            Stream receiveStream = ApiResponse.GetResponseStream();
            StreamReader reader2 = new StreamReader(receiveStream, Encoding.UTF8);
            string content = reader2.ReadToEnd();

            Console.WriteLine(content);

            Console.Read();
        }
    }
}