using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestTaskUKAD
{
    public class SitemapURL
    {
        public List<string> GetSitemapURLs(string userURL)
        {
            List<string> listSitemap = new List<string>();

            try
            {
                Console.WriteLine();
                string sitemapURL = userURL + "/sitemap.xml";

                WebClient wc = new WebClient();

                wc.Encoding = System.Text.Encoding.UTF8;

                string sitemapString = wc.DownloadString(sitemapURL);

                XmlDocument urlDoc = new XmlDocument();

                urlDoc.LoadXml(sitemapString);

                XmlNodeList xmlSitemapList = urlDoc.GetElementsByTagName("url");

                foreach (XmlNode node in xmlSitemapList)
                {
                    if (node["loc"] != null)
                    {
                        listSitemap.Add(node["loc"].InnerText);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("File not found");
            }
            Console.WriteLine(Environment.NewLine);

            return listSitemap;
        }

        public List<string> UniqueSitemapURL(List<string> listSitemap, HashSet<string> listCrawl)
        {
            Console.WriteLine(Environment.NewLine);
            List<string> listUniqueSitemapURL = listSitemap.Except(listCrawl).ToList();
            return listUniqueSitemapURL;
        }
    }
}
