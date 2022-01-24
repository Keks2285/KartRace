using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace Kartrace
{
    class TimeRemaining : Window
    {
        // MainWindow mainWindow = new MainWindow();
        public  void Remain(Label l, bool Active)
        {
          
            while (Active)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        DateTime otherDate = new DateTime(2022, 05, 22);
                        TimeSpan remaining = otherDate - DateTime.Now;
                        int months = (int)(remaining.TotalDays / 30);
                        l.Content = string.Format("До начала события сталось {0} месяцев {1} дней, {2} часов, {3} минут, {4} секунд",
                        months, remaining.Days - months * 30, remaining.Hours, remaining.Minutes, remaining.Seconds);
                    });

                }
                catch { Active = false; }

            }

        }

       public static bool Emailvalid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
    
}
