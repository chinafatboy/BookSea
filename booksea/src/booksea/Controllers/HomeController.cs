using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using booksea.Models;
using System.Net;

namespace booksea.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookSeaContext DBBookSea;
        public HomeController(BookSeaContext BookSea)
        {
            DBBookSea = BookSea;
        }
        public IActionResult Index()
        {
            ViewBag.title = "主页";
            ViewBag.contBuy = "/";
            HomeIndexModel ivm = new HomeIndexModel();//实例化一个包含排行、精选、热卖的类
            ivm.hotBooks = new List<BookList>();
            ivm.recBooks = new List<BookList>();
            ivm.bookCats = new List<BookCat>();
            ivm.ranBooks = new List<BookList>();
            //获取书本的分类信息
            foreach (var pt in DBBookSea.BookType.Where<BookType>(m => m.Id > 0).GroupBy<BookType, string>(m => m.ClassifType))
            {
                BookCat pc = new BookCat();
                pc.typeName = pt.Key;
                pc.types = new List<BookType>();
                foreach (var p in pt)
                {
                    pc.types.Add(new BookType { Id = p.Id, ClassifType = p.ClassifType, TyptName = p.TyptName });
                }
                ivm.bookCats.Add(pc);
            }

            //获取主页的几个主要商品，分为热卖、排行、精选。每样各3种
            //1.热卖 
            var hotBooks = DBBookSea.Book.Where<Book>(m => m.Id >0).OrderBy<Book, float>(m => (int)m.Id).Take<Book>(3);

            foreach (var p in hotBooks)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, SmallImg = p.SmallImg, Price = p.Price };
                pl.pList = new List<Prices>();
              var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList) //报错 已经解决 BookSeaContext
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                ivm.hotBooks.Add(pl);
            }

            //精选
            var recBooks = DBBookSea.Book.Where<Book>(m => m.Id > 9).OrderBy<Book, float>(m => (int)m.Id).Take(3);
            foreach (var p in recBooks)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, SmallImg = p.SmallImg, Price = p.Price };
                pl.pList = new List<Prices>();
                var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList)
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                ivm.recBooks.Add(pl);
            }

            //排行
            var ranBooks = DBBookSea.Book.Where<Book>(m => m.Id > 6).OrderBy<Book, float>(m => (int)m.Id).Take<Book>(3);
            foreach (var p in ranBooks)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, SmallImg = p.SmallImg, Price = p.Price };
                pl.pList = new List<Prices>();
                var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList)
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                ivm.ranBooks.Add(pl);
            }
            return View(ivm);
        }







        public IActionResult Catalog(int typeId, string typeName)
        {
            ViewBag.catalogName = typeName;
            //FlowerDbContext db = new FlowerDbContext();
            List<BookCat> productCats = new List<BookCat>();
            List<BookList> hotProducts = new List<BookList>();
            foreach (var pt in DBBookSea.BookType.Where<BookType>(m => m.Id > 0).GroupBy<BookType, string>(m => m.ClassifType))
            {
                BookCat pc = new BookCat();
                pc.typeName = pt.Key;
                pc.types = new List<BookType>();
                foreach (var p in pt)
                {
                    pc.types.Add(new BookType { Id = p.Id, ClassifType = p.ClassifType, TyptName = p.TyptName });
                }
                productCats.Add(pc);
            }
            var products = from p in DBBookSea.Book where p.BookState == 1 && (from t in DBBookSea.BookClass where t.TheBookType == typeId select t.TheBook).Contains(p.Id) select p;
            foreach (var p in products)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, BigImg = p.BigImg, Price = p.Price };
                pl.pList = new List<Prices>();
                var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList)
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                hotProducts.Add(pl);
            }
            ViewBag.productCats = productCats;
            ViewBag.catProducts = hotProducts;
            ViewBag.contBuy = Request.Path + Request.QueryString;
            return View();
        }



        public IActionResult WenYi(int typeId, string typeName)
        {
            ViewBag.title = "文艺";
            ViewBag.contBuy = "/";
            HomeIndexModel ivm = new HomeIndexModel();//实例化一个包含排行、精选、热卖的类
            ivm.hotBooks = new List<BookList>();
            ivm.recBooks = new List<BookList>();
            ivm.bookCats = new List<BookCat>();
            ivm.ranBooks = new List<BookList>();
            //获取书本的分类信息
            foreach (var pt in DBBookSea.BookType.Where<BookType>(m => m.Id > 0).GroupBy<BookType, string>(m => m.ClassifType))
            {
                BookCat pc = new BookCat();
                pc.typeName = pt.Key;
                pc.types = new List<BookType>();
                foreach (var p in pt)
                {
                    pc.types.Add(new BookType { Id = p.Id, ClassifType = p.ClassifType, TyptName = p.TyptName });
                }
                ivm.bookCats.Add(pc);
            }

            //获取主页的几个主要商品，分为热卖、排行、精选。每样各3种
            //1.热卖 
            var hotBooks = DBBookSea.Book.Where<Book>(m => m.Id > 9).OrderBy<Book, float>(m => (int)m.Id).Take<Book>(3);

            foreach (var p in hotBooks)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, SmallImg = p.SmallImg, Price = p.Price };
                pl.pList = new List<Prices>();
                var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList) //报错 已经解决 BookSeaContext
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                ivm.hotBooks.Add(pl);
            }

            //精选
            var recBooks = DBBookSea.Book.Where<Book>(m => m.Id > 3).OrderBy<Book, float>(m => (int)m.Id).Take(3);
            foreach (var p in recBooks)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, SmallImg = p.SmallImg, Price = p.Price };
                pl.pList = new List<Prices>();
                var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList)
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                ivm.recBooks.Add(pl);
            }

            //排行
            var ranBooks = DBBookSea.Book.Where<Book>(m => m.Id > 6).OrderBy<Book, float>(m => (int)m.Id).Take<Book>(3);
            foreach (var p in ranBooks)
            {
                BookList pl = new BookList();
                pl.p = new Book { Id = p.Id, BookName = p.BookName, SmallImg = p.SmallImg, Price = p.Price };
                pl.pList = new List<Prices>();
                var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == p.Id);
                foreach (var pclst in priceList)
                {
                    pl.pList.Add(new Prices
                    {
                        memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                        realPrice = (double)pclst.RealPrice
                    });
                }
                ivm.ranBooks.Add(pl);
            }
            return View(ivm);
        }
        public IActionResult jingguan()
        {
            return View();
        }
        public IActionResult keji()
        {
            return View();
        }
        public IActionResult lizhi()
        {
            return View();
        }
        public IActionResult renwen()
        {
            return View();
        }
        public IActionResult shenghuo()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            ViewBag.contBuy = Request.Headers["Referer"].ToString();
            //FlowerDbContext db = new FlowerDbContext();
            List<BookCat> productCats = new List<BookCat>();
            foreach (var pt in DBBookSea.BookType.Where<BookType>(m => m.Id > 0).GroupBy<BookType, string>(m => m.ClassifType))
            {
                BookCat pc = new BookCat();
                pc.typeName = pt.Key;
                pc.types = new List<BookType>();
                foreach (var p in pt)
                {
                    pc.types.Add(new BookType { Id = p.Id, ClassifType = p.ClassifType, TyptName = p.TyptName });
                }
                productCats.Add(pc);
            }
            ViewBag.productCats = productCats;

            BookList pl = new BookList();
            pl.p = DBBookSea.Book.Single<Book>(m => m.Id == id);
            pl.pList = new List<Prices>();
            var priceList = DBBookSea.PriceList.Where<PriceList>(m => m.TheBook == pl.p.Id);
            foreach (var pclst in priceList)
            {
                pl.pList.Add(new Prices
                {
                    memberName = DBBookSea.CustomerType.Where<CustomerType>(m => m.Id == pclst.TheCustomerType).First<CustomerType>().TypeName,
                    realPrice = (double)pclst.RealPrice
                });
            }
            return View(pl);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
