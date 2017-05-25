using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class PBook
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string BookTypeId { get; set; }
        public string BookPublish { get; set; }
        public string BookAutor { get; set; }
        public decimal? BookPrice { get; set; }
        public int? BookNum { get; set; }
        public byte[] Bookover { get; set; }

        public string BookFeature { get; set; }
    }
}
