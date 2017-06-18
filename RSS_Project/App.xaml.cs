using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace RSS
{
    

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            string procName = Process.GetCurrentProcess().ProcessName;

            // get the list of all processes by the "procName"       
            Process[] processes = Process.GetProcessesByName(procName);

            if (processes.Length > 1)
            {
                MessageBox.Show(procName + " already running");
                return;
            }
            else
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
            }
            
        }
    }
}
