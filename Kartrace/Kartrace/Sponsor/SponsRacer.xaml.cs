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

namespace Kartrace.Sponsor
{
    /// <summary>
    /// Логика взаимодействия для SponsRacer.xaml
    /// </summary>
    public partial class SponsRacer : Window
    {
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        string IDCharity = "";
        int IDRacer = 0;
        double don = 0;
        string fio = "";
        public SponsRacer()
        {
            InitializeComponent();
        }

        private void Ammount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TotalSumma.Content = $@"${Ammount.Text}";
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            if (Racer.SelectedValue == null || YourName.Text.Length<1 || Owner.Text.Length<1 || NumberCard.Text.Length!=16 || Month.Text.Length!=2 || Year.Text.Length != 4) { MessageBox.Show("Не все данные заполнены"); connect.Close(); return; }
            foreach (char a in NumberCard.Text) {if (!char.IsDigit(a)) { MessageBox.Show("Неккоректный номер карты"); connect.Close(); return; }}
            foreach (char a in Month.Text) { if (!char.IsDigit(a)) { MessageBox.Show("Неккоректный cрок действия"); connect.Close(); return; } }
            foreach (char a in Year.Text) { if (!char.IsDigit(a)) { MessageBox.Show("Неккоректный cрок действия"); connect.Close(); return; } }
            foreach (char a in CVC.Text) { if (!char.IsDigit(a)) { MessageBox.Show("Неккоректный CVC"); connect.Close(); return; } }
            if (Month.Text[0]!='0') 
            { 
                if (Convert.ToInt32(Month.Text) > 12) { MessageBox.Show("Неккоректный cрок действия"); connect.Close(); return; } 
            }
            try { Convert.ToInt32(Year.Text); }
            catch { connect.Close(); MessageBox.Show("Неккоректный cрок действия"); connect.Close(); return;}
            DateTime date=new DateTime();
            date.AddYears(Convert.ToInt32(Year.Text));
            date.AddMonths(Convert.ToInt32(Month.Text));
            if (DateTime.Compare(date, DateTime.Today) >0) { MessageBox.Show("Срок действия карты истек"); connect.Close(); return; }
            SqlCommand command = new SqlCommand($@"insert into Sponsorship (SponsorName, Amount, Id_Racer) values('{YourName.Text}','{Ammount.Text}',{Racer.SelectedValue})", connect) ;
            command.ExecuteNonQuery();
            connect.Close();
            Window window = new ThanksSponsor(fio, NameCharity.Content.ToString(), Ammount.Text);
            this.Hide();
            window.Show();
        }

        private void Chanel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Hide();
            window.Show();
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("select ID_Racer, concat(First_Name,' ' ,Last_Name,' ',ID_Country) as 'Гонщик'  from Racer", connect);

            //SqlDataReader read = command.ExecuteReader();
            //read.Read();
            //read.Close();
            DataTable data = new DataTable();
            data.Load(command.ExecuteReader());
            Racer.ItemsSource = data.DefaultView;
            Racer.DisplayMemberPath = "Гонщик";
            Racer.SelectedValuePath = "ID_Racer";
            connect.Close();
        }

        private void Racer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand($@"select concat(First_Name,' ' ,Last_Name,' ',ID_Country) as 'ФИО' from Racer where ID_Racer={Racer.SelectedValue}", connect);
            SqlDataReader read = command.ExecuteReader();
            read.Read();
            fio = read["ФИО"].ToString();
            read.Close();
            SqlCommand commandRegistration = new SqlCommand($@"select ID_Racer, ID_Charity from Registration where ID_Racer={Racer.SelectedValue}", connect);
            SqlDataReader readRegistration= commandRegistration.ExecuteReader();
            readRegistration.Read();
            
            IDCharity = readRegistration["ID_Charity"].ToString();
            if (readRegistration.HasRows)
            {
                readRegistration.Close();
                SqlCommand commandCharity = new SqlCommand($@"select Charity_Name from Charity where ID_Сharity={IDCharity}", connect);
                SqlDataReader readCharity = commandCharity.ExecuteReader();
                readCharity.Read();
                NameCharity.Content = readCharity["Charity_Name"].ToString();
                readCharity.Close();
            }
            else { readRegistration.Close(); NameCharity.Content = "Нет"; }
            
            connect.Close();
        }

        private void Minus50_Click(object sender, RoutedEventArgs e)
        {
            if (Ammount.Text[0] == ',') { MessageBox.Show("Введите корректную сумму пожертвования"); return; }
            try
            {
                don = Convert.ToDouble(Ammount.Text);
            }
            catch { MessageBox.Show("Введите корректную сумму пожертвования"); return; }
            if(don-50<50) { MessageBox.Show("Сумма не может быть меньше 50"); return; }
            don -= 50;
            Ammount.Text = don.ToString();
        }

        private void Plus50_Click(object sender, RoutedEventArgs e)
        {
            if (Ammount.Text[0] == ',') { MessageBox.Show("Введите корректную сумму пожертвования"); return; }
            try
            {
                don = Convert.ToDouble(Ammount.Text);
            }
            catch { MessageBox.Show("Введите корректную сумму пожертвования"); return; }
            don += 50;
            Ammount.Text = don.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Hide();
            window.Show();
        }
    }
}
