using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITStoreClient
{
    class Report
    {
        public Report(string ProductName,long IdProduct, decimal Quantity,string Measurement,decimal SuplyPrice, decimal Price,decimal Profit)
        {
            this.ProductName =ProductName;
            this.IdProduct = IdProduct;
            this.Measurment = Measurement;
            this.Price = Price;
            this.Quantity = Quantity;
            this.SuplyPrice = SuplyPrice;
            this.Profit = Price -SuplyPrice;
        }

        public string ProductName { get; set; }
        public string Measurment { get; set; }

        public long IdProduct { get;set; }
        public decimal Quantity { get; set;}
        public decimal SuplyPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
        public string Cashier { get; set;}
        public string Customer { get;set; }


    }

}
