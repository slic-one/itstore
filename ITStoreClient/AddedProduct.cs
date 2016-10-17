using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITStoreClient
{
    class AddedProduct
    {
        public AddedProduct(long Id, string Name, string Measurement, decimal Quantity,decimal Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Measurement = Measurement;
            this.Price =Price;
            this.Quantity = Quantity;
            this.FullPrice = Price * Quantity; 
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Measurement { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal FullPrice { get; set; }
    }
}
