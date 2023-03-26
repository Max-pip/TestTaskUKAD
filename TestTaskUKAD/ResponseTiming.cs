using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskUKAD
{
    public class ResponseTiming
    {
        public void ResponseTime(List<string> listUnionUrls) 
        {
            int urlCount = 1;
            var responseTimes = new Dictionary<string, long>();

            foreach (var url in listUnionUrls)
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri absoluteUrl))
                {
                    continue;
                }

                var request = WebRequest.Create(absoluteUrl);
                var stopwatch = new Stopwatch();

                try
                {
                    stopwatch.Start();
                    var response = request.GetResponse();
                    stopwatch.Stop();

                    responseTimes[absoluteUrl.AbsoluteUri] = stopwatch.ElapsedMilliseconds;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to retrieve URL {absoluteUrl}: {ex.Message}");
                }
            }

            var sortedUrls = responseTimes.OrderBy(kvp => kvp.Value)
                                         .Select(kvp => kvp.Key);

            foreach (var url in sortedUrls)
            {
                Console.WriteLine($"{urlCount++}){url} - {responseTimes[url]} ms");
            }
        }
    }
}
