using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Kartrace.Info
{
    /// <summary>
    /// Логика взаимодействия для CharityList.xaml
    /// </summary>
    public partial class CharityList : Window
    {
        private MainViewModel MainViewModel { get; set; } = new MainViewModel();
        public CharityList()
        {
            InitializeComponent();                  
           DataContext = MainViewModel;
        }
    }
    public class ImageViewModel
    {
        public ImageViewModel(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class MainViewModel
    {
        public static SqlConnection connect = new SqlConnection("Data Source=KEKS\\SQLEXPRESS;Initial Catalog=Karting;Integrated Security=True");
        public ObservableCollection<ImageViewModel> Images { get; set; } = new ObservableCollection<ImageViewModel>();

        public MainViewModel()
        {
            connect.Open();
            SqlCommand find = new SqlCommand($@"select Charity_Name, Charity_Description,Charity_Logo from Charity", connect);
            SqlDataReader read = find.ExecuteReader();

            while (read.Read()) // построчно считываем данные
            {
                //string path = Directory.GetCurrentDirectory() + $@"\LogoCharity\" + read["Charity_Logo"].ToString();
                Images.Add(new ImageViewModel($@"{read["Charity_Name"].ToString()}:"+"\n"+$@"{read["Charity_Description"].ToString()}",
                Directory.GetCurrentDirectory() + $@"\LogoCharity\" +read["Charity_Logo"].ToString()));
            }
            connect.Close();
        }
    }

}
