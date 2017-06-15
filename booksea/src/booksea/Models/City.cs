using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class City
    {
        public City()
        {
            Area = new HashSet<Area>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Province { get; set; }
        public virtual ICollection<Area> Area { get; set; }
        public virtual Province TheProvinceNavigation { get; set; }

    }
}
