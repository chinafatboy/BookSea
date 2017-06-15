using System;
using System.Collections.Generic;

namespace booksea.Models
{
    public partial class Role
    {
        public Role()
        {
            UserInfo = new HashSet<UserInfo>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
