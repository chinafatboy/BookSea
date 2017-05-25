using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class PManager
    {
        public int Id { get; set; }
        public string ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Pwd { get; set; }
    }
}
