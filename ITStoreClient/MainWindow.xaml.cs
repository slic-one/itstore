using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//значення береться з бази даних (ціна + кількість)
		decimal totalPrice = 0;

		public MainWindow()
		{
			LoginWindow login = new LoginWindow(this);
			login.ShowDialog();
			InitializeComponent();
		}

		private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void buttonPayment_Click(object sender, RoutedEventArgs e)
		{
			PaymentWindow paymentWindow = new PaymentWindow(totalPrice);
			paymentWindow.ShowDialog();
		}
	}
}
