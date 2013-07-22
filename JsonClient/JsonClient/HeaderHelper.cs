using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonClient
{
    public static class HeaderHelper
    {
        public static string BuildBasicAuthHeader(string username, string password)
        {
            return "Authorization: Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
        }
    }
}
