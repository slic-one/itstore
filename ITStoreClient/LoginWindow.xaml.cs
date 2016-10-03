using System.Windows;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void buttonLogin_Click(object sender, RoutedEventArgs e)
		{
            if (textBoxLogin.Text == null || textBoxLogin.Text == "" || passwordBox.Password == null || passwordBox.Password == "")
            {
                labelStatus.Foreground = Brushes.DarkRed;
                labelStatus.Content = "Поле для вводу логіна/пароля не може бути пустим!";
                return;
            }
            string str = CheckLogin();
            if (str == "no") 
			{
                labelStatus.Foreground = Brushes.DarkRed;
                labelStatus.Content = "Помилка: неправильний логін/пароль.";
                return;
			}
            if (str == "admin") {

                return;
            }

            if (str == "cashier") {
                MainWindow cashierWindow = new MainWindow();
                cashierWindow.Show();
                this.Close();
                
            }

            labelStatus.Foreground = Brushes.DarkRed;
            labelStatus.Content = "Помилка: " + str;
        }

		private string CheckLogin()
		{
			
            // TODO перевірка на правильність логіна та пароля з БД

            string login = textBoxLogin.Text;
            string password = passwordBox.Password;
            string str;
            try {
                ShopDarEntities data = new ShopDarEntities();
                List<User> users = new List<User>();
                if (data.Users.Where(l => l.Login == login).First().Password == password) {
                    User user = data.Users.First(l => l.Login == login);
                    if (user.Admin == true)
                    {
                        str = "admin";
                    }
                    else {
                        str = "cashier";
                    }
                }
                else {
                    str = "no";
                }
            }
            catch(Exception ex)
            {
                str = ex.Message;
            }
            return str;
       }

		private void buttonQuit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
