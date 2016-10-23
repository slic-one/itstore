using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ITStoreClient
{
    [Serializable]
    [XmlType("Report")]
    public class Report
    {

        public Report() { }
        public Report(string ProductName,long IdProduct, decimal Quantity,decimal Price,string Measurement,decimal TotalSuplyPrice, decimal TotalSalePrice,decimal Profit)
        {
            this.ProductName =ProductName;
            this.IdProduct = IdProduct;
            this.Quantity = Quantity;
            this.Price = Price;
            this.Measurment = Measurement;
            this.TotalSuplyPrice = TotalSuplyPrice;
            this.TotalSalePrice = TotalSalePrice;
            this.Profit = Profit;
         
        }

        public string ProductName { get; set; }
      
        public long IdProduct { get;set; }
        public decimal Quantity { get; set;}
       
        public string Measurment { get; set; }
        public decimal Price { get; set; }

        public decimal TotalSuplyPrice { get; set; }
        public decimal TotalSalePrice { get; set; }
        public decimal Profit { get; set; }
      

    }

   
}
