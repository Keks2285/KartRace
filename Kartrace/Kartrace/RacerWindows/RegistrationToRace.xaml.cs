using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для RegistrationToRace.xaml
    /// </summary>
    public partial class RegistrationToRace : Window
    {
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        public RegistrationToRace()
        {
            InitializeComponent();
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("select ID_Сharity, Charity_Name  from Charity", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            Organization.ItemsSource = datatbl.DefaultView;
            Organization.SelectedValuePath = "ID_Сharity";
            Organization.DisplayMemberPath = "Charity_Name";
            connect.Close();
        }

        private void ChBox25_Checked(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() + 25;
            Price.Content = otv + "$";
        }

        private void ChBox25_Unchecked(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() - 25;
            Price.Content = otv + "$";
        }

        private void ChBox40_Checked(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() + 40;
            Price.Content = otv + "$";
        }

        private void ChBox65_Checked(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() + 65;
            Price.Content = otv + "$";
        }

        private void ChBox65_Unchecked(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() - 65;
            Price.Content = otv + "$";
        }

        private void ChBox40_Unchecked(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() - 40;
            Price.Content = otv + "$";
        }
        public int Scan_price()
        {
            return Convert.ToInt32(Price.Content.ToString().Substring(0, Price.Content.ToString().Length-1));
        }

      

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() + 30;
            Price.Content = otv + "$";
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() + 50;
            Price.Content = otv + "$";
        }

       

        private void RadioButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() - 30;
            Price.Content = otv + "$";
        }

        private void RadioButton_Unchecked_2(object sender, RoutedEventArgs e)
        {
            int otv = Scan_price() - 50;
            Price.Content = otv + "$";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Organization.SelectedItem != null && (ChBox25.IsChecked == true || ChBox40.IsChecked == true || ChBox65.IsChecked == true)
             && (RB0.IsChecked == true || RB30.IsChecked == true || RB50.IsChecked == true) && Donate.Text.Length > 0)
            {
                if (Donate.Text[0]=='.')  { MessageBox.Show("Введите корректную сумму пожертвования"); return; }
                foreach (char i in Donate.Text){if (!char.IsDigit(i) || i!='.') { MessageBox.Show("Введите корректную сумму пожертвования"); return; }}
                double don = Convert.ToDouble(Donate.Text);
                if (don<0) { MessageBox.Show("Пожертвование не может быть меньше 0"); return; }
                // тут просто добавить в бд registration потом в созданную таблицу для шлемов и прочей лабуды доделать


            }
            else { MessageBox.Show("Не все поля запоолнены!"); }
        }
    }
}
