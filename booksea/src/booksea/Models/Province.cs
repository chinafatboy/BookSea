using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class Province
    {
        public Province()
        {
            City = new HashSet<City>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> City { get; set; }
    }
}
