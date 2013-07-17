using System;
using System.IO;
using System.Net;

namespace JsonClient
{
    /// <summary>
    /// The JsonClient is a lightweight class that lets you make calls 
    /// to Json Web APIs.
    /// </summary>
    public partial class JsonClient
    {
        /// <summary>
        /// Perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public static JsonResult Get(string url)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "GET", null, null);
        }

        /// <summary>
        /// POST data to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data, which will automatically be 
        /// encoded as json.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static JsonResult Post(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "POST", data, null);
        }

        /// <summary>
        /// POST json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static JsonResult Post(string url, string json)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "POST", null, json);
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
        public static JsonResult Put(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "PUT", data, null);
        }

        /// <summary>
        /// PUT json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static JsonResult Put(string url, string json)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "PUT", null, json);
        }

        /// <summary>
        /// DELETE the data from the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static JsonResult Delete(string url)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "DELETE", null, null);
        }

        private static JsonResult SendRequest(string url, string verb, object contentData, string contentJson)
        {
            //  Create the request, process the response.
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = verb;
                SetRequestContent(request, contentData, contentJson);

                //  Get and process the response.
                using (var response = (HttpWebResponse)request.GetResponse())
                    return new JsonResult(response);
            }
            catch (Exception exception)
            {
                return new JsonResult(exception);
            }
        }
        
        private static void SetRequestContent(HttpWebRequest request, object contentData, string contentJson)
        {
            if (contentData == null && contentJson == null)
                return;
            
            var jsonContent = contentJson ?? JsonEncoder.Encode(contentData);
            request.ContentLength = jsonContent.Length;
            request.ContentType = "application/json";
            using (var stream = request.GetRequestStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(jsonContent);
            }
        }
    }
}