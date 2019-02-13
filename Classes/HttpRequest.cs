using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YoutubePlayer
{
    static class HttpRequest
    {
            public static async Task<string> GetRequest(Dictionary<string, string> data)
        {
                string apiBaseUrl = "https://gozaltech.org/yap/yap_api";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            HttpClient client = new HttpClient();
            var postData = new FormUrlEncodedContent(data);
            HttpResponseMessage responseMessage = await client.PostAsync(apiBaseUrl, postData);
            string responseBody = await responseMessage.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
