using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class Receipt
    {
        public int Id { get; set; }
        public int? TheOrder { get; set; }
        public string ReceiptFile { get; set; }

        public virtual TheOrder TheOrderNavigation { get; set; }
    }
}
