//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITStoreClient
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductOrderQuantity
    {
        public int IdProductOrderQuantity { get; set; }
        public int IdSale { get; set; }
        public long IdProduct { get; set; }
        public decimal Quantity { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
