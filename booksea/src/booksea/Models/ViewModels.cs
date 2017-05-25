using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksea.Models
{
    public class ViewModels
    {
    }
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
    /// <summary>
    /// 说明：该类为记录购物车的数据库
    /// </summary>
    public class CartItem
    {
        public string BookName { get; set; }
        public string BookId { get; set; }
        public string BookAutor { get; set; }
        public decimal BookPrice { get; set; }

        public int qty { get; set; }
        public string BookNum { get; set; }
        public byte[] Bookover { get; set; }

    }
}
