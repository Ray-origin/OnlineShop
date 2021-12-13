using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShop.Models
{
    [MetadataType(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            [DisplayName("Mã Thành Viên")]
            public int MaTV { get; set; }

            [DisplayName("Tài Khoản")]
            [StringLength(10, ErrorMessage = "Tài Khoản chỉ chứa tối ta 10 kí tự")]
            [Required(ErrorMessage = " {0} không được bỏ trống")]
            public string TaiKhoan { get; set; }

            [DisplayName("Mật Khẩu")]
            [StringLength(32, ErrorMessage = "Mật KHẩu chỉ chứa tối đa 32 kí tự")]
            public string MatKhau { get; set; }

            [DisplayName("Họ và Tên")]
            public string HoTen { get; set; }

            [DisplayName("Email")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string Email { get; set; }
        }
    }
}