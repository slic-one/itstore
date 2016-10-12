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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

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
        Customer customer = null;
        DateTime registrationDate;
        DateTime outRegistrationDate;


        public MainWindow()
		{
			InitializeComponent();
            dataGrid.ItemsSource = resultProducts;

        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var dg = (DataGrid)sender;
            if (dg == null || dg.ItemsSource == null) return;

            var sourceCollection = dg.ItemsSource as ObservableCollection<AddedProduct>;
            if (sourceCollection == null) return;

            sourceCollection.CollectionChanged +=
                new NotifyCollectionChangedEventHandler(DataGrid_CollectionChanged);

        }

       
        void Grid_QuantityCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
                return;
            var d = sender as DataGrid;
            var c2 = (TextBox)e.Column.GetCellContent(e.Row);
            string str = c2.Text;
            decimal q=1;
            try
            {
                q=decimal.Parse(str, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." });
            }
            catch(Exception ex)
            {
                e.Cancel = true;
                d.CancelEdit(DataGridEditingUnit.Cell);
            }
            var index = d.SelectedIndex;
            
            long id = resultProducts[index].Id;
            string name = resultProducts[index].Name;
            string measurement = resultProducts[index].Measurement;
            decimal quantity = q;
            decimal price = resultProducts[index].Price;
            decimal fullPrice = quantity * price;
            resultProducts[index] = new AddedProduct(id, name, measurement, quantity, price);


        }

        void DataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
                price.Content = countPrice();

        }

        // TODO focus on Quantity column (below doesn't work)?????????
        private void addProductToDataGrid(Product product)
        {
            long id = product.idProduct;
			string name = product.Name;
			string measurement = data.Measurements.First(m => m.idMeasurement == product.idMeasurement).Description;
			decimal quantity = 1;
			var price = product.Price + product.Price * data.Markups.First(m => m.idMarkup == product.idMarkup).Percent / 100;

			if (resultProducts.Where(x => x.Id == id).Count() != 0)
			{
				var index = resultProducts.IndexOf(resultProducts.Where(x => x.Id == id).First());
                resultProducts[index] = new AddedProduct(id, name, measurement, resultProducts[index].Quantity + 1, price.Value);
				return;
			}
            
            AddedProduct addedProduct = new AddedProduct(id,name,measurement,quantity,price.Value);
           
            resultProducts.Add(addedProduct);

            //product.Quantity
            
            //dataGrid.CurrentCell = new DataGridCellInfo(
            //dataGrid.Items[dataGrid.Items.Count-1], dataGrid.Columns[3]);
            //dataGrid.BeginEdit();

        }
       
        private void MenuItemQuit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
        private void buttonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try {
                long id = Int64.Parse(textBoxProductId.Text);
                Product product = data.Products.Where(p => p.idProduct == id).First();
                addProductToDataGrid(product);
              
            }
            catch (Exception ex)
            {
                textBoxProductId.Foreground = Brushes.Red;
            }
        }
        
        private decimal countPrice()
        {
            int discountPercent = 0;
            try {
                discountPercent = data.CustomerDiscounts.Where(x => x.idDiscount == customer.idDiscount).Select(x => x.Percent).First();
            }
            catch (Exception ex){ }
            decimal price = 0;
            foreach(var p in resultProducts)
               price += p.Price * p.Quantity;
            decimal discount = price * discountPercent / 100;
            return price-discount;

        }
        private void buttonAddCustomer_Click(object sender, RoutedEventArgs e) {
            
            try {
                int id = Int32.Parse(textBoxCustomerId.Text);
                customer = data.Customers.Where(c => c.idCustomer == id).First();
            }
            catch(Exception ex) { return; }

            int discount = data.CustomerDiscounts.Where(d => d.idDiscount == customer.idDiscount).First().Percent;
            price.Content=countPrice();
            priceDiscount.Content = discount.ToString() + "%";
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
