﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ITStoreClient
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private ShopEntities context;

        private ObservableCollection<User> users;
        private ObservableCollection<User> cashiers;

        private ObservableCollection<Product> products;
        private Product selectedProduct;
        public Dictionary<int, string> CategoryProperty { get; set; }
        public Dictionary<int, string> MarkUpProperty { get; set; }
        public Dictionary<int, string> MeasurementProperty { get; set; }
        public Dictionary<int, string> ProducerProperty { get; set; }

        private ObservableCollection<Producer> producers;
        private ObservableCollection<Supplier> suppliers;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Markup> markUps;
        private ObservableCollection<Measurement> measurements;


        private ObservableCollection<Customer> customers;
        private Customer selectedCustomer;
        public Dictionary<int, string> DiscountProperty { get; set; }

        private bool addOrdeleteIsPushed;
        public AdminPanel()
        {
            InitializeComponent();
            context = new ShopEntities();

            users = GetUsers();
            UsersList.ItemsSource = users;
            UsersList.AutoGeneratedColumns += users_AutoGeneratedColumns;

            DiscountProperty = GetDiscounts();
            customers = GetCustomers();
            CustomersList.ItemsSource = customers;
            CustomersList.AutoGeneratedColumns += customers_AutoGeneratedColumns;
            DiscountComboBox.ItemsSource = DiscountProperty.Values;


            CategoryProperty = GetCategoriesList();
            MarkUpProperty = GetMarkUpsList();
            MeasurementProperty = GetMeasurmentsList();
            ProducerProperty = GetProdusersList();
            products = GetProducts();
            ProductsList.ItemsSource = products;
            ProductsList.AutoGeneratedColumns += products_AutoGeneratedColumns;
            CategoryComboBox.ItemsSource = CategoryProperty.Values;
            MarkupComboBox.ItemsSource = MarkUpProperty.Values;
            MeasurementComboBox.ItemsSource = MeasurementProperty.Values;
            ProducerComboBox.ItemsSource = ProducerProperty.Values;

            producers = GetProducers();
            ProducersList.ItemsSource = producers;
            ProducersList.AutoGeneratedColumns += producers_AutoGeneratedColumns;

            suppliers = GetSuppliers();
            SuppliersList.ItemsSource = suppliers;
            SuppliersList.AutoGeneratedColumns += suppliers_AutoGeneratedColumns;

            categories = GetCategories();
            CategoriesList.ItemsSource = categories;
            CategoriesList.AutoGeneratedColumns+= categories_AutoGeneratedColumns;

            cashiers = GetCashiers();
            cashierComboBox.ItemsSource = cashiers;
            customerComboBox.ItemsSource = customers;



        }

        #region User

        public ObservableCollection<User> GetUsers()
        {

            return new ObservableCollection<User>(
                    context.Users.Select(p => p).ToList<User>());
        }

        //change title of columns in datagridview
        void users_AutoGeneratedColumns(object sender, EventArgs e)
        {
            UsersList.Columns[0].Header = "Код користувача";
            UsersList.Columns[1].Header = "Ім'я";
            UsersList.Columns[2].Header = "Логін";
            UsersList.Columns[3].Header = "Пароль";
            UsersList.Columns[4].Header = "Адміністратор";
            UsersList.Columns[5].Visibility = Visibility.Hidden;//hide column
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            
            context.Users.Add(new User { Name = UserName.Text, Login = UserLogin.Text, Password = UserPassword.Text, Admin = UserIsAdmin.IsChecked.Value });
            context.SaveChanges();
            users = GetUsers();
            UsersList.ItemsSource = users;
            
            //int lastId = (int)users.Last().idUser + 1;
            //users.Add(new User { idUser = lastId, Name = UserName.Text, Login = UserLogin.Text, Password = UserPassword.Text, Admin = UserIsAdmin.IsChecked.Value });
        }
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            
            context.Users.Remove((User)UsersList.SelectedItem);
            users.Remove((User)UsersList.SelectedItem);
           

        }
        #endregion

        #region Customer


        private Dictionary<int, string> GetDiscounts()
        {
            return new Dictionary<int, string>(context.CustomerDiscounts.Select(p => new { p.idDiscount, p.Percent }).ToDictionary(p => p.idDiscount, p => p.Percent.ToString()));
        }

        public ObservableCollection<Customer> GetCustomers()
        {

            return new ObservableCollection<Customer>(
                    context.Customers.Select(p => p).ToList<Customer>());
        }

        //change title of columns in datagridview
        void customers_AutoGeneratedColumns(object sender, EventArgs e)
        {
            CustomersList.Columns[0].Header = "Код клієнта";
            CustomersList.Columns[1].Header = "Ім'я";
            CustomersList.Columns[2].Header = "Прізвище";
            CustomersList.Columns[3].Header = "Сума покупок";
            CustomersList.Columns[4].Visibility = Visibility.Hidden; //hide column
            CustomersList.Columns[5].Header = "Знижка %";
            CustomersList.Columns[6].Visibility = Visibility.Hidden;
        }


        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            addOrdeleteIsPushed = true;

            int id = DiscountProperty.Where(x => x.Value.Equals(DiscountComboBox.SelectedValue.ToString())).FirstOrDefault().Key;
            context.Customers.Add(new Customer { idCustomer = long.Parse(CustomerId.Text), Name = CustomerName.Text, Surname = CustomerSurname.Text, Spended_Money = Decimal.Parse(CustomerSpentMoney.Text), idDiscount = id });
            context.SaveChanges();
            customers = GetCustomers();
            CustomersList.ItemsSource = customers;

            addOrdeleteIsPushed = false;
            // CustomerDiscount cDiscount = new CustomerDiscount {idDiscount=id,Title=" ",Percent= Int32.Parse(DiscountComboBox.SelectedValue.ToString()) };
            // customers.Add(new Customer { idCustomer = long.Parse(CustomerId.Text), Name = CustomerName.Text, Surname = CustomerSurname.Text, Spended_Money = Decimal.Parse(CustomerSpentMoney.Text), idDiscount = id, CustomerDiscount=cDiscount});
        }


        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            addOrdeleteIsPushed = true;
            context.Customers.Remove((Customer)CustomersList.SelectedItem);
            customers.Remove((Customer)CustomersList.SelectedItem);
            addOrdeleteIsPushed = false;

        }

        //event for change selection in datagridview(CustomersList) and change selectedItem value in DiscountCombobox
        private void CustomersList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (addOrdeleteIsPushed != true)
            {
                selectedCustomer = CustomersList.SelectedItems[0] as Customer;
                if (selectedCustomer != null)
                {
                    DiscountComboBox.SelectedItem = DiscountProperty[selectedCustomer.idDiscount];
                }
                else
                {
                    DiscountComboBox.SelectedItem = DiscountProperty.First().Value;
                }
            }
        }

        //event for change selection in combobox (DiscountCombobox) and transfer it to datagridview
        private void DiscountComboBox_DropDownClosed(object sender, EventArgs e)
        {

            if (selectedCustomer != null)
            {
                string percent = ((sender as ComboBox).SelectedValue.ToString()); //get selected value in combobox
                GetCell(CustomersList, 5).Content = Int32.Parse(percent); //change discount value in selected cell

                int key = DiscountProperty.Where(x => x.Value.Equals(percent)).FirstOrDefault().Key; //get key by selected value in combobox
                                                                                                     //for test
                                                                                                     //GetCell(CustomersList, 4).Content = key;
                if (key != context.Customers.Where(x => x.idCustomer == selectedCustomer.idCustomer).FirstOrDefault().idDiscount)
                {
                    context.Customers.Where(x => x.idCustomer == selectedCustomer.idCustomer).FirstOrDefault().idDiscount = key;  //change idDiscount in Database

                }

            }


        }
        #endregion

        #region Product

        private Dictionary<int, string> GetCategoriesList()
        {
            return new Dictionary<int, string>(context.Categories.Select(p => new { p.idCatedory, p.Name }).ToDictionary(p => p.idCatedory, p => p.Name));
        }
        private Dictionary<int, string> GetMarkUpsList()
        {
            return new Dictionary<int, string>(context.Markups.Select(p => new { p.idMarkup, p.Percent }).ToDictionary(p => p.idMarkup, p => p.Percent.ToString()));
        }
        private Dictionary<int, string> GetMeasurmentsList()
        {
            return new Dictionary<int, string>(context.Measurements.Select(p => new { p.idMeasurement, p.Description }).ToDictionary(p => p.idMeasurement, p => p.Description));
        }
        private Dictionary<int, string> GetProdusersList()
        {
            return new Dictionary<int, string>(context.Producers.Select(p => new { p.idProducer, p.Name }).ToDictionary(p => p.idProducer, p => p.Name));
        }


        public ObservableCollection<Product> GetProducts()
        {

            return new ObservableCollection<Product>(
                    context.Products.Select(p => p).ToList<Product>());
        }
        //change title of columns in datagridview
        void products_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ProductsList.Columns[0].Header = "Код товару";
            ProductsList.Columns[1].Header = "Назва";
            ProductsList.Columns[2].Visibility = Visibility.Hidden;
            ProductsList.Columns[3].Header = "Ціна";
            ProductsList.Columns[4].Header = "Кількість";
            ProductsList.Columns[5].Visibility = Visibility.Hidden;
            ProductsList.Columns[6].Visibility = Visibility.Hidden;
            ProductsList.Columns[7].Visibility = Visibility.Hidden;
            ProductsList.Columns[8].Header = "Категорія";
            ProductsList.Columns[9].Visibility = Visibility.Hidden;
            ProductsList.Columns[10].Header = "Націнка %";
            ProductsList.Columns[11].Header = "Одиниця виміру";
            ProductsList.Columns[12].Header = "Виробник";
            ProductsList.Columns[13].Visibility = Visibility.Hidden;
        }
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            addOrdeleteIsPushed = true;
            context.Products.Remove((Product)ProductsList.SelectedItem);
            products.Remove((Product)ProductsList.SelectedItem);
            addOrdeleteIsPushed = false;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            addOrdeleteIsPushed = true;
            int CategoryId = CategoryProperty.Where(x => x.Value.Equals(CategoryComboBox.SelectedValue.ToString())).FirstOrDefault().Key;
            int MarkUpId = MarkUpProperty.Where(x => x.Value.Equals(MarkupComboBox.SelectedValue.ToString())).FirstOrDefault().Key;
            int MeasurementId = MeasurementProperty.Where(x => x.Value.Equals(MeasurementComboBox.SelectedValue.ToString())).FirstOrDefault().Key;
            int ProducerId = ProducerProperty.Where(x => x.Value.Equals(ProducerComboBox.SelectedValue.ToString())).FirstOrDefault().Key;


            context.Products.Add(new Product { idProduct = long.Parse(ProductId.Text), Name = ProductName.Text, idCategory = CategoryId, Price = Decimal.Parse(ProductPrice.Text), Quantity = Decimal.Parse(ProductQuantity.Text), idProducer = ProducerId, idMarkup = MarkUpId, idMeasurement = MeasurementId });
            context.SaveChanges();
            products = GetProducts();
            ProductsList.ItemsSource = products;
            addOrdeleteIsPushed = false;

        }

        private void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (addOrdeleteIsPushed != true)
            {
                selectedProduct = ProductsList.SelectedItems[0] as Product;
                if (selectedProduct != null)
                {
                    CategoryComboBox.SelectedItem = CategoryProperty[selectedProduct.idCategory];
                    MarkupComboBox.SelectedItem = MarkUpProperty[selectedProduct.idMarkup];
                    MeasurementComboBox.SelectedItem = MeasurementProperty[selectedProduct.idMeasurement];
                    ProducerComboBox.SelectedItem = ProducerProperty[selectedProduct.idProducer];
                }
                else
                {
                    CategoryComboBox.SelectedItem = CategoryProperty.First().Value;
                    MarkupComboBox.SelectedItem = MarkUpProperty.First().Value;
                    MeasurementComboBox.SelectedItem = MeasurementProperty.First().Value;
                    ProducerComboBox.SelectedItem = ProducerProperty.First().Value;
                }
            }

        }

        private void CategoryComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (selectedProduct != null)
            {
                string name = ((sender as ComboBox).SelectedValue.ToString()); //get selected value in combobox
                GetCell(ProductsList, 8).Content = name; //change category value in selected cell

                int key = CategoryProperty.Where(x => x.Value.Equals(name)).FirstOrDefault().Key; //get key by selected value in combobox

                if (key != context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idCategory)
                {
                    context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idCategory = key;  //change idCategory in Database

                }

            }
        }

        private void MarkupComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (selectedProduct != null)
            {
                string percent = ((sender as ComboBox).SelectedValue.ToString());
                GetCell(ProductsList, 10).Content = Int32.Parse(percent);

                int key = MarkUpProperty.Where(x => x.Value.Equals(percent)).FirstOrDefault().Key;

                if (key != context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idMarkup)
                {
                    context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idMarkup = key;

                }

            }
        }

        private void MeasurementComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (selectedProduct != null)
            {
                string discript = ((sender as ComboBox).SelectedValue.ToString());
                GetCell(ProductsList, 11).Content = discript;

                int key = MeasurementProperty.Where(x => x.Value.Equals(discript)).FirstOrDefault().Key;

                if (key != context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idMeasurement)
                {
                    context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idMeasurement = key;

                }

            }
        }

        private void ProducerComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (selectedProduct != null)
            {
                string name = ((sender as ComboBox).SelectedValue.ToString());
                GetCell(ProductsList, 12).Content = name;

                int key = ProducerProperty.Where(x => x.Value.Equals(name)).FirstOrDefault().Key;

                if (key != context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idProducer)
                {
                    context.Products.Where(x => x.idProduct == selectedProduct.idProduct).FirstOrDefault().idProducer = key;

                }

            }
        }
        #endregion


        #region Producer

        public ObservableCollection<Producer> GetProducers()
        {

            return new ObservableCollection<Producer>(
                    context.Producers.Select(p => p).ToList<Producer>());
        }

        //change title of columns in datagridview
        void producers_AutoGeneratedColumns(object sender, EventArgs e)
        {
            ProducersList.Columns[0].Header = "Код виробника";
            ProducersList.Columns[1].Header = "Назва";
            ProducersList.Columns[2].Visibility = Visibility.Hidden;
            ProducersList.Columns[3].Header = "Адреса";
            ProducersList.Columns[4].Visibility = Visibility.Hidden;

        }

        private void AddProducer_Click(object sender, RoutedEventArgs e)
        {
            context.Addresses.Add(new Address { Country = ProducerCountry.Text, Town = ProducerTown.Text, Street = ProducerStreet.Text });
            context.SaveChanges();
            int lastId = context.Addresses.ToList().Last().idAddress;
            context.Producers.Add(new Producer { Name = ProducerName.Text, idAddress = lastId });
            context.SaveChanges();
            producers = GetProducers();
            ProducersList.ItemsSource = producers;
            //producers.Add(new Producer { idProducer = (int)producers.Last().idProducer + 1, Name = ProducerName.Text, idAddress = lastId, Address = new Address { Country = ProducerCountry.Text, Town = ProducerTown.Text, Street = ProducerStreet.Text } });
        }
        private void DeleteProducer_Click(object sender, RoutedEventArgs e)
        {
            context.Producers.Remove((Producer)ProducersList.SelectedItem);
            producers.Remove((Producer)ProducersList.SelectedItem);

        }
        #endregion


        #region Supplier

        public ObservableCollection<Supplier> GetSuppliers()
        {

            return new ObservableCollection<Supplier>(
                    context.Suppliers.Select(p => p).ToList<Supplier>());
        }

        //change title of columns in datagridview
        void suppliers_AutoGeneratedColumns(object sender, EventArgs e)
        {
            SuppliersList.Columns[0].Header = "Код постачальника";
            SuppliersList.Columns[1].Header = "Назва";
            SuppliersList.Columns[2].Visibility = Visibility.Hidden;
            SuppliersList.Columns[3].Header = "Адреса";
            SuppliersList.Columns[4].Visibility = Visibility.Hidden;
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            context.Addresses.Add(new Address { Country = SupplierCountry.Text, Town = SupplierTown.Text, Street = SupplierStreet.Text });
            context.SaveChanges();
            int lastId = context.Addresses.ToList().Last().idAddress;
            context.Suppliers.Add(new Supplier { Name = SupplierName.Text, idAddress = lastId });
            context.SaveChanges();
            suppliers = GetSuppliers();
            SuppliersList.ItemsSource = suppliers;
            //suppliers.Add(new Supplier { idSupplier = (int)suppliers.Last().idSupplier + 1, Name = SupplierName.Text, idAddress = lastId, Address = new Address { Country = SupplierCountry.Text, Town = SupplierTown.Text, Street = SupplierStreet.Text } });
        }
        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            context.Suppliers.Remove((Supplier)SuppliersList.SelectedItem);
            suppliers.Remove((Supplier)SuppliersList.SelectedItem);

        }
        #endregion

        #region Category
        public ObservableCollection<Category> GetCategories()
        {

            return new ObservableCollection<Category>(
                    context.Categories.Select(p => p).ToList<Category>());
        }

        //change title of columns in datagridview
        void categories_AutoGeneratedColumns(object sender, EventArgs e)
        {
            CategoriesList.Columns[0].Header = "Код категорії";
            CategoriesList.Columns[1].Header = "Назва";
            CategoriesList.Columns[2].Visibility = Visibility.Hidden;//hide column
           
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {

            context.Categories.Add(new Category { Name = CategoryName.Text});
            context.SaveChanges();
            categories = GetCategories();
            CategoriesList.ItemsSource = categories;
           
        }
        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {

            context.Categories.Remove((Category)CategoriesList.SelectedItem);
            categories.Remove((Category)CategoriesList.SelectedItem);


        }
        #endregion

        #region Reports

        public ObservableCollection<User> GetCashiers()
        {

            return new ObservableCollection<User>(
                    context.Users.Where(u=>u.Admin==false).Select(p => p).ToList<User>());
        }

        private void searchReportClick(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Report> reportItems = new ObservableCollection<Report>();
            ShopEntities data = context;
            DateTime? fromDate = dateFrom.SelectedDate;
            DateTime? toDate = dateTo.SelectedDate;

            var sales = data.Sales.Where(s => s.DateSale >= fromDate && s.DateSale <= toDate).Select(x => x);



            var prod = from sl in sales
                     from pq in data.ProductOrderQuantities
                     where (pq.IdSale == sl.idSale)
                     group pq by  pq.IdProduct
                     into grp
                     select new { idProduct = grp.Key, Quantity = grp.Select(x => x.Quantity).Sum()};

            var rez = from p in data.Products
                      from pr in prod
                      from m in data.Measurements
                      from markup in data.Markups
                      where (p.idProduct == pr.idProduct && p.idMeasurement == m.idMeasurement && p.idMarkup == markup.idMarkup)
                      select new
                      {
                          Name = p.Name,
                          id = pr.idProduct,
                          Quantity = pr.Quantity,
                          Measurement = m.Name,
                          SupplyPrice = p.Price,
                          SalePrice = p.Price + p.Price * markup.Percent / 100,
                          Profit = p.Price * markup.Percent / 100
                      };

            dataGridReport.ItemsSource = prod.ToList();
        }
        
        #endregion

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context.SaveChanges();
                MessageBox.Show("Зміни успішно збережені.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            //context.SaveChanges();
            Close();
        }

        #region GETCELL // methods to find cell in selected row


        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridCell GetCell(DataGrid grid, int column)
        {
            DataGridRow row = grid.ItemContainerGenerator.ContainerFromIndex(grid.SelectedIndex) as DataGridRow;

            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }





        #endregion



    }
}
