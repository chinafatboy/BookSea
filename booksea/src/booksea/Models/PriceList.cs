using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class PriceList
    {
        public int Id { get; set; }
        public int? TheBook { get; set; }
        public int? TheCustomerType { get; set; }
        public double? RealPrice { get; set; }

        public virtual CustomerType TheCustomerTypeNavigation { get; set; }
        public virtual Book TheProductNavigation { get; set; }
    }
}
