using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SlotMachineMultithreaded
{
    public partial class MainWindow : Window
    {
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        BackgroundWorker backgroundWorker2 = new BackgroundWorker();
        BackgroundWorker backgroundWorker3 = new BackgroundWorker();

        SolidColorBrush myGreenBrush = new SolidColorBrush(Color.FromRgb(208, 255, 214));
        SolidColorBrush myRedBrush = new SolidColorBrush(Color.FromRgb(211, 63, 73));
        SolidColorBrush myBlackBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        int winCounter = 1;
        
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
            lblSlot_1.Content = e.ProgressPercentage;
        }
        private void BgWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblSlot_2.Content = e.ProgressPercentage;
        }
        private void BgWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblSlot_3.Content = e.ProgressPercentage;
        }

        private void BgWorker_Work1(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                backgroundWorker1.ReportProgress(Random_Num_Generate());
                Thread.Sleep(100);

                if (backgroundWorker1.CancellationPending)
                {
                    break;
                }
            }
        }

        private void BgWorker_Work2(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                backgroundWorker2.ReportProgress(Random_Num_Generate());
                Thread.Sleep(100);

                if (backgroundWorker2.CancellationPending)
                {
                    break;
                }
            }
        }

        private void BgWorker_Work3(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                backgroundWorker3.ReportProgress(Random_Num_Generate());
                Thread.Sleep(100);

                if (backgroundWorker3.CancellationPending)
                {
                    break;
                }
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (backgroundWorker1.IsBusy || backgroundWorker2.IsBusy || backgroundWorker3.IsBusy)
            {
                CancelAllWorkers();
            }
            else
            {
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                    btn1.Content = "Stop";
                    btn1.Foreground = myRedBrush;
                    btn1.FontWeight = FontWeights.Heavy;
                }
                else
                {
                    CancelAllWorkers();
                }

                if (!backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.RunWorkerAsync();
                    btn2.Content = "Stop";
                    btn2.Foreground = myRedBrush;
                    btn2.FontWeight = FontWeights.Heavy;
                }
                else
                {
                    CancelAllWorkers();
                }

                if (!backgroundWorker3.IsBusy)
                {
                    backgroundWorker3.RunWorkerAsync();
                    btn3.Content = "Stop";
                    btn3.Foreground = myRedBrush;
                    btn3.FontWeight = FontWeights.Heavy;
                }
                else
                {
                    CancelAllWorkers();
                }
                btnPlay.Content = "Stop";
                btnPlay.Foreground = myRedBrush;
                btnPlay.FontWeight = FontWeights.Heavy;
                //btnPlay.Background = myRedBrush;
            }
        }

        private void CancelAllWorkers()
        {
            //btnPlay.Background = btn1.Background = btn2.Background = btn3.Background = myGreenBrush;
            btnPlay.Content = "Play";
            btnPlay.Foreground = myBlackBrush;
            btnPlay.FontWeight = FontWeights.Medium;

            backgroundWorker1.CancelAsync();
            btn1.Content = "Hit";
            btn1.Foreground = myBlackBrush;
            btn1.FontWeight = FontWeights.Medium;

            backgroundWorker2.CancelAsync();
            btn2.Content = "Hit";
            btn2.Foreground = myBlackBrush;
            btn2.FontWeight = FontWeights.Medium;

            backgroundWorker3.CancelAsync();
            btn3.Content = "Hit";
            btn3.Foreground = myBlackBrush;
            btn3.FontWeight = FontWeights.Medium;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();

                btnPlay.Content = "Stop";
                btn1.Content = "Stop";
                btnPlay.Foreground = btn1.Foreground = myRedBrush;
                btnPlay.FontWeight = btn1.FontWeight = FontWeights.Heavy;
            }
            else
            {
                backgroundWorker1.CancelAsync();
                if (!backgroundWorker2.IsBusy && !backgroundWorker3.IsBusy)
                {
                    btnPlay.Content = "Play"; 
                    btnPlay.Foreground = myBlackBrush;
                    btnPlay.FontWeight = FontWeights.Medium;
                }
                btn1.Content = "Hit";
                btn1.Foreground = myBlackBrush;
                btn1.FontWeight = FontWeights.Medium;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();

                btnPlay.Content = "Stop";
                btn2.Content = "Stop";
                btnPlay.Foreground = btn2.Foreground = myRedBrush;
                btnPlay.FontWeight = btn2.FontWeight = FontWeights.Heavy;
            }
            else
            {
                backgroundWorker2.CancelAsync();
                if (!backgroundWorker1.IsBusy && !backgroundWorker3.IsBusy)
                {
                    btnPlay.Content = "Play";
                    btnPlay.Foreground = myBlackBrush;
                    btnPlay.FontWeight = FontWeights.Medium;
                }
                btn2.Content = "Hit";
                btn2.Foreground = myBlackBrush;
                btn2.FontWeight = FontWeights.Medium;
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker3.IsBusy)
            {
                backgroundWorker3.RunWorkerAsync();

                btnPlay.Content = "Stop";
                btn3.Content = "Stop";
                btnPlay.Foreground = btn3.Foreground = myRedBrush;
                btnPlay.FontWeight = btn3.FontWeight = FontWeights.Heavy;
            }
            else
            {
                backgroundWorker3.CancelAsync();
                if (!backgroundWorker1.IsBusy && !backgroundWorker2.IsBusy)
                {
                    btnPlay.Content = "Play";
                    btnPlay.Foreground = myBlackBrush;
                    btnPlay.FontWeight = FontWeights.Medium;
                }
                btn3.Content = "Hit";
                btn3.Foreground = myBlackBrush;
                btn3.FontWeight = FontWeights.Medium;
            }
        }

        private int Random_Num_Generate()
        {
            return new Random().Next(0, 9);
        }

        private void btnCashIn_Click(object sender, RoutedEventArgs e)
        {
            if (lblSlot_1.Content.ToString() == lblSlot_2.Content.ToString() && lblSlot_2.Content.ToString() == lblSlot_3.Content.ToString())
            {
                lblWins.Content = "Wins: " + winCounter++.ToString();
                MessageBox.Show("Winner...");
            }
            else
            {
                MessageBox.Show("Try again");
            }
        }
    }
}
