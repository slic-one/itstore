using System;
using System.Windows;
using System.Windows.Media;
using System.Data.SqlClient;

namespace ITStoreClient
{
	/// <summary>
	/// Interaction logic for AdminPanel.xaml
	/// </summary>
	public partial class AdminPanel : Window
	{
		SqlConnection connection;
		SqlConnectionStringBuilder builder;

		SqlCommand cmd;
		SqlDataReader reader;

		string databaseLogin = "sa"; // default database superadmin name
		string databasePassword = "1"; // default password

		public AdminPanel()
		{
			InitializeComponent();
			//ConnectToDatabase();
		}

		private void ConnectToDatabase()
		{
			try {
				builder = new SqlConnectionStringBuilder();
				builder.UserID = databaseLogin;
				builder.Password = databasePassword;
				builder.InitialCatalog = "ShopDar";
				builder.DataSource = @".\SQLEXPRESS";

				connection = new SqlConnection(builder.ConnectionString);
				connection.Open();
			}
			catch (Exception ex)
			{
				status.Foreground = Brushes.Red;
				status.Content = ex.Message;
				status.Foreground = Brushes.Black;
			}
		}

		private void LoadTables()
		{
			//cmd = connection.CreateCommand();
			//cmd.CommandText = @"SELECT * FROM sys.tables;";
			//reader = cmd.ExecuteReader();

			//while (reader.Read())
			//{
			//	comboBoxTables.Items.Add(reader.GetString(reader.GetOrdinal("name")));
			//}
			//reader.Close();
		}
	}
}
