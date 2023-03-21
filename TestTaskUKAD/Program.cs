using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTaskUKAD
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your URL");
            string userURL = Console.ReadLine();

            int urlCount = 1;
            CrawlURL crawl = new CrawlURL();
            List<string> listCrawl = crawl.FindUrls(userURL);
            Console.WriteLine("Urls FOUNDED BY CRAWLING THE WEBSITE");
            Console.WriteLine(string.Join(Environment.NewLine, listCrawl.Select(a => $"{urlCount++}) {a}")));

            urlCount = 1;
            SitemapURL sitemapURL = new SitemapURL();
            List<string> listSitemap = sitemapURL.GetSitemapURLs(userURL);
            Console.WriteLine("Urls FOUNDED IN SITEMAP THE WEBSITE");
            Console.WriteLine(string.Join(Environment.NewLine, listSitemap.Select(a => $"{urlCount++}) {a}")));

            ResponseTiming timing = new ResponseTiming();
            
            urlCount = 1;
            List<string> listUniqueCrawlURL = crawl.UniqueCrawlURL(listCrawl, listSitemap);
            Console.WriteLine("Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml");
            Console.WriteLine(string.Join(Environment.NewLine, listUniqueCrawlURL.Select(a => $"{urlCount++}) {a}")));


            List<string> listUniqueSitemapURL = new List<string>();
            if (listSitemap.Count != 0 ) 
            {
                urlCount = 1;
                listUniqueSitemapURL = sitemapURL.UniqueSitemapURL(listSitemap, listCrawl);
                Console.WriteLine("Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site");
                Console.WriteLine(string.Join(Environment.NewLine, listUniqueSitemapURL.Select(a => $"{urlCount++}) {a}")));
            } else
            {
                Console.WriteLine("Sitemap not found");
            }


            urlCount = 1;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Union URLs with response timing");
            List<string> listUnionUrls = listCrawl.Union(listUniqueSitemapURL).ToList();

            timing.ResponseTime(listUnionUrls);

            Console.WriteLine(Environment.NewLine);
            Console.Write("Urls(html documents) found after crawling a website: " + listCrawl.Count());
            Console.WriteLine(Environment.NewLine);
            Console.Write("Urls found in sitemap: " + listSitemap.Count());
            Console.WriteLine(Environment.NewLine);
        }
    }
}
