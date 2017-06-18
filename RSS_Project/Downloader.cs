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
                try
                {
                    new WebClient().DownloadFile(enclosure.Url, folderPath + "//" + filepath);
                } catch (Exception e)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
                        window.consoleOutput = e.Message + e.StackTrace;
                    }));
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


