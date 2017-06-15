using System;
using System.Collections.Generic;

namespace booksea.Models
{
    //图书类存储图书资料包括书名、描述等
    public partial class Book
    {
        public Book()
        {
            TheOrder = new HashSet<TheOrder>();//顺序
            PriceList = new HashSet<PriceList>();//价格
            BookClass = new HashSet<BookClass>();//图书分类
        }
        public int Id { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string BookFeature { get; set; }
        public string BookDescription { get; set; }
        public string Bookmeaning { get; set; }
        public double? Price { get; set; }
        public string SmallImg { get; set; }
        public string BigImg { get; set; }
        public int? BookState { get; set; }

        public virtual ICollection<TheOrder> TheOrder { get; set; }
        public virtual ICollection<PriceList> PriceList { get; set; }
        public virtual ICollection<BookClass> BookClass { get; set; }
    }
}
