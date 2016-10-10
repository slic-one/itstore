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
        List<AddedProduct> resultProducts = new List<AddedProduct>();
        ShopEntities data = new ShopEntities();


        public MainWindow()
		{
			InitializeComponent();
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
        //Загрузка содержимого таблицы addedProducts - test
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            List<Product> products = data.Products.ToList();
            foreach (var p in products)
                addProductToDataGrid(p);
            dataGrid.ItemsSource = resultProducts;
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
