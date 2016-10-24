using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for PaymentWindow.xaml
	/// </summary>
	public partial class PaymentWindow : Window
	{
		public PaymentWindow(decimal toPay)
		{
			InitializeComponent();
			LoadValues(toPay);
		}

		void LoadValues(decimal toPay)
		{
			labelToPayValue.Content = toPay;
		}

		private void buttonDone_Click(object sender, RoutedEventArgs e)
		{
			//внести значення в базу, закрити діалогове вікно

			Close();
		}
        private void textBox_submitted_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text != "0.0")
            {
                decimal toPay;
                decimal submitted;
                string str = labelToPayValue.Content.ToString();
                string str1 = txt.Text.ToString();
                decimal.TryParse(str, out toPay);

				if (decimal.Parse(textBox_submitted.Text) < toPay)
				{
					buttonDone.IsEnabled = false;
				}
				else
				{
					buttonDone.IsEnabled = true;
				}
				decimal.TryParse(str1, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." }, out submitted);
                submitted = (submitted - toPay);
                labelChangeValue.Content = submitted;
            }
        }
    }
}
