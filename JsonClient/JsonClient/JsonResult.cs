using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

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
        public override string ToString()
        {
            return string.IsNullOrEmpty(Json) ? base.ToString() : Json;
        }

        internal JsonResult(HttpWebResponse response)
        {
            ReadContent(response);
            CopyResponseData(response);
            lazyDynamic = new Lazy<dynamic>(() => string.IsNullOrEmpty(Json) ? null : System.Web.Helpers.Json.Decode(Json));
        }

        internal JsonResult(Exception exception)
        {
            Error = exception;
            if(exception is WebException)
            {
                CopyResponseData(((WebException) exception).Response);
            }
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

        private void CopyResponseData(WebResponse response)
        {
            Response = new ResponseDetails
            {
                ContentLength = response.ContentLength,
                ContentType = response.ContentType,
                Headers = response.Headers,
                IsMutuallyAuthenticated = response.IsMutuallyAuthenticated,
                ResponseUri = response.ResponseUri,
                SupportsHeaders = response.SupportsHeaders
            };
            var httpResponse = response as HttpWebResponse;
            if(httpResponse != null)
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
        public dynamic Dynamic { get { return lazyDynamic.Value; } }

        /// <summary>
        /// Gets the respose object, which contains details about the response.
        /// </summary>
        /// <value>
        /// The respose object, which contains details about the response.
        /// </value>
        public ResponseDetails Response { get; internal set; }

        /// <summary>
        /// Gets the exception that occured during the request (if any).
        /// </summary>
        /// <value>
        /// The the exception that occured during the request (if any).
        /// </value>
        public Exception Error { get; internal set; }

        public class ResponseDetails
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