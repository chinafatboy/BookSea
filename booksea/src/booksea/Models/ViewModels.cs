using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using booksea.Models.AccountViewModels;

namespace booksea.Models
{
    public class ViewModels
    {
    }
    //页面信息
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
        public string BookFeature { get; set; }
        public double price { get; set; }
        public double realPrice { get; set; }
        public int qty { get; set; }
        public string smallImg { get; set; }

    }
    /// <summary>
    /// 价格，包括会员名字和会员价
    /// </summary>
    public class Prices
    {
        public string memberName { get; set; }
        public double realPrice { get; set; }
    }
    /// <summary>
    /// 将所有图书信息存储的一个列表
    /// </summary>
    public class BookList
    {
        public Book p { get; set; }//图书所有信息
        public List<Prices> pList { get; set; }//价格列表
    }
    /// <summary>
    /// BookCat是表示一堆书为一个类包括类型名和类型
    /// </summary>
    public class BookCat
    {
        //类型
        public string typeName { get; set; }//类型名
        public List<BookType> types { get; set; }//图书类型列表
    }
    /// <summary>
    /// 定义页面的热卖和精选排行的列表存储
    /// </summary>
    public class HomeIndexModel
    {
        //热卖
        public List<BookList> hotBooks { get; set; }//热卖
        //精选
        public List<BookList> recBooks { get; set; }//精选
        //排行
        public List<BookList> ranBooks { get; set; }//排行
        public List<BookCat> bookCats { get; set; }//图书类型
    }


    public class OrderViewModel
    {
        public Customer curCustomer { get; set; }
        public Payment payment { get; set; }
        public List<OrderInfo> orders { get; set; }
        public List<Consigenn> receivers { get; set; }
        public List<CustomerWords> words { get; set; }
        public int orderQty { get; set; }
    }

    public class OrderInfo
    {
        public double price { get; set; }
        public double realPrice { get; set; }

        public int theBook { get; set; }
        public string bookName { get; set; }
        public string bookFeature { get; set; }
        public string smallImg { get; set; }
    }

    public class PayRequestInfo
    {
        public string PostUrl { get; set; }
        public string MerId { get; set; }
        public string Amt { get; set; }
        public string PaymentTypeObjId { get; set; }
        public string MerTransId { get; set; }
        public string ReturnUrl { get; set; }
        public string CheckValue { get; set; }

    }

    public class MemberHomeModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        //public LocalPasswordModel PassWordModel { get; set; }
        public RegisterViewModel CustomerInfo { get; set; }
        public List<OrderList> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class OrderList
    {
        public DateTime orderTime { get; set; }
        public double amt { get; set; }

        public string orderState { get; set; }
        public string productName { get; set; }
        public string smallImg { get; set; }
        public DateTime transTime { get; set; }
        public string name { get; set; }
        public string words { get; set; }
        public string receiptFile { get; set; }

    }



}

