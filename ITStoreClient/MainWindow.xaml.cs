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
using System.Collections.ObjectModel;
namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//значення береться з бази даних (ціна + кількість)
		decimal totalPrice = 0;
        ObservableCollection<AddedProduct> resultProducts = new ObservableCollection<AddedProduct>();
        ShopEntities data = new ShopEntities();


        public MainWindow()
		{
			InitializeComponent();
            dataGrid.ItemsSource = resultProducts;

        }

        //Загрузка содержимого таблицы addedProducts - test
        private void addProductToDataGrid(Product product)
        {
            int id = product.idProduct;
            string name = product.Name;
            string measurement = data.Measurements.First(m => m.idMeasurement == product.idMeasurement).Description;
            int quantity = 1;
            var price = product.Price+product.Price * data.Markups.First(m => m.idMarkup == product.idMarkup).Percent/100;
            AddedProduct addedProduct = new AddedProduct(id,name,measurement,quantity,price.Value);
            resultProducts.Add(addedProduct);
            
        }
       
        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
        private void buttonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try {
                int id = Int32.Parse(textBoxProductId.Text);
                Product product = data.Products.Where(p => p.idProduct == id).First();
                addProductToDataGrid(product);
              
            }
            catch (Exception ex)
            {
                textBoxProductId.Foreground = Brushes.Red;
            }
        }
        
        private void buttonPayment_Click(object sender, RoutedEventArgs e)
		{
			PaymentWindow paymentWindow = new PaymentWindow(totalPrice);
			paymentWindow.ShowDialog();
		}

        private void textBoxProductId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxProductId.Foreground != Brushes.Black)
                textBoxProductId.Foreground = Brushes.Black;
        }

        private void textBoxCustomerId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxCustomerId.Foreground != Brushes.Black)
                textBoxCustomerId.Foreground = Brushes.Black;
        }
    }
}
