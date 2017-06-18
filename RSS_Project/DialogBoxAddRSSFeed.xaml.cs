using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RSS
{
    /// <summary>
    /// Interaction logic for DialogBoxAddRSSFeed.xaml
    /// </summary>
    public partial class DialogBoxAddRSSFeed : Window
    {
        public DialogBoxAddRSSFeed()
        {
            InitializeComponent();
        }
       
        private void btValidateAndAddNewRssFeed_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbUrl.Text)) {
                System.Windows.Forms.MessageBox.Show("URL Error:\nPlease enter url");
            } else if (String.IsNullOrEmpty(tbFolderPath.Text)) {
                System.Windows.Forms.MessageBox.Show("FOLDER ERROR:\nPlease select folder");
            } else if (String.IsNullOrEmpty(tbTitle.Text)) {
                System.Windows.Forms.MessageBox.Show("TITLE ERROR:\nPlease specify title");
            } else {
                MainWindow window = (MainWindow)System.Windows.Application.Current.MainWindow;
                window.watchlist.addRssFeed(tbUrl.Text, tbTitle.Text, tbFolderPath.Text);
                this.Close();
            }
        }

        private void btOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (!String.IsNullOrEmpty(fbd.SelectedPath))
                {
                    tbFolderPath.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
