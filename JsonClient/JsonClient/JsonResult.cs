using System;
using System.IO;
using System.Net;

namespace JsonClient
{
    /// <summary>
    /// Represents the result of a call by the JsonClient.
    /// Use the <see cref="Json"/> property to get the raw Json text, the 
    /// <see cref="Dynamic"/> property to get the result as a dynamic object,
    /// the <see cref="Response"/> property for low-level response details and the
    /// <see cref="Error"/> property to get the details of any exception that occured.
    /// </summary>
    public class JsonResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResult"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        internal JsonResult(HttpWebResponse response)
        {
            ReadContent(response);
            CopyResponseData(response);
            lazyDynamic = new Lazy<dynamic>(() => string.IsNullOrEmpty(Json) ? null : System.Web.Helpers.Json.Decode(Json));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResult"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        internal JsonResult(Exception exception)
        {
            Error = exception;
            var webException = exception as WebException;
            if (webException != null && webException.Response != null)
            {
                CopyResponseData(webException.Response);
            }
            lazyDynamic = new Lazy<dynamic>(() => string.IsNullOrEmpty(Json) ? null : System.Web.Helpers.Json.Decode(Json));
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// If the instance contains Json, the Json is returned.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Json) ? base.ToString() : Json;
        }

        private void ReadContent(WebResponse response)
        {
            //  Do we have response content?
            string json = null;
            if (response.ContentLength > 0)
            {
                //  Read get the response stream.  
                using (var responseStream = response.GetResponseStream())
                {
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

        private void CopyResponseData(WebResponse response)
        {
            Response = new HttpResponse
                {
                    ContentLength = response.ContentLength,
                    ContentType = response.ContentType,
                    Headers = response.Headers,
                    IsMutuallyAuthenticated = response.IsMutuallyAuthenticated,
                    ResponseUri = response.ResponseUri
#if LITE
    //  Only in .NET 4.5
                ,SupportsHeaders = response.SupportsHeaders
#endif
                };
            var httpResponse = response as HttpWebResponse;
            if (httpResponse != null)
            {
                Response.CharacterSet = httpResponse.CharacterSet;
                Response.ContentEncoding = httpResponse.ContentEncoding;
                Response.Cookies = httpResponse.Cookies;
                Response.LastModified = httpResponse.LastModified;
                Response.ProtocolVersion = httpResponse.ProtocolVersion;
                Response.Server = httpResponse.Server;
                Response.StatusCode = httpResponse.StatusCode;
                Response.StatusDescription = httpResponse.StatusDescription;
            }
        }

        /// <summary>
        /// The internally used lazy dynamic result object.
        /// </summary>
        private readonly Lazy<dynamic> lazyDynamic;

        /// <summary>
        /// Gets the raw json result.
        /// </summary>
        /// <value>
        /// The raw json result.
        /// </value>
        public string Json { get; internal set; }

        /// <summary>
        /// Gets the dynamic result.
        /// </summary>
        /// <value>
        /// The dynamic result result.
        /// </value>
        public dynamic Dynamic
        {
            get { return lazyDynamic.Value; }
        }

        /// <summary>
        /// Gets the respose object, which contains details about the response.
        /// </summary>
        /// <value>
        /// The respose object, which contains details about the response.
        /// </value>
        public HttpResponse Response { get; internal set; }

        /// <summary>
        /// Gets the exception that occured during the request (if any).
        /// </summary>
        /// <value>
        /// The the exception that occured during the request (if any).
        /// </value>
        public Exception Error { get; internal set; }
    }
}