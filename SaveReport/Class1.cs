using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Data;
namespace SaveReport
{
    public class Report
    {
        private DataTable GetData()
        {
            //строка соединения
            string connString = @"Data Source=406-8;Initial Catalog=ShopDar;Persist Security Info=True;User ID=sa;Password=1 Connection Timeout=260";

            SqlConnection con = new SqlConnection(connString);

            DataTable dt = new DataTable();
            try
            {
//                CREATE VIEW Report AS
//SELECT DateSale, idSale, idProduct,[User].Name,Sale.Price,Product.Price,idCustomer,Sale.Quantity
//  FROM Sale,Product,[User]

        string query = @"SELECT     Customers.CompanyName, Customers.ContactName, Orders.ShipVia, Orders.Freight, Orders.ShipName, Orders.ShipAddress, Orders.ShipCity, Orders.ShipRegion, Orders.ShipPostalCode, Orders.ShipCountry
FROM         Customers INNER JOIN
                      Orders ON Customers.CustomerID = Orders.CustomerID";
                SqlCommand comm = new SqlCommand(query, con);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }
    }
}
