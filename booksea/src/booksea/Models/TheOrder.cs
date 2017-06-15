using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class TheOrder
    {
        public TheOrder()
        {
            CustomerWords = new HashSet<CustomerWords>();
            Receipt = new HashSet<Receipt>();
        }
        public int Id { get; set; }
        public int? Customer { get; set; }
        public DateTime? OrderTime { get; set; }
        public int? TheBook { get; set; }
        public double? Amt { get; set; }
        public int? ThePayment { get; set; }
        public int? TheConsignee { get; set; }
        public int? Theclerk { get; set; }
        public int? TheDeliverer { get; set; }
        public int? OrderState { get; set; }


        public virtual ICollection<CustomerWords> CustomerWords { get; set; }
        public virtual ICollection<Receipt> Receipt { get; set; }
        public virtual UserInfo TheClerkNavigation { get; set; }
        public virtual Consigenn TheConsigneeNavigation { get; set; }
        public virtual Customer TheCustomerNavigation { get; set; }
        public virtual UserInfo TheDelivererNavigation { get; set; }
        public virtual Payment ThePaymentNavigation { get; set; }
        public virtual Book TheProductNavigation { get; set; }
    }
}
