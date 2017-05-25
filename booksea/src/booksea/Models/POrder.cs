using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class POrder
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderMessage { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public decimal? OrderPrice { get; set; }
    }
}
