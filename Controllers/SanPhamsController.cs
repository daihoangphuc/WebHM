using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHM.Data;
using WebHM.Models;

namespace WebHM.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SanPhamsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SanPhams
    public async Task<IActionResult> Index()
    {
        // Khởi tạo truy vấn cơ bản với các liên kết cần thiết
        IQueryable<SanPham> sanPhams = _context.SanPhams
            .Include(s => s.DanhMuc)
            .Include(s => s.NguoiBan);

        // Kiểm tra xem người dùng hiện tại có phải là Seller không
        if (User.IsInRole("Seller"))
        {
            // Lấy ID của người dùng hiện tại
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Lọc sản phẩm chỉ thuộc về Seller đó
            sanPhams = sanPhams.Where(s => s.NguoiBanId == userId);
        }

        // Thực hiện truy vấn và chuyển dữ liệu tới view
        var list = await sanPhams.ToListAsync();
        return View(list);
    }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.NguoiBan)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            // Kiểm tra xem người dùng có phải là Seller không
            if (User.IsInRole("Seller"))
            {
                // Nếu là Seller, chỉ cho phép chọn người dùng hiện tại
                ViewData["NguoiBanId"] = new SelectList(new[] { User.FindFirstValue(ClaimTypes.NameIdentifier) });
            }
            else
            {
                // Nếu là Admin, cho phép chọn tất cả người dùng
                ViewData["NguoiBanId"] = new SelectList(_context.Users, "Id", "UserName");
            }

            // Truyền danh mục vào dropdown
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc");

            return View();
        }



        // POST: SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,TenSanPham,Gia,MoTa,HinhAnh,MaDanhMuc,NguoiBanId")] SanPham sanPham)
        {
            // If the user is a Seller, set NguoiBanId to the current user's ID
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(currentUserId);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Seller"))
                {
                    sanPham.NguoiBanId = currentUserId;
                }
            }

                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
      

            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewData["NguoiBanId"] = new SelectList(_context.Users, "Id", "Id", sanPham.NguoiBanId);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewData["NguoiBanId"] = new SelectList(_context.Users, "Id", "Id", sanPham.NguoiBanId);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSanPham,TenSanPham,Gia,MoTa,HinhAnh,MaDanhMuc,NguoiBanId")] SanPham sanPham)
        {
            if (id != sanPham.MaSanPham)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSanPham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewData["NguoiBanId"] = new SelectList(_context.Users, "Id", "Id", sanPham.NguoiBanId);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .Include(s => s.NguoiBan)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SanPhams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SanPhams'  is null.");
            }
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
          return (_context.SanPhams?.Any(e => e.MaSanPham == id)).GetValueOrDefault();
        }
    }
}
