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
    public class JsonWebClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonWebClient"/> class.
        /// </summary>
        public JsonWebClient()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonWebClient"/> class.
        /// </summary>
        /// <param name="serviceRoot">The service root.</param>
        public JsonWebClient(string serviceRoot)
        {
            serviceUri = new Uri(serviceRoot);
        }

        /// <summary>
        /// Perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public JsonResult Get(string url)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "GET");
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
        public JsonResult Post(string url, object data)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "POST", JsonEncoder.Encode(data));
        }

        /// <summary>
        /// POST json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public JsonResult Post(string url, string json)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "POST", json);
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
        public JsonResult Put(string url, object data)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "PUT", JsonEncoder.Encode(data));
        }

        /// <summary>
        /// PUT json to the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="json">The json data.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public JsonResult Put(string url, string json)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "PUT", json);
        }

        /// <summary>
        /// DELETE the data from the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// A JsonResult for the response.
        /// </returns>
        public JsonResult Delete(string url)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "DELETE");
        }

        /// <summary>
        /// Builds the URL from the service root and the provided relative path.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The url.</returns>
        private string BuildUrl(string url)
        {
            return serviceUri == null ? url : new Uri(serviceUri, url).ToString();
        }

        private readonly Uri serviceUri;
    }
}
