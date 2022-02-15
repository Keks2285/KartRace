using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для RacerChangeData.xaml
    /// </summary>
    public partial class RacerChangeData : Window
    {
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        public RacerChangeData(string Login, string Password)
        {
            InitializeComponent();
            EMAIL = Login;
            PASSWORD = Password;
        }
        string fullfilenamae = "";
        string PhotoFile = "";
        string CurrentPhoto = "";
        string Name = "";
        string Familia="";
        string EMAIL="";
        string PASSWORD = "";
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();

            if (Email.Text != "" && Password.Text != "" && RepeatPassword.Text.Equals(Password.Text) && Gender.SelectedValue != null && I.Text.Length > 0 && F.Text.Length > 0 && Country.SelectedValue != null && NameFile.Content != "" && DateBirth != null)
            {
                try
                {
                    DateTime date = DateTime.Parse(DateBirth.Text);
                    TimeSpan years = DateTime.Today.Subtract(date);
                    if (years.TotalDays/365<18) { MessageBox.Show("Гонщик младше 18-ти"); return; }
                }
                catch { MessageBox.Show("Введены некорректные данные"); return; }
                if (!TimeRemaining.Emailvalid(Email.Text))
                {
                    connect.Close();
                    MessageBox.Show("Неправильная почта"); return;
                }
                
                SqlCommand find = new SqlCommand($@"select * from [User] where Email='{Email.Text}' and Password='{Password.Text}' and First_Name!='{Name}' and Last_Name!='{Familia}' ", connect);
                SqlDataReader readerMatch = find.ExecuteReader();
                if (readerMatch.HasRows) // если есть данный сотрудник 
                {
                    connect.Close();
                    MessageBox.Show("Введите другую почту или пароль");
                    return;
                }               
                readerMatch.Close();
                //try
                //{

                    SqlCommand add = new SqlCommand($@"update [User] Set First_Name='{I.Text}', Last_Name='{F.Text}',Email='{Email.Text}', Password='{Password.Text}' where First_Name='{Name}' and Last_Name='{Familia}'", connect);
                    SqlCommand add1 = new SqlCommand($@"update Racer Set First_Name='{I.Text}', Last_Name='{F.Text}',Gender='{Gender.SelectedValue}', ID_Country='{Country.SelectedValue}', Photo='{NameFile.Content}' where First_Name='{Name}' and Last_Name='{Familia}' ", connect);
                    // SqlCommand add1 = new SqlCommand($@"INSERT [dbo].[Racer] ( [First_Name], [Last_Name], [Gender], [DateOfBirth], [ID_Country], [Photo]) VALUES ( N'{Firsname.Text}', N'{LastName.Text}', N'{Gender.SelectedValue}', N'{DateBirth.Text}', N'{Country.SelectedValue}', N'{PhotoFile}')", connect);
                      add.ExecuteNonQuery();
                      add1.ExecuteNonQuery();
                    if (CurrentPhoto != null) { 
                     File.Delete(Directory.GetCurrentDirectory() + $@"\RacersImages\{CurrentPhoto}" );
                    }  
                    File.Copy(fullfilenamae, Directory.GetCurrentDirectory() + $@"\RacersImages\{PhotoFile}");
                    
                //}
                //catch { MessageBox.Show("Ведены неправильные данные"); }               
            }
            connect.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fullfilenamae = openFileDialog.FileName;
                PhotoFile = System.IO.Path.GetFileName(fullfilenamae);
                PhotO.Source = new BitmapImage(new Uri(fullfilenamae));              
                NameFile.Content = PhotoFile;
            }
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();

            SqlCommand findIDEmploy = new SqlCommand($@"select * from [User] where Email='{EMAIL}' and Password='{PASSWORD}'", connect);
            SqlDataReader reader = findIDEmploy.ExecuteReader();
            reader.Read();
            Name = reader["First_Name"].ToString();
            Familia = reader["Last_Name"].ToString();
            
            I.Text = Name;
            F.Text = Familia;
            Email.Text= reader["Email"].ToString();
           


            reader.Close();
            SqlCommand find = new SqlCommand($@"select * from Racer where First_Name='{Name}' and Last_Name='{Familia}'", connect);
            SqlDataReader readerRacer = find.ExecuteReader();
            readerRacer.Read();
            DateBirth.Text = readerRacer["DateOfBirth"].ToString();
            CurrentPhoto = readerRacer["Photo"].ToString();
            if (readerRacer["Photo"].ToString() != "")
            {
                NameFile.Content = readerRacer["Photo"].ToString();
                PhotO.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + $@"\RacersImages\{NameFile.Content}"));
            }
            readerRacer.Close();



             SqlCommand commandEmployee = new SqlCommand("select ID_Gender,Gender_Name  from Gender", connect);
            DataTable datatblEmployee = new DataTable();
            datatblEmployee.Load(commandEmployee.ExecuteReader());
            Gender.ItemsSource = datatblEmployee.DefaultView;
            Gender.SelectedValuePath = "ID_Gender";
            Gender.DisplayMemberPath = "Gender_Name";

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
            Window w = new RacerMenu(EMAIL, PASSWORD);
            this.Hide();
            w.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RacerMenu(EMAIL,PASSWORD);
            this.Hide();
            w.Show();
        }
    }
}
