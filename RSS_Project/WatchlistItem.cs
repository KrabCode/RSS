using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RSS
{
    /*
        watchlist in JSON format:
              [
                  {
                    "url": "http://www2.rozhlas.cz/podcast/podcast_porady.php?p_po=101033",
                    "folder": "C:\\Users\\Jakub\\Desktop\\Historie+++",
                    "title": "Historie +",
                    "Status": "up to date"
                  },
               ...
              ]
        */

    public class WatchlistItem : INotifyPropertyChanged
    {
        private string _url = "";
        public string url { get { return _url; } set { _url = value; NotifyPropertyChanged("url"); } }
        private string _folder = "";
        public string folder { get { return _folder; } set { _folder = value; NotifyPropertyChanged("folder"); } }
        /*
        private string[] _knownFiles = new string[] { };
        public string[] knownFiles { get { return _knownFiles; } set { _knownFiles = value; NotifyPropertyChanged("knownFiles"); } }*/
        private string _title;
        public string title { get { return _title; } set { _title = value; NotifyPropertyChanged("title"); } }
        private string _status;
        public string Status { get { if (_status == null) { return "unknown status, please recheck"; } else return _status; } set { _status = value; NotifyPropertyChanged("Status"); } }
        private string _timestamp;
        public string Timestamp { get { return _timestamp; } set { _timestamp = value; NotifyPropertyChanged("Timestamp"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    
}
