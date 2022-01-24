using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace Kartrace.RacerWindows
{
    /// <summary>
    /// Логика взаимодействия для RacerRegistration.xaml
    /// </summary>
    public partial class RacerRegistration : Window
    {
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        public RacerRegistration()
        {
            InitializeComponent();
        }
        string PhotoFile = "";
        string fullfilenamae = "";
        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Look_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fullfilenamae = openFileDialog.FileName;
                PhotoFile = System.IO.Path.GetFileName(fullfilenamae);
                PhotO.Source = new BitmapImage( new Uri(fullfilenamae));
                Photo.Content = PhotoFile;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();

            if (Email.Text != "" && Password.Text != "" && RepeatPassword.Text.Equals(Password.Text) && Gender.SelectedValue != null)
            {
                if (!TimeRemaining.Emailvalid(Email.Text))
                {
                    connect.Close();
                    MessageBox.Show("Неправильная почта"); return;
                }
                SqlCommand findIDEmploy = new SqlCommand($@"select * from [User] where Email='{Email.Text}' or Password='{Password.Text}'", connect);
                SqlDataReader readerEmploy = findIDEmploy.ExecuteReader();
                if (readerEmploy.HasRows) // если есть данный сотрудник 
                {
                    connect.Close();
                    MessageBox.Show("Введите другую почту или пароль");
                    return;
                }
                readerEmploy.Close();

                 try
                {

                SqlCommand add = new SqlCommand($@"insert into [User] (Email, [Password], First_Name, Last_Name, ID_Role) values (N'{Email.Text}',N'{Password.Text}',	N'{Firsname.Text}',	N'{LastName.Text}',	N'R')", connect);
                SqlCommand add1 = new SqlCommand($@"INSERT [dbo].[Racer] ( [First_Name], [Last_Name], [Gender], [DateOfBirth], [ID_Country], [Photo]) VALUES ( N'{Firsname.Text}', N'{LastName.Text}', N'{Gender.SelectedValue}', N'{DateBirth.Text}', N'{Country.SelectedValue}', N'{PhotoFile}')", connect);
                add.ExecuteNonQuery();
                add1.ExecuteNonQuery();
                    File.Copy(fullfilenamae, Directory.GetCurrentDirectory()+$@"\RacersImages\{PhotoFile}");
                  }
                  catch { MessageBox.Show("Ведены неправильные данные"); }
                finally {
                connect.Close();
                 }
            }
            else { connect.Close(); }
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            connect.Open();
            SqlCommand commandEmployee = new SqlCommand("select ID_Gender,Gender_Name  from Gender", connect);
            DataTable datatblEmployee = new DataTable();
            datatblEmployee.Load(commandEmployee.ExecuteReader());
            Gender.ItemsSource = datatblEmployee.DefaultView;
            Gender.SelectedValuePath = "ID_Gender";
            Gender.DisplayMemberPath= "Gender_Name";

            SqlCommand command = new SqlCommand("select ID_Country,Country_Name  from Country", connect);
            DataTable data = new DataTable();
            data.Load(command.ExecuteReader());
            Country.ItemsSource = data.DefaultView;
            Country.SelectedValuePath = "ID_Country";
            Country.DisplayMemberPath = "Country_Name";
            connect.Close();
        }

        private void Chanel_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RacerMenu();
            this.Hide();
            w.Show();
        }
    }
}
