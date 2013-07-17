using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace JsonClient
{
    /// <summary>
    /// The JsonClient is a lightweight class that lets you make calls 
    /// to Json Web APIs.
    /// </summary>
    public partial class JsonClient
    {
        public static JsonResult Get(string url)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "GET", null, null);
        }
        
        public static JsonResult Post(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "POST", data, null);
        }

        public static JsonResult Post(string url, string json)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "POST", null, json);
        }
        
        public static JsonResult Put(string url, object data)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "PUT", data, null);
        }
        
        public static JsonResult Put(string url, string json)
        {
            //  Send the request with the correct verb.
            return SendRequest(url, "PUT", null, json);
        }

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