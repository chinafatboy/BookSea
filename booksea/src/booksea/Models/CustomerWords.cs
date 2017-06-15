using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class CustomerWords
    {
        public int Id { get; set; }
        public int? TheOrder { get; set; }
        public string Words { get; set; }

        public virtual TheOrder TheOrderNavigation { get; set; }
    }
}
