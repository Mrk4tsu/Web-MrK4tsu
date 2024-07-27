using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO.ViewModel
{
    public class UserViewModel
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập.")]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu.")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmParword { get; set; }

        [Display(Name = "Tên hiển thị")]
        [Required(ErrorMessage = "Yêu cầu nhập tên người dùng.")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Yêu cầu nhập email.")]
        public string Email { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }
    }
}
