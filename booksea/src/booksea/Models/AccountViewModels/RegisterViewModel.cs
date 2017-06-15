using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace booksea.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email地址格式不正确.")]
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [RegularExpression(@"^1\d{10}$", ErrorMessage = "移动电话号码不符合格式.")]
        [Display(Name = "移动电话")]
        public string MobilePhone { get; set; }

        [RegularExpression(@"^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$", ErrorMessage = "电话号码格式不正确.")]
        [Display(Name = "办公电话")]
        public string OfficePhone { get; set; }

        [RegularExpression(@"^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$", ErrorMessage = "电话号码格式不正确.")]
        [Display(Name = "住宅电话")]
        public string HomePhone { get; set; }

        [RegularExpression(@"^[1-9]\d{4,9}$", ErrorMessage = "QQ号码不正确.")]
        [Display(Name = "  QQ号码")]
        public string QqNum { get; set; }
    }
}
