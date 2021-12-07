using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using System.Diagnostics;

namespace SlotMachineMultithreaded
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        BackgroundWorker backgroundWorker2 = new BackgroundWorker();
        BackgroundWorker backgroundWorker3 = new BackgroundWorker();

        int num = 0;

        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker1.DoWork += BgWorker_Work1;
            backgroundWorker2.DoWork += BgWorker_Work2;
            backgroundWorker3.DoWork += BgWorker_Work3;

            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;

            backgroundWorker1.ProgressChanged += BgWorker1_ProgressChanged;
            backgroundWorker2.ProgressChanged += BgWorker2_ProgressChanged;
            backgroundWorker3.ProgressChanged += BgWorker3_ProgressChanged;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker3.WorkerReportsProgress = true;
        }

        private void BgWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lblSlot_1.Content = e.ProgressPercentage;
        }
        private void BgWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lblSlot_2.Content = e.ProgressPercentage;
        }
         private void BgWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lblSlot_3.Content = e.ProgressPercentage;
        }

        private void BgWorker_Work1(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                backgroundWorker1.ReportProgress(Random_Num_Generate());
                Thread.Sleep(100);

                if (backgroundWorker1.CancellationPending)
                {
                    //backgroundWorker.Dispose();
                    break;
                }
            }
        }

        private void BgWorker_Work2(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                backgroundWorker2.ReportProgress(Random_Num_Generate());
                System.Threading.Thread.Sleep(100);

                if (backgroundWorker2.CancellationPending)
                {
                    //backgroundWorker.Dispose();
                    break;
                }
            }
        }

        private void BgWorker_Work3(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                backgroundWorker3.ReportProgress(Random_Num_Generate());
                System.Threading.Thread.Sleep(100);

                if (backgroundWorker3.CancellationPending)
                {
                    //backgroundWorker.Dispose();
                    break;
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy || !backgroundWorker2.IsBusy || !backgroundWorker3.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.RunWorkerAsync();
                backgroundWorker3.RunWorkerAsync();

                btnStart.Content = "Stop";
            }
            else
            {
                backgroundWorker1.CancelAsync();
                backgroundWorker2.CancelAsync();
                backgroundWorker3.CancelAsync();
                btnStart.Content = "Start";
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();

                btnStart.Content = "Stop";
                btn1.Content = "Stop";
                //btn1.Background == Brushes.Red;
            }
            else
            {
                backgroundWorker1.CancelAsync();
                btnStart.Content = "Start";
                btn1.Content = "Hit";
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();

                btnStart.Content = "Stop";
                btn2.Content = "Stop";
                //btn1.Background == Brushes.Red;
            }
            else
            {
                backgroundWorker2.CancelAsync();
                btnStart.Content = "Start";
                btn2.Content = "Hit";
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker3.IsBusy)
            {
                backgroundWorker3.RunWorkerAsync();

                btnStart.Content = "Stop";
                btn3.Content = "Stop";
                //btn1.Background == Brushes.Red;
            }
            else
            {
                backgroundWorker3.CancelAsync();
                btnStart.Content = "Start";
                btn3.Content = "Hit";
            }
        }

        private int Random_Num_Generate()
        {
            return new Random().Next(0, 9);
        }
    }
}
