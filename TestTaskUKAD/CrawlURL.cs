using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskUKAD
{
    public class CrawlURL
    {
        public List<string> FindUrls(string url)
        {
            var urls = new List<string>();
            var visitedUrls = new HashSet<string>();

            string urlUser = url;

            Uri uri = new Uri(urlUser);

            string homePage = $"{uri.Scheme}://{uri.Authority}";

            FindUrlsRecursive(homePage, urls, visitedUrls);

            return urls;
        }

        private void FindUrlsRecursive(string url, List<string> urls, HashSet<string> visitedUrls)
        {
            if (visitedUrls.Contains(url)) return;

            visitedUrls.Add(url);

            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                var reader = new System.IO.StreamReader(stream);
                var html = reader.ReadToEnd();

                var matches = System.Text.RegularExpressions.Regex.Matches(html, @"<a.*?href=""(.*?)"".*?>");

                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    var newUrl = match.Groups[1].Value;

                    if (!string.IsNullOrEmpty(newUrl))
                    {
                        var uri = new Uri(new Uri(url), newUrl);
                        newUrl = uri.AbsoluteUri;

                        if (newUrl.StartsWith(url) && !visitedUrls.Contains(newUrl))
                        {
                            // Check if the URL with an anchor leads to the same page as the previous URL
                            var prevUri = new Uri(url);
                            var currUri = new Uri(newUrl);
                            if (prevUri.PathAndQuery == currUri.PathAndQuery && prevUri.Fragment != currUri.Fragment)
                            {
                                continue;
                            }

                            if (newUrl.EndsWith("/"))
                            {
                                urls.Add(newUrl);
                            }
                            else
                            {
                                urls.Add(newUrl + "/");
                            }

                            //urls.Add(newUrl);
                            FindUrlsRecursive(newUrl, urls, visitedUrls);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve URL {url}: {ex.Message}");
            }
        }

        public List<string> UniqueCrawlURL(List<string> listCrawl, List<string> listSitemap)
        {
            Console.WriteLine(Environment.NewLine);
            List<string> listUniqueCrawlURL = listCrawl.Except(listSitemap).ToList();
            return listUniqueCrawlURL;
        }
    }
}
