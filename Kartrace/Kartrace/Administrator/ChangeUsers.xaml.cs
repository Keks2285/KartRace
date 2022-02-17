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

namespace Kartrace.Administrator
{
    /// <summary>
    /// Логика взаимодействия для ChangeUsers.xaml
    /// </summary>
    public partial class ChangeUsers : Window
    {
        string email="";
        string password = "";
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        public ChangeUsers()
        {
            InitializeComponent();
        }

        private void Win_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("select ID_Role, Role_Name from Role", connect);
            DataTable data = new DataTable();
            data.Load(command.ExecuteReader());
            Role.ItemsSource = data.DefaultView;
            Role.DisplayMemberPath = "Role_Name";
            Role.SelectedValuePath = "ID_Role";
            SqlCommand command2 = new SqlCommand("select Email, Password, concat(First_Name,' ', Last_Name) as 'Сотрудник', ID_Role from [User]", connect);
            DataTable data2 = new DataTable();
            data2.Load(command2.ExecuteReader());
            User.ItemsSource = data2.DefaultView;
            User.DisplayMemberPath = "Сотрудник";
            User.SelectedValuePath = "Email";
            connect.Close();
        }

        private void User_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connect.Open();

            SqlCommand findIDEmploy = new SqlCommand($@"select * from [User] where Email='{User.SelectedValue}'", connect);
            SqlDataReader reader = findIDEmploy.ExecuteReader();
            reader.Read();
            I.Text = reader["First_Name"].ToString();
            F.Text = reader["Last_Name"].ToString();
            Email.Text = reader["Email"].ToString();
            password = reader["Password"].ToString();
            reader.Close();
            connect.Close();
        }

        private void Chanel_Click(object sender, RoutedEventArgs e)
        {
            Window w = new UsersChoise();
            Hide();
            w.Show();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();

            if (I.Text.Length < 1 || F.Text.Length < 1 || Role.SelectedValue == null || User.SelectedValue == null || Password.Text.Length < 1)
            {MessageBox.Show("Не все данные заполнены"); connect.Close(); return; }
            if (!TimeRemaining.Emailvalid(Email.Text))
            {
                connect.Close();
                MessageBox.Show("Неправильная почта"); connect.Close(); return;
            }
            if (Password.Text.Length < 5) { MessageBox.Show("Длина пароля хотя бы 5 символов"); connect.Close(); return; }
            if (Password.Text != Repeat.Text) { MessageBox.Show("Пароли не совпадают"); connect.Close(); return; }

            SqlCommand find = new SqlCommand($@"select * from [User] where Email='{Email.Text}' and Email!='{User.SelectedValue}'", connect);
            SqlDataReader readerMatch = find.ExecuteReader();
            if (readerMatch.HasRows) // если есть данный сотрудник 
            {
                connect.Close();
                MessageBox.Show("Введите другую почту");
                return;
            }
            readerMatch.Close();

            SqlCommand add = new SqlCommand($@"update [User] Set First_Name='{I.Text}', Last_Name='{F.Text}',Email='{Email.Text}', Password='{Password.Text}', ID_Role='{Role.SelectedValue}' where Email='{User.SelectedValue}'", connect);
            add.ExecuteReader();
            MessageBox.Show("Данные успешно изменены");
            connect.Close();
        }
    }
}
