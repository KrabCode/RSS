using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RSS
{
    public class Watchlist 
    {
        private string _watchlistFilePath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("RSS.exe", "") + "Settings.txt";
        private Downloader dl = new Downloader();
        //Viewmodel for the watchlistGui listview
        public ObservableCollection<WatchlistItem> Settings {get;set;}
            
        public Watchlist()
        {
            Settings = loadSettings();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        

        public void addRssFeed(string url, string title, string folderPath)
        {
            WatchlistItem item = new WatchlistItem();
            item.folder = folderPath;
            item.url = url;
            item.title = title;    
            Settings.Add(item);
            saveSettings();
            refreshRssFeed(url);
        }

        public void removeRssFeed(string url)
        {
            WatchlistItem itemToRemove = null;
            foreach(WatchlistItem item in Settings)
            {
                if(item.url == url)
                {
                    itemToRemove = item;
                    break;
                }
            }
            if (itemToRemove != null)
            {
                Settings.Remove(itemToRemove);
            }
            saveSettings();
        }

        private void saveSettings()
        {
            string json = JsonConvert.SerializeObject(Settings,Formatting.Indented);
            File.WriteAllLines(_watchlistFilePath, new String[] { json }, Encoding.UTF8);
        }

        private ObservableCollection<WatchlistItem> loadSettings()
        {
            if (File.Exists(_watchlistFilePath))
            {
                string json = File.ReadAllText(_watchlistFilePath);
                ObservableCollection<WatchlistItem> items = JsonConvert.DeserializeObject<ObservableCollection<WatchlistItem>>(json);
                foreach(var item in items)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
                return items;
            }
            else
            {
                return new ObservableCollection<WatchlistItem>();
            }            
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            saveSettings();
            //TODO make this work
        }

        public void refreshAllKnownFeeds()
        {
            foreach (var rssSettings in Settings)
                try
                {
                    refreshRssFeed(rssSettings.url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + e.StackTrace);
                }
        }

        public void refreshRssFeed(string url)
        {
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync(url);
            }
            else
            {
                downloadQueue.Add(url);
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (downloadQueue.Count > 0)
            {
                var item = downloadQueue[0];
                downloadQueue.Remove(item);
                bw.RunWorkerAsync(item);
            }
        }

        BackgroundWorker bw = new BackgroundWorker();
        List<string> downloadQueue = new List<string>();
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            downloadRssFeed(Convert.ToString(e.Argument));
        }
        

        private void downloadRssFeed(string url)
        {
            WatchlistItem rssSettings = null;
            bool rssSettingsFound = false;
            foreach (WatchlistItem item in Settings)
            {
                if (item.url == url)
                {
                    rssSettings = item;
                    rssSettings.Status = "downloading...";
                    rssSettings.Timestamp = DateTime.Now.ToLocalTime().ToString();
                    rssSettingsFound = true;
                    break;
                }
            }
            if (rssSettingsFound)
            {
                var newFeed = dl.GetXml(url);

                if (newFeed != null)
                {
                    foreach (var item in newFeed.Channel.Items)
                    {
                        foreach (var enclosureItem in item.Enclosures)
                        {
                            string filename = dl.getFilenameFromFileUrl(enclosureItem.Url.ToString());
                            if (!File.Exists(rssSettings.folder + "//" + filename) && Settings.Contains(rssSettings))
                            {

                                dl.DownloadItemEnclosures(item, rssSettings.folder, filename);
                            }
                        }
                    }
                    rssSettings.Status = "up to date";
                }
            }

        }
    }
}
