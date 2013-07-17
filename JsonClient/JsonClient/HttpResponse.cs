using System;
using System.Net;

namespace JsonClient
{
    /// <summary>
    /// Details of an HTTP response.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Gets the character set of the response.
        /// </summary>
        /// <value>
        /// The character set of the response.
        /// </value>
        public string CharacterSet { get; internal set; }

        /// <summary>
        /// Gets the method used to encode the content of the response.
        /// </summary>
        /// <value>
        /// The method used to encode the content of the response.
        /// </value>
        public string ContentEncoding { get; internal set; }

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        /// <value>
        /// The length of the content.
        /// </value>
        public long ContentLength { get; internal set; }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; internal set; }

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        /// <value>
        /// The cookies.
        /// </value>
        public CookieCollection Cookies { get; internal set; }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public WebHeaderCollection Headers { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is mutually authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is mutually authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsMutuallyAuthenticated { get; internal set; }

        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <value>
        /// The last modified time.
        /// </value>
        public DateTime LastModified { get; internal set; }

        /// <summary>
        /// Gets the method for the response.
        /// </summary>
        /// <value>
        /// The method for the response.
        /// </value>
        public string Method { get; internal set; }

        /// <summary>
        /// Gets the protocol version.
        /// </summary>
        /// <value>
        /// The protocol version.
        /// </value>
        public Version ProtocolVersion { get; internal set; }

        /// <summary>
        /// Gets the response URI.
        /// </summary>
        /// <value>
        /// The response URI.
        /// </value>
        public Uri ResponseUri { get; internal set; }

        /// <summary>
        /// Gets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public string Server { get; internal set; }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// Gets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the response supports headers.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supports headers; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsHeaders { get; internal set; }
    }
}