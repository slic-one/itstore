using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITStoreClient
{
    class AddedProduct
    {
        public AddedProduct(int Id, string Name, string Measurement, int Quantity,decimal Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Measurement = Measurement;
            this.Price =Price;
            this.Quantity = Quantity;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Measurement { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
