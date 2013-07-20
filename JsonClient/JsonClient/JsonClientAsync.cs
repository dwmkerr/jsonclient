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
        /// Asynchronously perform a request using the provided verb, such as GET to the specified url.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public static async Task<JsonResult> RequestAsync(string verb, string url)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(url, verb);
        }

        /// <summary>
        /// Asynchronously perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public static async Task<JsonResult> GetAsync(string url)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(url, "GET");
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
            return await JsonWebRequest.SendRequestAsync(url, "POST", JsonEncoder.Encode(data));
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
            return await JsonWebRequest.SendRequestAsync(url, "POST", json);
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
            return await JsonWebRequest.SendRequestAsync(url, "PUT", JsonEncoder.Encode(data));
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
            return await JsonWebRequest.SendRequestAsync(url, "PUT", json);
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
            return await JsonWebRequest.SendRequestAsync(url, "DELETE");
        }
    }
}