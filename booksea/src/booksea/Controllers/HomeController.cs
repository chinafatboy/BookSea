using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using booksea.Models;

namespace booksea.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBBookSeaProjectContext DBBookSea;
        public HomeController(DBBookSeaProjectContext BookSea)
        {
            this.DBBookSea = BookSea;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "主页";
            ViewBag.contBuy = "/";
            List<PBook> PBookInfo = new List<PBook>();  //实例化图书信息列表
            //把图书信息表的所有内容抛出来
            foreach (var bookInfo in DBBookSea.PBook.Where<PBook>(Book => Book.Id > 0))
            {
                PBookInfo.Add(bookInfo);
            }
            //把获得的结果View给视图使用
            return View(PBookInfo);
        }

        public IActionResult Detail()
        {

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
