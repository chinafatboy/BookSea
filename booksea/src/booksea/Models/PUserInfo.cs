using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class PUserInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Sex { get; set; }
        public string Tellphine { get; set; }
        public string Address { get; set; }
        public DateTime? RegiterTime { get; set; }
        public string Email { get; set; }
        public string Postcodes { get; set; }
        public int StatusId { get; set; }
    }
}
