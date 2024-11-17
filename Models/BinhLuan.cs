using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebHM.Models
{
    public class BinhLuan
    {
        [Key]
        public int MaBinhLuan { get; set; }
        public int MaSanPham { get; set; } // Khóa ngoại đến SanPham
        public string KhachHangId { get; set; } // Khóa ngoại đến AspNetUsers
        public string NoiDungBinhLuan { get; set; }
        public DateTime NgayBinhLuan { get; set; }

        // Quan hệ với SanPham và AspNetUsers
        public SanPham SanPham { get; set; }
        public ApplicationUser KhachHang { get; set; }
    }
}
