using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksea.Models
{
    public class Area
    {
        public Area()
        {
            Consigenn = new HashSet<Consigenn>();
            Division = new HashSet<Division>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TheCity { get; set; }

        public virtual ICollection<Consigenn> Consigenn { get; set; }
        public virtual ICollection<Division> Division { get; set; }
        public virtual City TheCityNavigation { get; set; }
    }
}
