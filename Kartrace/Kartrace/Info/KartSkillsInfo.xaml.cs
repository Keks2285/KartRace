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
    /// Логика взаимодействия для KartSkillsInfo.xaml
    /// </summary>
    public partial class KartSkillsInfo : Window
    {
        public KartSkillsInfo()
        {
            InitializeComponent();
        }

        private void Map_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Map();
            Hide();
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new InfoMenu();
            Hide();
            window.Show();
        }
    }
}
