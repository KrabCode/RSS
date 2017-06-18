using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSS
{
    public class Watchlist 
    {
        private string _watchlistFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RSS\\" + "Settings.txt";
        private Downloader dl = new Downloader();
        //Viewmodel for the watchlistGui listview
        public ObservableCollection<WatchlistItem> MainWatchlist {get;set;}
        BackgroundWorker bw = new BackgroundWorker();
        List<string> downloadQueue = new List<string>();

        public Watchlist()
        {
            MainWatchlist = loadSettings();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
            window.consoleOutput = "Settings stored at: " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RSS";
        }

        public void addRssFeed(WatchlistItem item)
        {
            if (!doesWatchlistContainUrl(item.url))
            {
                item.PropertyChanged += Item_PropertyChanged;
                MainWatchlist.Add(item);                
                saveSettings();
                refreshRssFeed(item.url);                
            }
            else
            {
                MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
                window.consoleOutput = "RSS address already in use!";
            }
            
        }

        private bool doesWatchlistContainUrl(string url)
        {
            bool result = false;
            foreach(WatchlistItem item in MainWatchlist)
            {
                if(item.url == url)
                {
                    result = true;
                }
            }
            return result;
        }

        public void removeRssFeed(string url)
        {
            WatchlistItem itemToRemove = null;
            foreach(WatchlistItem item in MainWatchlist)
            {
                if(item.url == url)
                {
                    itemToRemove = item;
                    break;
                }
            }
            if (itemToRemove != null)
            {
                MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
                window.consoleOutput = "Removed feed: " + itemToRemove.title + " with url: " + itemToRemove.url;
                MainWatchlist.Remove(itemToRemove);
                saveSettings();
            }
            
        }

        private void saveSettings()
        {
            string json = JsonConvert.SerializeObject(MainWatchlist,Formatting.Indented);
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
        }

        public void refreshAllKnownFeeds()
        {
            foreach (var rssSettings in MainWatchlist)
                try
                {
                    refreshRssFeed(rssSettings.url);
                }
                catch (Exception e)
                {
                    MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
                    window.consoleOutput = e.Message;
                }
        }

        public void refreshRssFeed(string url)
        {
            if (bw.IsBusy != true)
            {
                MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
                window.consoleOutput = "Downloading from: "+ url;
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
            else
            {
                bw_sayGoodbye();
            }
        }

        private void bw_sayGoodbye()
        {
            MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
            window.consoleOutput = "Downloads finished";
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            downloadRssFeed(Convert.ToString(e.Argument));
        }
        

        private void downloadRssFeed(string url)
        {
            WatchlistItem rssSettings = null;
            bool rssSettingsFound = false;
            foreach (WatchlistItem item in MainWatchlist)
            {
                if (item.url == url)
                {
                    rssSettings = item;
                    rssSettings.status = "downloading...";
                    rssSettings.timestamp = DateTime.Now.ToLocalTime().ToString();
                    rssSettingsFound = true;
                    break;
                }
            }
            if (rssSettingsFound)
            {
                var newFeed = dl.GetXml(url);
                if (newFeed != null)
                {
                    int intendedBacklogSize = rssSettings.backlogSize;
                    int index = 0;
                    foreach (var item in newFeed.Channel.Items)
                    {
                        foreach (var enclosureItem in item.Enclosures)
                        {
                            string filename = dl.getFilenameFromFileUrl(enclosureItem.Url.ToString());
                            if (!File.Exists(rssSettings.folder + "//" + filename) && MainWatchlist.Contains(rssSettings))
                            {
                                dl.DownloadItemEnclosures(item, rssSettings.folder, filename);
                            }
                        }
                        if(index++ > intendedBacklogSize)
                        {
                            break;
                        }
                    }
                    rssSettings.status = "up to date";
                }
            }
        }
    }


}
