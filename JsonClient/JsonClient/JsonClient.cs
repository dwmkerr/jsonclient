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
        public static JsonResult Get(string url)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "GET", null);
        }

        public static async Task<JsonResult> GetAsync(string url)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "GET", null);
        }

        public static JsonResult Post(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "POST", data);
        }

        public static async Task<JsonResult> PostAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "POST", data);
        }

        public static JsonResult Put(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "PUT", data);
        }

        public static async Task<JsonResult> PutAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "PUT", data);
        }

        public static JsonResult Delete(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "DELETE", data);
        }
        public static async Task<JsonResult> DeleteAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "DELETE", data);
        }


        private static JsonResult ProcessResponse(HttpWebResponse response)
        {
            //  sensible in all cases?
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string message = String.Format("GET failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }

            //  Create a result object.
            return new JsonResult(response);
        }

        private static JsonResult SendRequest(string url, string verb, object content)
        {
            //  Create the request, process the response.
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = verb;
            SetRequestContent(request, content);

            //  Get and process the response.
            using (var response = (HttpWebResponse)request.GetResponse())
                return ProcessResponse(response);
        }

        private static async Task<JsonResult> SendRequestAsync(string url, string verb, object content)
        {
            //  Create the request, process the response.
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = verb;
            request = await SetRequestContentAsync(request, content);

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
                return ProcessResponse(response);
        }

        private static void SetRequestContent(HttpWebRequest request, object content)
        {
            request.ContentLength = 0;
            request.ContentType = "application/json";

            if (content != null)
            {
                var jsonContent = Json.Encode(content);
                request.ContentLength = jsonContent.Length;
                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(jsonContent);
                }
            }
        }

        private static async Task<HttpWebRequest> SetRequestContentAsync(HttpWebRequest request, object content)
        {
            request.ContentLength = 0;
            request.ContentType = "application/json";

            if (content != null)
            {
                var jsonContent = Json.Encode(content);
                request.ContentLength = jsonContent.Length;
                using (var stream = await request.GetRequestStreamAsync())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(jsonContent);
                }
            }

            return request;
        }
    }

    public class JsonResult
    {
        internal JsonResult(HttpWebResponse response)
        {
            ReadContent(response);
            CopyResponseData(response);
            lazyDynamic = new Lazy<dynamic>(() => string.IsNullOrEmpty(Json) ? null : System.Web.Helpers.Json.Decode(Json));
        }

        private void ReadContent(HttpWebResponse response)
        {
            //  Do we have response content?
            string json = null;
            if (response.ContentLength > 0)
            {
                //  Read get the response stream.  
                using (var responseStream = response.GetResponseStream())
                {
                    //  todo is the valid here?
                    if (responseStream == null)
                        throw new InvalidOperationException("Invalid response stream.");
                    using (var reader = new StreamReader(responseStream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            Json = json;
        }

        private void CopyResponseData(HttpWebResponse response)
        {
            Respose = new Response
                          {
                              CharacterSet = response.CharacterSet,
                              ContentEncoding = response.ContentEncoding,
                              ContentLength = response.ContentLength,
                              ContentType = response.ContentType,
                              Cookies = response.Cookies,
                              Headers = response.Headers,
                              IsMutuallyAuthenticated = response.IsMutuallyAuthenticated,
                              LastModified = response.LastModified,
                              Method = response.Method,
                              ProtocolVersion = response.ProtocolVersion,
                              ResponseUri = response.ResponseUri,
                              Server = response.Server,
                              StatusCode = response.StatusCode,
                              StatusDescription = response.StatusDescription,
                              SupportsHeaders = response.SupportsHeaders
                          };
        }

        private readonly Lazy<dynamic> lazyDynamic;

        public string Json { get; internal set; }

        public dynamic Dynamic { get { return lazyDynamic.Value; } }

        public Response Respose { get; internal set; }

        public class Response
        {
            public string CharacterSet { get; internal set;}
            public string ContentEncoding { get; internal set;}
            public long ContentLength { get; internal set;}
            public string ContentType { get; internal set;}
            public CookieCollection Cookies { get; internal set; }
            public WebHeaderCollection Headers { get; internal set;}
            public bool IsMutuallyAuthenticated { get; internal set;}
            public DateTime LastModified { get; internal set;}
            public string Method { get; internal set;}
            public Version ProtocolVersion { get; internal set;}
            public Uri ResponseUri { get; internal set;}
            public string Server { get; internal set;}
            public HttpStatusCode StatusCode { get; internal set;}
            public string StatusDescription { get; internal set;}
            public bool SupportsHeaders { get; internal set;}
        }
    }
}