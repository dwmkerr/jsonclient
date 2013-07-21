using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonClient
{
    /// <summary>
    /// The JsonWebClient allows more control over Json requests 
    /// than the static methods offered in the JsonClient class.
    /// The JsonWebClient allows the requests to be fully customised.
    /// </summary>
    public partial class JsonWebClient
    {
        /// <summary>
        /// Perform a request using the provided verb, such as GET to the specified url.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public async Task<JsonResult> RequestAsync(string verb, string url)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(url, verb, null, Headers);
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
        public async Task<JsonResult> RequestAsync(string verb, string url, object data)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(url, verb, JsonEncoder.Encode(data), Headers);
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
        public async Task<JsonResult> RequestAsync(string verb, string url, string json)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(url, verb, json, Headers);
        }

        /// <summary>
        /// Perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public async Task<JsonResult> GetAsync(string url)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(BuildUrl(url), "GET", null, headers);
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
        public async Task<JsonResult> PostAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(BuildUrl(url), "POST", JsonEncoder.Encode(data), headers);
        }

        /// <summary>
        /// POST json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public async Task<JsonResult> PostAsync(string url, string json)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(BuildUrl(url), "POST", json, headers);
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
        public async Task<JsonResult> PutAsync(string url, object data)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(BuildUrl(url), "PUT", JsonEncoder.Encode(data), headers);
        }

        /// <summary>
        /// PUT json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public async Task<JsonResult> PutAsync(string url, string json)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(BuildUrl(url), "PUT", json, headers);
        }

        /// <summary>
        /// DELETE the data from the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public async Task<JsonResult> DeleteAsync(string url)
        {
            //  Send the request with the correct verb.
            return await JsonWebRequest.SendRequestAsync(BuildUrl(url), "DELETE", null, headers);
        }
    }
}
