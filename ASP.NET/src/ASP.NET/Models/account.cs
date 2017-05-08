using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ASP.NET.Models
{
    public class account
    {
        [Required(ErrorMessage = "账号不能为空！")]
        public String username { get; set; }
        [Required(ErrorMessage = "密码不能为空！")]
        public String password { get; set; }
        [Required(ErrorMessage = "电子邮箱不能为空！")]
        public String eaddress { get; set; }
    }
}
