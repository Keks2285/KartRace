using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Kartrace
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool Active = true;
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        Thread t;
        public MainWindow()
        {
            InitializeComponent();
            //tr = new TimeRemaining();
           // tr.Remain(Remaining, IsActive);
            t = new Thread(new ThreadStart(Remain));
            t.Start();
        }

        private void Menu1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Racer.Width = Menu1.Width / 5.5;
            Racer.Height = Menu1.Height / 4;
            Sponsor.Width = Menu1.Width / 5.5;
            Sponsor.Height = Menu1.Height / 4;
            Info.Width = Menu1.Width / 5.5;
            Info.Height = Menu1.Height / 4;
            Enter.Width = Menu1.Width / 5.5;
            Enter.Height = Menu1.Height / 4;
        }

        private void Menu1_Loaded(object sender, RoutedEventArgs e)
        {
            Racer.Width = Menu1.Width / 5.5;
            Racer.Height = Menu1.Height / 4;
            Sponsor.Width = Menu1.Width / 5.5;
            Sponsor.Height = Menu1.Height / 4;
            Info.Width = Menu1.Width / 5.5;
            Info.Height = Menu1.Height / 4;
            Enter.Width = Menu1.Width / 5.5;
            Enter.Height = Menu1.Height / 4;
        }

        private void Menu1_StateChanged(object sender, EventArgs e)
        {
            Racer.Width = Menu1.Width / 5.5;
            Racer.Height = Menu1.Height / 4;
            Sponsor.Width = Menu1.Width / 5.5;
            Sponsor.Height = Menu1.Height / 4;
            Info.Width = Menu1.Width / 5.5;
            Info.Height = Menu1.Height / 4;
            Enter.Width = Menu1.Width / 5.5;
            Enter.Height = Menu1.Height / 4;
        }

        private void Racer_Click(object sender, RoutedEventArgs e)
        {
            connect.Close();
            Window w = new RacerWindows.RacerRegistration();
            t.Abort();
            this.Hide();
            w.Show();
        }
        void Remain()
        {
            while (Active)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                        {
                            DateTime otherDate = new DateTime(2022, 05, 22);
                            TimeSpan remaining = otherDate-DateTime.Now  ;
                            int months = (int)(remaining.TotalDays / 30);
                            int years = months / 12;
                            Remaining.Content = string.Format("До начала события сталось {0} лет, {1} месяцев, {2} дней, {3} часов, {4} минут, {5} секунд",
                            years,months-years*12,remaining.Days-months*30, remaining.Hours, remaining.Minutes, remaining.Seconds);
                    });

                }
                catch { Active = false; }

            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
           // Authorization window = new Authorization();
              Window window = new Authorization();
            this.Hide();
            window.Show();
            t.Abort();
        }
    }
}
