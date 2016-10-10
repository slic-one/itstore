using System;
using System.Windows;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Linq;

namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for AdminPanel.xaml
	/// </summary>
	public partial class AdminPanel : Window
	{
        private ObservableCollection<Product> products;


        public AdminPanel()
		{
			InitializeComponent();
            products = GetProducts();
            lstCars.ItemsSource = products;
        }

				
        public static ObservableCollection<Product> GetProducts()
        {
            ShopEntities context = new ShopEntities();

            return new ObservableCollection<Product>(
                    context.Products.Select(p => p).ToList<Product>());
        }

       

        private void cmdGetProduct_Click(object sender, RoutedEventArgs e)
        {
           // products = GetProducts();
            //lstCars.ItemsSource = products;
        }

        private void cmdDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            //cars.Remove((CarTable)lstCars.SelectedItem);
        }
        private void cmdAddCar_Click(object sender, RoutedEventArgs e)
        {
            //cars.Remove((CarTable)lstCars.SelectedItem);
        }
        private void cmdDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            //cars.Remove((CarTable)lstCars.SelectedItem);
        }

		private void buttonQuit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
