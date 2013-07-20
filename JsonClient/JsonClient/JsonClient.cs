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
        /// Perform a request using the provided verb, such as GET to the specified url.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public static JsonResult Request(string verb, string url)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, verb);
        }

        /// <summary>
        /// Perform a request using the provided verb, such as GET to the specified url.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static JsonResult Request(string verb, string url, object data)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, verb, JsonEncoder.Encode(data));
        }

        /// <summary>
        /// Perform a request using the provided verb, such as GET to the specified url.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public static JsonResult Request(string verb, string url, string json)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, verb, json);
        }

        /// <summary>
        /// Perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public static JsonResult Get(string url)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, "GET");
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
            return JsonWebRequest.SendRequest(url, "POST", JsonEncoder.Encode(data));
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
            return JsonWebRequest.SendRequest(url, "POST", json);
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
            return JsonWebRequest.SendRequest(url, "PUT", JsonEncoder.Encode(data));
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
            return JsonWebRequest.SendRequest(url, "PUT", json);
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
            return JsonWebRequest.SendRequest(url, "DELETE");
        }
    }
}