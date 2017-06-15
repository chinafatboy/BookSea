using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class Consigenn
    {
        public Consigenn()
        {
            TheOrder = new HashSet<TheOrder>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AreaName { get; set; }
        public int Customer { get; set; }
        public string StreetName { get; set; }
        public string RoadName { get; set; }
        public string DoorNumber { get; set; }
        public int? ZipCode { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string OfficePhone { get; set; }
        public string HomePhone { get; set; }
        public string Qqnumber { get; set; }
        public virtual ICollection<TheOrder> TheOrder { get; set; }
        public virtual Area TheAreaNavigation { get; set; }
        public virtual Customer TheCustomerNavigation { get; set; }
    }
}
