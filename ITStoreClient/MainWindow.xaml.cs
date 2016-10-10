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

        /// TODO !!!!!!!!!!!!
        void Grid_QuantityCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Cancel)
                return;
            var d = sender as DataGrid;
            var c2 = (TextBox)e.Column.GetCellContent(e.Row);
            string str = c2.Text;
            decimal q;
            try
            {
                q=decimal.Parse(str, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." });
            }
            catch(Exception ex)
            {
                e.Cancel = true;
                d.CancelEdit(DataGridEditingUnit.Cell);
            }
          
            //var num=e.Column.Value();
            //Quantity.SetValue(
            //var cell = (AddedProduct)dg.SelectedItem;
            //var product = resultProducts.Where(x => x.Id == cell.Id).First();
            //product.FullPrice = product.Quantity * product.Price;
            //price.Content = priceWithoutDiscount();

        }

        void DataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
                price.Content = priceWithoutDiscount();

        }

        // TODO focus on Quantity column (below doesn't work)?????????
        private void addProductToDataGrid(Product product)
        {
            int id = product.idProduct;
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
                int id = Int32.Parse(textBoxProductId.Text);
                Product product = data.Products.Where(p => p.idProduct == id).First();
                addProductToDataGrid(product);
              
            }
            catch (Exception ex)
            {
                textBoxProductId.Foreground = Brushes.Red;
            }
        }
        
        private decimal priceWithoutDiscount()
        {
            decimal price=0;
            foreach(var p in resultProducts)
               price += p.Price * p.Quantity;
            return price;

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
