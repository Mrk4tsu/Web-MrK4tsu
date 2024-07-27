using System.ComponentModel.DataAnnotations;

namespace WebMrKatsu.Areas.Admin.Data
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mời nhập password")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}