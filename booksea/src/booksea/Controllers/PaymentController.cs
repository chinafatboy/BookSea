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
    public class PaymentController : Controller
    {
        private readonly BookSeaContext DBBookSea;
        private IHostingEnvironment host = null;
        public PaymentController(BookSeaContext _DBBookSea, IHostingEnvironment _host )
        {
            DBBookSea = _DBBookSea;
            host = _host;
        }



        public ActionResult Index()
        {
            string merId, amt, merTransId, transId, transTime;
         //   int paymentTypeId = int.Parse(Request.Form["paymentTypeId"]);
         //   PaymentType paymentMethod = DBBookSea.PaymentType.Single(m => m.Id == paymentTypeId);
            if (RemotePost.PaymentVerify(host.WebRootPath, Request, out merId, out amt, out merTransId, out transId, out transTime) && merId == "Team02")
            {
                Payment pay = DBBookSea.Payment.Single(m => m.Id == int.Parse(merTransId));
                TheOrder[] orders = DBBookSea.TheOrder.Where(m => m.ThePayment == int.Parse(merTransId)).ToArray<TheOrder>();
                pay.TransTime = DateTime.Parse(transTime);
                pay.TransNo = transId;
                foreach (TheOrder or in orders)
                {
                    or.OrderState = 1;
                }
                DBBookSea.SaveChanges();
                ViewBag.paymentMsg = "付款成功！     付款号：" + merTransId.ToString() + "；   金额：" + amt.ToString() + "元。";//付款成功！显示付款信息作为测试。
            }
            return View();
        }



    }
}
