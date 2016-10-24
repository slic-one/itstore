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
using System;

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
        User user = null;
        


        public MainWindow(User user)
		{
			InitializeComponent();
            dataGrid.ItemsSource = resultProducts;
            this.user = user;
            
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
            totalPrice = countPrice();
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
			AddProduct();
        }

		private void AddProduct()
		{
			try
			{
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
        private void findCustomer() {

            try
            {
                long id = Int64.Parse(textBoxCustomerId.Text);
                customer = data.Customers.Where(c => c.idCustomer == id).First();
            }
            catch (Exception ex) { return; }

            int discount = data.CustomerDiscounts.Where(d => d.idDiscount == customer.idDiscount).First().Percent;
            price.Content = countPrice();
            priceDiscount.Content = discount.ToString() + "%";
        }
        private void buttonAddCustomer_Click(object sender, RoutedEventArgs e) {
            findCustomer();
        }
        private void buttonPayment_Click(object sender, RoutedEventArgs e)
		{
			PaymentWindow paymentWindow = new PaymentWindow(totalPrice);
            int idSale=ChangeSale();
			paymentWindow.ShowDialog();
            ChangeDB(idSale);
            string products=null;
            foreach(var i in resultProducts)
            {
                products+=(string.Format("{0}*{1}={2:c}",i.Name,i.Quantity,i.Price*i.Quantity)+"\r");
            }

            string st = string.Format("{0}{1:T}{2}{3}{4}{5}{6}{7}","\t Новий Чек\r",DateTime.Now, "\r Касир: ", user.Name,"\r Товари: \r"
                ,products,"\r Ціна:",totalPrice);
            MessageBox.Show(st);
			resultProducts.Clear();
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

		private void textBoxSearchById_TextChanged(object sender, TextChangedEventArgs e)
		{
			listViewSearchItems.Items.Clear();
			try {
				var findRes = data.Products.Where(x => x.idProduct.ToString().Substring(0, textBoxSearchById.Text.Length) == textBoxSearchById.Text);

				foreach (Product res in findRes)
				{
					//listViewSearchItems.Items.Add(res.Name);
					ListViewItem itm = new ListViewItem();
					listViewSearchItems.Items.Add(new ListViewItem { Content = (res.idProduct + " " + res.Name + " " + res.Price), Tag = res });
				}
			}
			catch(Exception)
			{
				listViewSearchItems.Items.Clear();
                return;
			}
		}

		protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// TODO add to resultProduct
		}

		private void textBoxSearchByName_TextChanged(object sender, TextChangedEventArgs e)
		{
			listViewSearchItems.Items.Clear();
			try
			{
				var findRes = data.Products.Where(x => x.Name.ToString().Substring(0, textBoxSearchByName.Text.Length) == textBoxSearchByName.Text);

				foreach (Product res in findRes)
				{
					ListViewItem itm = new ListViewItem();
					listViewSearchItems.Items.Add(new ListViewItem { Content = (res.idProduct + " " + res.Name + " " + res.Price), Tag = res });
				}
			}
			catch (Exception)
			{
				listViewSearchItems.Items.Clear();
				return;
			}
		}

		private void textBoxProductId_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				AddProduct();
				textBoxProductId.Clear();
			}
		}

		private void textBoxCustomerId_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
                findCustomer();
                textBoxCustomerId.Clear();
			}
		}

		private void listViewSearchItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// витягує з виділеного елемента списку Tag, в якому ссилка на Product
			addProductToDataGrid((Product)((ListViewItem)((ListView)sender).SelectedItem).Tag);
		}

        private void ChangeDB(int id)
        {
            foreach(var tmp in resultProducts)
            {
                
                ITStoreClient.ProductOrderQuantity ord = new ProductOrderQuantity();
                id = data.Sales.Select(s1 => s1.idSale).ToArray().Last();
                int idPOQ = data.ProductOrderQuantities.Select(x => x.IdProductOrderQuantity).Max();
                idPOQ += 1;
                ord.IdProductOrderQuantity = idPOQ;
                ord.IdSale = id;
                Product product = data.Products.Where(p => p.Name == tmp.Name).First();
                ord.IdProduct=product.idProduct;
                ord.Quantity = tmp.Quantity;
                data.ProductOrderQuantities.Add(ord);
                var Quantityproduct = from t in data.Products
                        where t.Name == tmp.Name
                        select t;
                
               foreach(var i in Quantityproduct)
                {
                    i.Quantity -= tmp.Quantity;
                }
                data.SaveChanges();
            }
            
        }
        private int ChangeSale()
        {
            ITStoreClient.Sale sale=new Sale();
            if(customer != null)
            {
                sale.idCustomer = customer.idCustomer;
            }
                
                sale.idUser = user.idUser;
                sale.FullPrice = totalPrice;
                sale.DateSale = DateTime.Now;
            bool s=true;
            int id;
            do
            {
                if (!s)
                {
                    data.Sales.Remove(sale);
                }
                data.Sales.Add(sale);
                id = data.Sales.Select(s1 => s1.idSale).ToArray().Last();
                try
                {
                    data.SaveChanges();
                }
                catch(Exception ex)
                {
                    s = false;
                }
                

            } while (!s);
                
                   if(customer!=null)
            {
                data.Customers.Where(c => c.idCustomer == customer.idCustomer).First().Spended_Money+=totalPrice;
            }
            data.SaveChanges();
            return id;
        }

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			resultProducts.Clear();
		}
	}
   
}
