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
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {

        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        public AddUsers()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            if (I.Text.Length <1 || F.Text.Length<1 || Role.SelectedValue==null || Password.Text.Length<1) 
            { MessageBox.Show("Не все данные заполнены"); connect.Close(); return; }
            if (Password.Text.Length < 5) { MessageBox.Show("Длина пароля хотя бы 5 символов"); connect.Close(); return; }
            if (Password.Text != Repeat.Text) { MessageBox.Show("Пароли не совпадают"); return; }
            if (!TimeRemaining.Emailvalid(Email.Text))
            {
                connect.Close();
                MessageBox.Show("Неправильная почта"); connect.Close(); return;
            }
            SqlCommand findIDEmploy = new SqlCommand($@"select * from [User] where Email='{Email.Text}' or Password='{Password.Text}'", connect);
            SqlDataReader readerEmploy = findIDEmploy.ExecuteReader();
            if (readerEmploy.HasRows) // если есть данный сотрудник 
            {
                connect.Close();
                MessageBox.Show("Введите другую почту или пароль"); connect.Close();
                return;
            }
            SqlCommand add1 = new SqlCommand($@"insert into [User](Email, Password, First_Name, Last_Name, ID_Role) values('{Email.Text}','{Password.Text}', '{I.Text}','{F.Text}','{Role.SelectedValue}')", connect);
            add1.ExecuteNonQuery();
            MessageBox.Show("Пользователь зарегистрирован");
            connect.Close();
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
            connect.Close();
        }

        private void Chanel_Click(object sender, RoutedEventArgs e)
        {
            Window w = new UsersChoise();
            Hide();
            w.Show();
        }
    }
}
