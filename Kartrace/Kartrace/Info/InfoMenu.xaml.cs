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

namespace Kartrace.Info
{
    /// <summary>
    /// Логика взаимодействия для InfoMenu.xaml
    /// </summary>
    public partial class InfoMenu : Window
    {
        public InfoMenu()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window window = new KartSkillsInfo();
            Hide();
            window.Show();            
        }

        private void Back_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CharityList();
            Hide();
            window.Show();
        }
    }
}
