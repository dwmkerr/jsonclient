﻿using System;
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
        /// Perform a request using the provided verb, such as GET to the specified url.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public JsonResult Request(string verb, string url)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, verb, null, Headers);
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
        public JsonResult Request(string verb, string url, object data)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, verb, JsonEncoder.Encode(data), Headers);
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
        public JsonResult Request(string verb, string url, string json)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(url, verb, json, Headers);
        }

        /// <summary>
        /// Perform a GET request for the specified url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>A JsonResult for the response.</returns>
        public JsonResult Get(string url)
        {
            //  Send the request with the correct verb.
            return JsonWebRequest.SendRequest(BuildUrl(url), "GET", null, headers);
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
            return JsonWebRequest.SendRequest(BuildUrl(url), "POST", JsonEncoder.Encode(data), headers);
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
            return JsonWebRequest.SendRequest(BuildUrl(url), "POST", json, headers);
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
            return JsonWebRequest.SendRequest(BuildUrl(url), "PUT", JsonEncoder.Encode(data), headers);
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
            return JsonWebRequest.SendRequest(BuildUrl(url), "PUT", json, headers);
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
            return JsonWebRequest.SendRequest(BuildUrl(url), "DELETE", null, headers);
        }

        /// <summary>
        /// Adds the basic authorization header to the client.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void AddBasicAuthorizationHeader(string username, string password)
        {
            var headerValue = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            //  Remove the authentication header if we have it.
            var currentAuthKey =
                Headers.Keys.FirstOrDefault(k => string.Compare(k, "Authorization", StringComparison.OrdinalIgnoreCase) == 0);
            if (currentAuthKey != null)
                Headers[currentAuthKey] = headerValue;
            else
                Headers["Authorization"] = headerValue;
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
        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

        /// <summary>
        /// Gets the headers. These headers are addded to requests sent from the client.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public Dictionary<string, string> Headers { get { return headers; } } 
    }
}
