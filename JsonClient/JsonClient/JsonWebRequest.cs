using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace JsonClient
{
    /// <summary>
    /// Used internally to create and send a json web request.
    /// </summary>
    internal partial class JsonWebRequest
    {
        /// <summary>
        /// Creates and sends a json request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="verb">The verb.</param>
        /// <param name="content">The content.</param>
        /// <param name="headers">The headers to include in the request.</param>
        /// <returns>
        /// The json result.
        /// </returns>
        internal static JsonResult SendRequest(string url, string verb, string content = null, Dictionary<string, string> headers = null)
        {
            //  Create the request.
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = verb;
            
            //  Add custom headers.
            if(headers != null)
            {
                foreach(var headerKey in headers.Keys)
                    request.Headers.Add(headerKey, headers[headerKey]);
            }

            //  If there is any content, set it.
            if (string.IsNullOrEmpty(content) == false)
            {
                try
                {
                    request.ContentLength = content.Length;
                    request.ContentType = "application/json";
                    using (var stream = request.GetRequestStream())
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(content);
                    }
                }
                catch (Exception exception)
                {
                    return new JsonResult(exception);
                }
            }

            //  Get the response.
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                    return new JsonResult(response);
            }
            catch (Exception exception)
            {
                return new JsonResult(exception);
            }
        }
    }
}
