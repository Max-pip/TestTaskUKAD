using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskUKAD
{
    public class CrawlURL
    {
        public HashSet<string> GetCrawlURLs(string userURL)
        {
            string URL = userURL;

            HashSet<string> listCrawl = new HashSet<string>();
            var baseUri = new Uri(URL);
            var web = new HtmlWeb();
            var doc = web.Load(baseUri);

            foreach (var link in doc.DocumentNode.Descendants("a"))
            {
                var href = link.GetAttributeValue("href", "");
                var uri = new Uri(baseUri, href);

                if (uri.Host == baseUri.Host && uri.Scheme == baseUri.Scheme)
                {
                    listCrawl.Add(uri.AbsoluteUri);
                }
            }

            return listCrawl;
        }

        public List<string> UniqueCrawlURL(HashSet<string> listCrawl, List<string> listSitemap)
        {
            Console.WriteLine(Environment.NewLine);
            List<string> listUniqueCrawlURL = listCrawl.Except(listSitemap).ToList();
            return listUniqueCrawlURL;
        }
    }
}
