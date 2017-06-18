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
        private string _folder = "";
        private string _title;
        private string _status;
        private string _timestamp;
        private int _backlogSize;

        public string url { get { return _url; } set { _url = value; NotifyPropertyChanged("url"); } }        
        public string folder { get { return _folder; } set { _folder = value; NotifyPropertyChanged("folder"); } }        
        public string title { get { return _title; } set { _title = value; NotifyPropertyChanged("title"); } }        
        public string status { get { if (_status == null) { return "unknown status"; } else return _status; } set { _status = value; NotifyPropertyChanged("status"); } }        
        public string timestamp { get { return _timestamp; } set { _timestamp = value; NotifyPropertyChanged("timestamp"); } }        
        public int backlogSize { get { return _backlogSize; } set { _backlogSize = value; NotifyPropertyChanged("backlogSize"); } }

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
