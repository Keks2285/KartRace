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
    /// Логика взаимодействия для ThanksRacer.xaml
    /// </summary>
    public partial class ThanksRacer : Window
    {
        string EMAIL = "";
        string PASSWORD = "";
        public ThanksRacer(string email, string password)
        {
            EMAIL = email;
            PASSWORD = password;
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RacerMenu(EMAIL, PASSWORD);
            this.Hide();
            w.Show();
        }
    }
}
