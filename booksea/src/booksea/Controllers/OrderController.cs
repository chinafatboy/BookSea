using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using booksea.Models;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http;
using booksea.Infrastructure;
using Microsoft.AspNetCore.Hosting;


namespace booksea.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookSeaContext DBBookSea;
        private IHostingEnvironment host = null;
        public OrderController(BookSeaContext db,IHostingEnvironment _host)
        {
            DBBookSea = db;
            host = _host;
        }

        //获取订单页
        //GET:/Order/
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Request = Request;
            string uid = User.Identity.Name;
            OrderViewModel ovm = new OrderViewModel();
            ovm.orders = new List<OrderInfo>();
            ovm.receivers = new List<Consigenn>();
            ovm.payment = new Payment();
            ovm.words = new List<CustomerWords>();
            //获取信息显示在页面
            ovm.curCustomer = DBBookSea.Customer.Single(m => m.UserName == uid);
            ViewBag.payments = DBBookSea.PaymentType.Where(m => m.Id > 0).ToArray<PaymentType>();
            List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
            ovm.orderQty = 0;
            ovm.payment.Amount = 0.0;
            foreach (var cartItem in curCart)
            {
                ovm.orderQty += cartItem[1];
                int pId = cartItem[0];
                for (int i = 0; i < cartItem[1]; i++)
                {
                    var book = DBBookSea.Book.Single(m => m.Id == pId);
                    var price = DBBookSea.PriceList.Single(m => m.TheBook == pId && m.TheCustomerType == ovm.curCustomer.CustomerType);
                    ovm.orders.Add(new OrderInfo { theBook = book.Id, price = (double)book.Price, realPrice = (double)price.RealPrice, bookName = book.BookName, bookFeature = book.BookFeature, smallImg = book.SmallImg });
                    ovm.receivers.Add(new Consigenn());
                    ovm.words.Add(new CustomerWords());
                    ovm.payment.Amount += price.RealPrice;
                }
            }
            return View("Order", ovm);
        }








        [HttpPost]
        public ActionResult Index(OrderViewModel ovm)
        {
            ViewBag.Request = Request;
            Customer curCust = DBBookSea.Customer.Single(m => m.Id == ovm.curCustomer.Id);
            if (curCust.MobilePhone != ovm.curCustomer.MobilePhone && ovm.curCustomer.MobilePhone != "")
            {
                curCust.MobilePhone = ovm.curCustomer.MobilePhone;
            }
            if (curCust.HomePhone != ovm.curCustomer.HomePhone && ovm.curCustomer.HomePhone != "")
            {
                curCust.HomePhone = ovm.curCustomer.HomePhone;
            }

            if (curCust.OfficePhone != ovm.curCustomer.OfficePhone && ovm.curCustomer.OfficePhone != "")
            {
                curCust.OfficePhone = ovm.curCustomer.OfficePhone;
            }

            if (curCust.Email != ovm.curCustomer.Email && ovm.curCustomer.Email != "")
            {
                curCust.Email = ovm.curCustomer.Email;
            }

            if (curCust.QqNumber != ovm.curCustomer.QqNumber && ovm.curCustomer.QqNumber != "")
            {
                curCust.QqNumber = ovm.curCustomer.QqNumber;
            }
            DBBookSea.SaveChanges();
            bool succeed = true;
            int payId = 0;
            int curZip;
            try
            {
                Payment p = DBBookSea.Payment.Add(new Payment()).Entity;
                p.Amount = double.Parse(Request.Form["paymentAmt"]);
                p.ThePaymentType = int.Parse(Request.Form["paymentType"]);
                p.PaymentState = 0;

                for (int i = 0; i < ovm.orderQty; i++)
                {
                    Consigenn cons = DBBookSea.Consigenn.Add(new Consigenn()).Entity;
                    cons.AreaName = int.Parse(Request.Form["selDist_" + i]);
                    cons.Customer = curCust.Id;
                    cons.Name = Request.Form["name_" + i].ToString().Trim();
                    cons.RoadName = Request.Form["road_" + i].ToString().Trim();
                    cons.StreetName = Request.Form["street_" + i].ToString().Trim();
                    cons.DoorNumber = Request.Form["door_" + i].ToString().Trim();
                    if (int.TryParse(Request.Form["zip_" + i].ToString().Trim(), out curZip))
                    {
                        cons.ZipCode = curZip;
                    }
                    cons.MobilePhone = Request.Form["mobile_" + i].ToString().Trim();
                    cons.HomePhone = Request.Form["home_" + i].ToString().Trim();
                    cons.OfficePhone = Request.Form["office_" + i].ToString().Trim();
                    cons.Email = Request.Form["email_" + i].ToString().Trim();
                    cons.Qqnumber = Request.Form["QQ_" + i].ToString().Trim();
                    TheOrder o = DBBookSea.TheOrder.Add(new TheOrder()).Entity;
                    o.ThePayment = p.Id;
                    o.TheConsignee = cons.Id;
                    o.Customer = curCust.Id;
                    o.TheBook = int.Parse(Request.Form["productId_" + i].ToString().Trim());
                    o.OrderState = 0;
                    o.OrderTime = DateTime.Now;
                    o.Amt = double.Parse(Request.Form["realPrice_" + i].ToString().Trim());
                    if (Request.Form["sendWord_" + i].ToString().Trim() != "")
                    {
                        CustomerWords cw = DBBookSea.CustomerWords.Add(new CustomerWords()).Entity;
                        cw.TheOrder = o.Id;
                        cw.Words = Request.Form["sendWord_" + i].ToString().Trim();
                    }
                }
                DBBookSea.SaveChanges();//报错   
                    payId = p.Id;
              //  }

            }
            catch(Exception e)
            {
                throw (e);
                succeed = false;
                Response.WriteAsync(e.ToString()/*"<script>alert('数据未成功保存，请重新尝试！');</script>"*/);
            }
            if (succeed)
            {
                //此为写支付处理的代码
                string paymentUrl = "", paymentMethod = "";
                foreach (PaymentType pt in DBBookSea.PaymentType.Where(m => m.Id > 0).ToArray<PaymentType>())
                {
                    if (pt.Id == int.Parse(Request.Form["paymentType"]))
                    {
                        paymentUrl = pt.Url;
                        paymentMethod = pt.MethodName;
                        break;
                    }
                }
                PayRequestInfo pri = new PayRequestInfo();
                pri.Amt = Request.Form["paymentAmt"];
                pri.MerId = "Team02";
                pri.MerTransId = payId.ToString();
                pri.PaymentTypeObjId = Request.Form["paymentType"];  
                pri.PostUrl = "http://payportal.chinacloudsites.cn";
                pri.ReturnUrl = "http://" + Request.Host + Url.Action("Index", "Payment");
                pri.CheckValue = RemotePost.getCheckValue(host.WebRootPath,pri.MerId, pri.ReturnUrl, pri.PaymentTypeObjId, pri.Amt, pri.MerTransId);
                return View("PayRequest", pri);
            }
            else
            {
                //如果未能成功保存数据则执行以下行。由于ovm中未能将原来的order等数据带回，这里要重新获取
                ovm.orders = new List<OrderInfo>();
                ovm.receivers = new List<Consigenn>();
                ovm.words = new List<CustomerWords>();
                ovm.payment = new Payment();
                //获取信息以显示在页面
                ViewBag.payments = DBBookSea.PaymentType.Where(m => m.Id > 0).ToArray<PaymentType>();
                List<int[]> curCart = HttpContext.Session.GetJson<List<int[]>>("Cart");
                ovm.orderQty = 0;
                ovm.payment.Amount = 0.0;
                foreach (var cartItem in curCart)
                {
                    ovm.orderQty += cartItem[1];
                    int pId = cartItem[0];
                    for (int i = 0; i < cartItem[1]; i++)
                    {
                        var product = DBBookSea.Book.Single(m => m.Id == pId);
                        var price = DBBookSea.PriceList.Single(m => m.TheBook == pId && m.TheCustomerType == ovm.curCustomer.CustomerType);
                        ovm.orders.Add(new OrderInfo { theBook = product.Id, price = (double)product.Price, realPrice = (double)price.RealPrice, bookName = product.BookName, bookFeature = product.BookFeature, smallImg = product.SmallImg });
                        ovm.receivers.Add(new Consigenn());
                        ovm.words.Add(new CustomerWords());
                        ovm.payment.Amount += price.RealPrice;
                    }
                }
                return View("Order", ovm);
            }
        }
        //该方法由ajax调用
        public void getProvinces()
        {
            var provinces = from ct in DBBookSea.Province where ct.Id != 0 select new { ct.Id, ct.Name };
            Response.ContentType = "text/plain";
            Response.WriteAsync(JsonConvert.SerializeObject(provinces));
        }
        //该方法由ajax调用
        public void getCities(int id)
        {
            var cities = from ct in DBBookSea.City where ct.Province == id select new { ct.Id, ct.Name };
            Response.ContentType = "text/plain";
            Response.WriteAsync(JsonConvert.SerializeObject(cities));
        }
        //该方法由ajax调用
        public void getDistricts(int id)
        {
            var dists = from ct in DBBookSea.Area where ct.TheCity == id select new { ct.Id, ct.Name };
            Response.ContentType = "text/plain";
            Response.WriteAsync(JsonConvert.SerializeObject(dists));
        }


    }
}
