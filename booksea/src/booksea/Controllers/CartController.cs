using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using booksea.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using booksea.Models.AccountViewModels;
using booksea.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;
using Microsoft.AspNetCore.Http;
using booksea.Infrastructure;


namespace booksea.Controllers
{
    public class CartController : Controller
    {
        private readonly DBBookSeaProjectContext DBBookSea;
        public CartController(DBBookSeaProjectContext BookSea)
        {
            this.DBBookSea = BookSea;
        }
        public ActionResult Index()
        {
            if (Request.Query["RetUrl"].ToString() != "")
            {
                ViewBag.contimueBuy = Request.Query["RetUrl"].ToString();
            }
            else
            {
                ViewBag.contimueBuy = Request.Headers["RetUrl"].ToString();
            }
            ViewBag.contBuy = ViewBag.contimueBuy;
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            if (curCart == null) curCart = new List<int[]>();
            if (curFavi == null) curFavi = new List<int[]>();
            List<CartItem> cart = new List<CartItem>();
            List<CartItem> favi = new List<CartItem>();
            foreach (int[] i in curCart)
            {
                int curId = i[0];
                int curQty = i[1];
                CartItem cartitem = (from p in DBBookSea.PBook
                                     where p.Id == curId
                                     select
                                     new CartItem
                                     {
                                         BookName = p.BookName,
                                         BookAutor = p.BookAutor,
                                         BookPrice = (decimal)p.BookPrice,
                                         qty = curQty,
                                         Bookover = p.Bookover
                                     }).FirstOrDefault<CartItem>();
                cart.Add(cartitem);
            }
            foreach (int[] i in curFavi)
            {
                int curId = i[0];
                int curQty = i[1];
                CartItem cartitem = (from p in DBBookSea.PBook
                                     where p.Id == curId
                                     select
                                     new CartItem
                                     {
                                         BookName = p.BookName,
                                         BookAutor = p.BookAutor,
                                         BookPrice = (decimal)p.BookPrice,
                                         qty = curQty,
                                         Bookover = p.Bookover
                                     }
                                     ).FirstOrDefault<CartItem>();
                favi.Add(cartitem);
            }
            List<PBook> book = new List<PBook>();
            //该处待定，需要再分析
            foreach (var pt in DBBookSea.PBook.Where<PBook>(m => m.Id > 0))
            {
                book.Add(pt);
            }
            ViewBag.cart = cart;
            ViewBag.favi = favi;
            ViewBag.book = book;
            return View("Cart");
        }

        public ActionResult AddCart(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            if (curCart == null)
            {
                HttpContext.Session.SetJson("Cart",new List<int[]>{ new int[] { id,1} });
            }
            else
            {
                bool found = false;
                foreach (var p in curCart)
                {
                    if (p[0] == id)
                    {
                        found = true;
                        p[1] += 1;
                        break;
                    }
                }
                if (!found)
                {
                    curCart.Add(new int[] { id, 1 });
                }
                HttpContext.Session.SetJson("Cart", curCart);
            }
            return Index();
        }

        public RedirectResult AddFavi(int id)
        {
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            if (curFavi == null)
                HttpContext.Session.SetJson("Favi", new List<int[]> { new int[] { id, 1 } });
            else
            {
                bool found = false;
                foreach (var p in curFavi)
                {
                    if (p[0] == id)
                    {
                        found = true;
                        p[1] += 1;
                        break;
                    }
                }
                if (!found)
                {
                    curFavi.Add(new int[] { id, 1 });
                }
                HttpContext.Session.SetJson("Favi", curFavi);
            }

            string continueBuy = Request.Headers["Referer"].ToString();
            if (Request.Query["retUrl"].ToString() != "")
            {
                continueBuy = Request.Query["retUrl"].ToString();
            }
            return Redirect(continueBuy);
        }


        public RedirectResult updateCartRow(int id)
        {
            int value = int.Parse(Request.Query["value"].ToString());
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            curCart[id][1] = value;
            HttpContext.Session.SetJson("Cart", curCart);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }

        public RedirectResult deleCartRow(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            curCart.RemoveAt(id);
            HttpContext.Session.SetJson("Cart", curCart);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }


        public RedirectResult storeCartRow(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            if (curFavi == null)
            {
                curFavi = new List<int[]>();
                HttpContext.Session.SetJson("Favi", curFavi);
            }
            curFavi.Add(curCart[id]);
            curCart.RemoveAt(id);
            HttpContext.Session.SetJson("Cart", curCart);
            HttpContext.Session.SetJson("Favi", curFavi);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
        public RedirectResult deleFaviRow(int id)
        {
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            curFavi.RemoveAt(id);
            HttpContext.Session.SetJson("Favi", curFavi);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }
        public RedirectResult buyFaviRow(int id)
        {
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            List<int[]> curFavi = HttpContext.Session.GetJson<List<int[]>>("Favi");
            curCart.Add(curFavi[id]);
            curFavi.RemoveAt(id);
            HttpContext.Session.SetJson("Cart", curCart);
            HttpContext.Session.SetJson("Favi", curFavi);
            return Redirect("/Cart?retUrl=" + Request.Query["retUrl"].ToString());
        }




    }
}
