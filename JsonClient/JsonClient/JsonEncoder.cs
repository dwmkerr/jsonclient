using System.Web.Helpers;

namespace JsonClient
{
    /// <summary>
    /// Utility class to encode/decode json.
    /// </summary>
    internal static class JsonEncoder
    {
        /// <summary>
        /// Encodes the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <returns>The json representation of the object.</returns>
        public static string Encode(object @object)
        {
            //  Currently, use the Web Helpers.
            return Json.Encode(@object);
        }

        /// <summary>
        /// Decodes the specified json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>The decoded object.</returns>
        public static dynamic Decode(string json)
        {
            //  Currently, use the Web Helpers.
            return Json.Decode(json);
        }
    }
}
