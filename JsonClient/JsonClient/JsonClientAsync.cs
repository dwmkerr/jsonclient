using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace JsonClient
{
    public partial class JsonClient
    {
        /// <summary>
        /// Asynchronously perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public static async Task<JsonResult> GetAsync(string url)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "GET", null, null);
        }

        /// <summary>
        /// Asynchronously POST data to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data, which will automatically be 
        /// encoded as json.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static async Task<JsonResult> PostAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "POST", data, null);
        }

        /// <summary>
        /// Asynchronously POST json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static async Task<JsonResult> PostAsync(string url, string json)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "POST", null, json);
        }

        /// <summary>
        /// PUT data to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data, which will automatically be 
        /// encoded as json.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static async Task<JsonResult> PutAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "PUT", data, null);
        }

        /// <summary>
        /// PUT json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static async Task<JsonResult> PutAsync(string url, string json)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "PUT", null, json);
        }

        /// <summary>
        /// DELETE the data from the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static async Task<JsonResult> DeleteAsync(string url)
        {
            //  Send the request with the correct verb.
            return await SendRequestAsync(url, "DELETE", null, null);
        }
        
        private static async Task<JsonResult> SendRequestAsync(string url, string verb, object contentData, string contentJson)
        {
            //  Create the request, process the response.
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = verb;
                request = await SetRequestContentAsync(request, contentData, contentJson);

                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                    return new JsonResult(response);
            }
            catch (Exception exception)
            {
                return new JsonResult(exception);
            }
        }

        private static async Task<HttpWebRequest> SetRequestContentAsync(HttpWebRequest request, object contentData, string contentJson)
        {
            if (contentData != null || contentJson != null)
            {
                var jsonContent = contentJson ?? Json.Encode(contentData);
                request.ContentLength = jsonContent.Length;
                request.ContentType = "application/json";
                using (var stream = await request.GetRequestStreamAsync())
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(jsonContent);
                }
            }

            return request;
        }
    }
}