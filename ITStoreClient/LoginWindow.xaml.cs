using System.Windows;
using System.Windows.Media;

namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		Window parent;

		public LoginWindow(Window parent)
		{
			InitializeComponent();
			this.parent = parent;
		}

		private void buttonLogin_Click(object sender, RoutedEventArgs e)
		{
			if (CheckLogin())
			{
				this.Close();
			}
		}

		private bool CheckLogin()
		{
			if (textBoxLogin.Text == null || textBoxLogin.Text == "" || passwordBox.Password == null || passwordBox.Password == "")
			{
				labelStatus.Foreground = Brushes.DarkRed;
				labelStatus.Content = "Поле для вводу логіна/пароля не може бути пустим!";
				return false;
			}

			// TODO перевірка на правильність логіна та пароля з БД

			if (textBoxLogin.Text == "tmp" && passwordBox.Password == "tmp")
			{
				return true;
			}
			else
			{
				labelStatus.Foreground = Brushes.DarkRed;
				labelStatus.Content = "Помилка: неправильний логін/пароль.";
				return false;
			}
		}

		private void buttonQuit_Click(object sender, RoutedEventArgs e)
		{
			parent.Close();
			Close();
		}
	}
}
