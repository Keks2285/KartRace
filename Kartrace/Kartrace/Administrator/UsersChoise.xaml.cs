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

namespace Kartrace.Administrator
{
    /// <summary>
    /// Логика взаимодействия для UsersChoise.xaml
    /// </summary>
    public partial class UsersChoise : Window
    {
        public UsersChoise()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window w = new AddUsers();
            Hide();
            w.Show();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            Window w = new ChangeUsers();
            Hide();
            w.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window w = new AdminMenu();
            Hide();
            w.Show();
        }
    }
}
