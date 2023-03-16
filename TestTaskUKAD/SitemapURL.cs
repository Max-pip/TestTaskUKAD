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
                /*Create a new instance of the System.Net Webclient*/
                WebClient wc = new WebClient();
                /*Set the Encodeing on the Web Client*/
                wc.Encoding = System.Text.Encoding.UTF8;
                /* Download the document as a string*/
                string sitemapString = wc.DownloadString(sitemapURL);
                /*Create a new xml document*/
                XmlDocument urlDoc = new XmlDocument();
                /*Load the downloaded string as XML*/
                urlDoc.LoadXml(sitemapString);
                /*Create an list of XML nodes from the url nodes in the sitemap*/
                XmlNodeList xmlSitemapList = urlDoc.GetElementsByTagName("url");
                /*Loops through the node list and prints the values of each node*/

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
