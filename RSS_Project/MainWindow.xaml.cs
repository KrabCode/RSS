using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {

        private DateTime refreshTime;
        private int recheckFrequency;

        private string _autoRefreshFrequencyFilepath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("RSS.exe", "") + "Refresh.txt";

        private Timer timer;
        public Watchlist watchlist;

        public MainWindow()
        {
            InitializeComponent();           
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;
            recheckFrequency = loadRecheckFrequencyFromFile();
            watchlist = new Watchlist();            
            setBinding();            
            resetTimer(); 
        }

        public void setBinding()
        {
            Binding watchlistBind = new Binding();
            watchlistBind.Source = watchlist;
            watchlistBind.Path = new PropertyPath("Settings");
            watchlistBind.Mode = BindingMode.TwoWay;
            watchlistGui.SetBinding(ItemsControl.ItemsSourceProperty, watchlistBind);
            tbRecheckFrequency.Text = (recheckFrequency/60/60/1000) + "";
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            DialogBoxAddRSSFeed dialogBox = new DialogBoxAddRSSFeed();
            dialogBox.ShowDialog();
        }

        private void btRecheck_Click(object sender, RoutedEventArgs e)
        {
            watchlist.refreshAllKnownFeeds();
        }

        private void bt_removeFeed_Click(object sender, RoutedEventArgs e)
        {
            Button btSender = (Button)sender;
            watchlist.removeRssFeed((string)btSender.Tag);
        }

        private void bt_refreshFeed_Click(object sender, RoutedEventArgs e)
        {
            Button btSender = (Button)sender;
            watchlist.refreshRssFeed((string)btSender.Tag);
        }

        private void tbRecheckFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.IsLoaded)
            {
                try
                {
                    recheckFrequency = Convert.ToInt32(tbRecheckFrequency.Text) * 60 * 60 * 1000;
                    saveRecheckFrequencyToFile();
                    resetTimer();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void resetTimer()
        {
            if(timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
            DateTime lastRefresh = findEarliestRefreshDateTime();            
            refreshTime = lastRefresh.AddMilliseconds(recheckFrequency);
            tbNextRefreshAt.Text = "next refresh at: " + refreshTime;
            timer = new Timer();
            //timer.Interval = 1000 * 60 * 15; //once every 15 minutes
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            
        }
        
        private DateTime findEarliestRefreshDateTime()
        {
            DateTime earliestDateTime = DateTime.Now;
            foreach(var rssSetting in watchlist.Settings)
            {
                try
                {
                    DateTime lastUpdate = DateTime.Parse(rssSetting.Timestamp);
                    if (earliestDateTime > lastUpdate)
                    {
                        earliestDateTime = lastUpdate;
                    }
                }
                catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                
            }
            return earliestDateTime;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now > refreshTime)
            {
                Console.WriteLine("Refreshing all known feeds");
                refreshTime = DateTime.Now.AddMilliseconds(loadRecheckFrequencyFromFile());
                watchlist.refreshAllKnownFeeds();
                //TODO figure out the timers.
            }
        }

        private int loadRecheckFrequencyFromFile()
        {
            if (File.Exists(_autoRefreshFrequencyFilepath))
            {
                string json = File.ReadAllText(_autoRefreshFrequencyFilepath);
                return JsonConvert.DeserializeObject<int>(json);
            }
            else
            {
                return 24 * 60 * 60 * 1000;
            }
        }

        private void saveRecheckFrequencyToFile()
        {
            string json = JsonConvert.SerializeObject(recheckFrequency, Formatting.Indented);
            File.WriteAllLines(_autoRefreshFrequencyFilepath, new String[] { json }, Encoding.UTF8);
        }        
    }
}
