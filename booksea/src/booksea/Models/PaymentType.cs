using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Payment = new HashSet<Payment>();
        }
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Url { get; set; }
        public string MethodName { get; set; }
        public string SmallImg { get; set; }
        public string BigImg { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
