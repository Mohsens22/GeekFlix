using Common;
using Imagine.Uwp.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeekFlixClient
{
    public static class Rest
    {
        static string baseUri = "http://ec2-52-50-156-235.eu-west-1.compute.amazonaws.com/api/videos";
        public static List<OutputItem> getListAsync(int limit = 30, int offset = 0)
        {
            var uri = baseUri + $"?limit={limit}&offset={offset}";
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var str = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<OutputItem>>(str);
            }
        }
        public static void DeleteItem(OutputItem item)
        {
            var uri = baseUri + $"/{item.OutputPath}";
            WebRequest request = WebRequest.Create(uri);
            request.Method = "DELETE";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {
                $"Something went wrong {e.Message}".ShowMessage(e.StackTrace);
            }

        }
        public static Uri GetDownloadLink(this OutputItem item) => new Uri(baseUri + $"/{item.OutputPath}");

    }
}
