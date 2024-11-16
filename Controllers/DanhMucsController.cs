using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHM.Data;
using WebHM.Models;

namespace WebHM.Controllers
{
    public class DanhMucsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DanhMucsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DanhMucs
        public async Task<IActionResult> Index()
        {
              return _context.DanhMucs != null ? 
                          View(await _context.DanhMucs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DanhMucs'  is null.");
        }

        // GET: DanhMucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DanhMucs == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // GET: DanhMucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDanhMuc,TenDanhMuc,MoTa")] DanhMuc danhMuc)
        {

                _context.Add(danhMuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
    
            return View(danhMuc);
        }

        // GET: DanhMucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DanhMucs == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        // POST: DanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDanhMuc,TenDanhMuc,MoTa")] DanhMuc danhMuc)
        {
            if (id != danhMuc.MaDanhMuc)
            {
                return NotFound();
            }


                try
                {
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.MaDanhMuc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            return View(danhMuc);
        }

        // GET: DanhMucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DanhMucs == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // POST: DanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DanhMucs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DanhMucs'  is null.");
            }
            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc != null)
            {
                _context.DanhMucs.Remove(danhMuc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucExists(int id)
        {
          return (_context.DanhMucs?.Any(e => e.MaDanhMuc == id)).GetValueOrDefault();
        }
    }
}
