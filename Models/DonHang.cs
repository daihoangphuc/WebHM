using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebHM.Models
{
    public class DonHang
    {
        [Key]
        public int MaDonHang { get; set; }
        public string KhachHangId { get; set; } // Khóa ngoại đến AspNetUsers (khách hàng)
        public DateTime NgayDatHang { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }

        // Quan hệ với AspNetUsers và ChiTietDonHang
        public IdentityUser KhachHang { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
