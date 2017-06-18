using Argotic.Syndication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace RSS
{
    public class Downloader
    {

        public RssFeed GetXml(string uri)
        {
            XmlReader reader = XmlReader.Create(uri);
            RssFeed feed = new RssFeed(new Uri(uri), uri);
            feed.Load(reader);
            return feed;
        }

        public void DownloadItemEnclosures(RssItem item, string folderPath, string filepath)
        {
            Directory.CreateDirectory(folderPath);
            foreach (var enclosure in item.Enclosures)
            {
                Console.WriteLine("downloading the following file: " + enclosure.Url.ToString() + " to: " + folderPath);
                try
                {
                    new WebClient().DownloadFile(enclosure.Url, folderPath + "//" + filepath);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message + e.StackTrace);
                }
            }
        }

        public string getFilenameFromFileUrl(string url)
        {
            string[] split = url.Split('/');
            return split[split.Length - 1];
        }        
    }
}


