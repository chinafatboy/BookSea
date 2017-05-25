using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class POrderDetail
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string BookId { get; set; }
        public int Num { get; set; }
        public int? OrderDePosttatus { get; set; }
        public int? OrderDeRecevtatus { get; set; }
        public decimal? BookPrice { get; set; }
    }
}
