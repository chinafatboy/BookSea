using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class BookClass
    {
        public int Id { get; set; }
        public int? TheBook { get; set; }
        public int? TheBookType { get; set; }
        public virtual Book TheProductNavigation { get; set; }
        public virtual BookType TheProductTypeNavigation { get; set; }
    }
}
