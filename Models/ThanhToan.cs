﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebHM.Models
{
    public class ThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }
        public string KhachHangId { get; set; } // Khóa ngoại đến AspNetUsers
        public decimal SoTien { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string PhuongThuc { get; set; } // Ví dụ: thẻ tín dụng, PayPal, v.v.

        // Quan hệ với AspNetUsers
        public IdentityUser KhachHang { get; set; }
    }
}
