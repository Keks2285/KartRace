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
        public RacerMenu()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RegistrationToRace();
            this.Hide();
            w.Show();
        }
    }
}
