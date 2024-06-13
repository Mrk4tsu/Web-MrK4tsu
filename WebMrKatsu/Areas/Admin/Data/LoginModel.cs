using System.ComponentModel.DataAnnotations;

namespace WebMrKatsu.Areas.Admin.Data
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập password")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}