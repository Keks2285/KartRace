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

namespace Kartrace.Sponsor
{
    /// <summary>
    /// Логика взаимодействия для ThanksSponsor.xaml
    /// </summary>
    public partial class ThanksSponsor : Window
    {
        public ThanksSponsor(string FIO, string FOND, string COST)
        {
            InitializeComponent();
            fio.Content = FIO;
            fond.Content = FOND;
            cost.Content = COST;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window w = new MainWindow();
            Hide();
            w.Show();
        }
    }
}
