using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskUKAD
{
    public class ResponseTiming
    {
        public void ResponseTime(List<string> listUnionUrls)
        {
            int urlCount = 1;
            HttpClient client = new HttpClient();
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (string urls in listUnionUrls)
            {
                double responseTimeMs = client.GetAsync(urls).ContinueWith(task => stopwatch.Elapsed.TotalMilliseconds).Result;
                Console.WriteLine($"{urlCount++}) Response time for {urls} - {responseTimeMs} ms");
            }
            stopwatch.Stop();
        }
    }
}
