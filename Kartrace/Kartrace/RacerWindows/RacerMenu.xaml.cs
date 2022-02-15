using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kartrace.RacerWindows
{
    /// <summary>
    /// Логика взаимодействия для RacerMenu.xaml
    /// </summary>
    public partial class RacerMenu : Window
    {
        string EMAIL;
        string PASSWORD;
        public RacerMenu(string login, string password)
        {
            InitializeComponent();
            EMAIL = login;
            PASSWORD = password;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RegistrationToRace(EMAIL,PASSWORD);
            this.Hide();
            w.Show();
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RacerChangeData(EMAIL, PASSWORD);
            this.Hide();
            w.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Hide();
            window.Show();
        }
    }
}
