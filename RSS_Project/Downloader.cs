using Argotic.Syndication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Windows;

namespace RSS
{
    public class Downloader
    {

        public RssFeed GetXml(string uri)
        {
            try
            {
                XmlReader reader = XmlReader.Create(uri);
                var feed = new RssFeed(new Uri(uri), uri);                
                feed.Load(reader);
                return feed;
            }
            catch (Exception e){
                Console.WriteLine(e.StackTrace);                
            }
            return null;
            
        }
        

        internal void DownloadEnclosureFiles(string url, string folderPath, string filepath, RssItem item)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            WebClient client = new WebClient();
            foreach (var enclosure in item.Enclosures)
            {
                try
                {
                    client.DownloadFile(enclosure.Url, folderPath + "//" + filepath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + e.StackTrace);
                }
            }
            client.Dispose();
        }
    }
}


