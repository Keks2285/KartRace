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
using System.Windows.Shapes;

namespace Kartrace
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        bool Active = true;
        Thread t;
        public Authorization()
        {
            InitializeComponent();
            t = new Thread(new ThreadStart(Remain));
            t.Start();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Hide();
            window.Show();
        }





        private void Chanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
          
                SqlCommand c = new SqlCommand($@"select ID_Role from [User] where Email='{Email.Text}' and Password='{PasswordText.Password}'", connect);
                SqlDataReader reader = c.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    if (reader["ID_Role"].ToString().Equals("R"))
                    {
                        connect.Close();
                        Window w = new RacerWindows.RacerMenu();
                        t.Abort();
                        this.Hide();
                        w.Show();
                        return;
                    }
                    if (reader["ID_Role"].ToString().Equals("A"))
                    {
                        connect.Close();
                        Window w = new Administrator.AdminMenu();
                        t.Abort();
                        this.Hide();
                        w.Show();
                        return;
                    }
                if (reader["ID_Role"].ToString().Equals("C"))
                {
                    connect.Close();
                    Window w = new Administrator.AdminMenu();
                    t.Abort();
                    this.Hide();
                    w.Show();
                    return;
                }
            }
            
            connect.Close();
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
                        TimeSpan remaining = otherDate - DateTime.Now;
                        int months = (int)(remaining.TotalDays / 30);
                        int years = months / 12;
                        Remaining.Content = string.Format("До начала события сталось {0} лет, {1} месяцев, {2} дней, {3} часов, {4} минут, {5} секунд",
                        years, months - years * 12, remaining.Days - months * 30, remaining.Hours, remaining.Minutes, remaining.Seconds);
                    });

                }
                catch { Active = false; }

            }
        }
    }
}
