using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.Xml;
using System.Net.Http;
using System.Diagnostics;

namespace TestTaskUKAD
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int urlCount = 1;
            Console.WriteLine("Enter your URL");
            string userURL = Console.ReadLine();

            CrawlURL crawl = new CrawlURL();
            HashSet<string> listCrawl = crawl.GetCrawlURLs(userURL);

            SitemapURL sitemapURL = new SitemapURL();
            List<string> listSitemap = sitemapURL.GetSitemapURLs(userURL);

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
            List<string> listUnionUrls = listUniqueSitemapURL.Union(listUniqueCrawlURL).ToList();

            timing.ResponseTime(listUnionUrls);

            Console.WriteLine(Environment.NewLine);
            Console.Write("Urls(html documents) found after crawling a website: " + listUniqueCrawlURL.Count());
            Console.WriteLine(Environment.NewLine);
            Console.Write("Urls found in sitemap: " + listUniqueSitemapURL.Count());
            Console.WriteLine(Environment.NewLine);
        }
    }
}
