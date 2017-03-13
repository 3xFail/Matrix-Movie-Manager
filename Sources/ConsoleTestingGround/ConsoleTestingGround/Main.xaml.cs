using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Xml;
using Multiscreen_Progressbar;

namespace ConsoleTestingGround
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bw = new BackgroundWorker();
        Popup pop = new Popup();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            XMLNodeInsertForThread1();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Progress Bar Window close
            pop.Close();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pop.prgTest.Value = e.ProgressPercentage;
        }
        public void XMLNodeInsertForThread1()
        {
            string FileName = System.IO.Directory.GetCurrentDirectory() + "\\CodeScratcherThread1.xml";
            for (int i = 1; i <= 1000; i++)
            {
                System.Xml.XmlDocument myDoc = new System.Xml.XmlDocument();
                myDoc.Load(FileName);
                XmlNode node = myDoc.SelectSingleNode("/CodescratcherNode");
                XmlElement Elements = default(XmlElement);
                Elements = myDoc.CreateElement("Codescratcher");
                var _with1 = Elements;
                _with1.SetAttribute("CodescratcherOutput", "Codescratcher XML Node" + i);
                node.AppendChild(Elements);
                System.Xml.XmlTextWriter myWriter = new System.Xml.XmlTextWriter(FileName, System.Text.Encoding.UTF8);
                myWriter.Formatting = System.Xml.Formatting.Indented;
                myDoc.Save(myWriter);
                myWriter.Close();
                bw.ReportProgress(i / 10);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Background Worker code///
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();

            //Progress Bar Window
            pop.Show();
        }
        private void My_Movies_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate();
        }

        private void Do_Have_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate();
        }

        private void Search_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate();
        }

        private void Suggest_Butt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Butt_Click(object sender, RoutedEventArgs e)
        {
            Main_Frame.Navigate(new SettingsPage(this));
        }
    }
}