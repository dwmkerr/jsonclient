using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace JsonClient
{
    public class JsonClient
    {
        public static string ToJson(object @object)
        {
            return Json.Encode(@object);
        }

        public static dynamic FromJson(string json)
        {
            return Json.Decode(json);
        }

        public static JsonResult Post(string url, object data)
        {
            var content = Json.Encode(data);
            var request = CreateWebRequest(url, "POST", content);

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                return ProcessResponse(response);
            }
        }

        public static void Put(string url, object daat)
        {

        }

        public static void Delete(string url, object data)
        {

        }

        private static JsonResult ProcessResponse(HttpWebResponse response)
        {
            //  sensible in all cases?
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }

            //  Do we have response content?
            string json = null;
            if (response.ContentLength > 0)
            {
                //  Read get the response stream.  
                using (var responseStream = response.GetResponseStream())
                {
                    //  todo is the valid here?
                    if(responseStream == null)
                        throw new InvalidOperationException("Invalid response stream.");
                    using (var reader = new StreamReader(responseStream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }

            //  Create a result object.
            return new JsonResult(response, json);
        }

        public static JsonResult Get(string url)
        {
            var request = CreateWebRequest(url, "GET");

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                return ProcessResponse(response);
            }
        }

        public static async Task<JsonResult> GetAsync(string url)
        {
            var request = CreateWebRequest(url, "GET");

            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {
                return ProcessResponse(response);
            }
        }


        private static HttpWebRequest CreateWebRequest(string url, string verb, string content = null)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);

            request.Method = verb;
            request.ContentLength = 0;
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(content))
            {
                request.ContentLength = content.Length;
                using (var stream = request.GetRequestStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(content);
                    }
                }
            }

            return request;
        }
    }

    public class JsonResult
    {
        internal JsonResult(HttpWebResponse response, string json)
        {
            Response = response;
            Json = json;
            lazyDynamic = new Lazy<dynamic>(() => System.Web.Helpers.Json.Decode(Json));
        }

        private readonly Lazy<dynamic> lazyDynamic; 

        public HttpWebResponse Response { get; private set; }

        public string Json { get; private set; }

        public dynamic Dynamic { get { return lazyDynamic.Value; } }
    }
}