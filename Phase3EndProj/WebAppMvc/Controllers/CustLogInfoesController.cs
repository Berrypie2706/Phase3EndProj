using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    public class CustLogInfoesController : Controller
    {
        private readonly CustomerLogInfoContext _context;

        public CustLogInfoesController(CustomerLogInfoContext context)
        {
            _context = context;
        }

        // GET: CustLogInfoes
        public async Task<IActionResult> Index()
        {
            var customerLogInfoContext = _context.CustLogInfos.Include(c => c.UserInfoNavigation);
            return View(await customerLogInfoContext.ToListAsync());
        }

        // GET: CustLogInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustLogInfos == null)
            {
                return NotFound();
            }

            var custLogInfo = await _context.CustLogInfos
                .Include(c => c.UserInfoNavigation)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (custLogInfo == null)
            {
                return NotFound();
            }

            return View(custLogInfo);
        }

        // GET: CustLogInfoes/Create
        public IActionResult Create()
        {
            ViewData["UserInfo"] = new SelectList(_context.UserInfos, "UserId", "UserId");
            return View();
        }

        // POST: CustLogInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,CustEmail,CustName,LogStatus,UserInfo")] CustLogInfo custLogInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(custLogInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserInfo"] = new SelectList(_context.UserInfos, "UserId", "UserId", custLogInfo.UserInfo);
            return View(custLogInfo);
        }

        // GET: CustLogInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustLogInfos == null)
            {
                return NotFound();
            }

            var custLogInfo = await _context.CustLogInfos.FindAsync(id);
            if (custLogInfo == null)
            {
                return NotFound();
            }
            ViewData["UserInfo"] = new SelectList(_context.UserInfos, "UserId", "UserId", custLogInfo.UserInfo);
            return View(custLogInfo);
        }

        // POST: CustLogInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,CustEmail,CustName,LogStatus,UserInfo")] CustLogInfo custLogInfo)
        {
            if (id != custLogInfo.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(custLogInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustLogInfoExists(custLogInfo.LogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserInfo"] = new SelectList(_context.UserInfos, "UserId", "UserId", custLogInfo.UserInfo);
            return View(custLogInfo);
        }

        // GET: CustLogInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustLogInfos == null)
            {
                return NotFound();
            }

            var custLogInfo = await _context.CustLogInfos
                .Include(c => c.UserInfoNavigation)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (custLogInfo == null)
            {
                return NotFound();
            }

            return View(custLogInfo);
        }

        // POST: CustLogInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustLogInfos == null)
            {
                return Problem("Entity set 'CustomerLogInfoContext.CustLogInfos'  is null.");
            }
            var custLogInfo = await _context.CustLogInfos.FindAsync(id);
            if (custLogInfo != null)
            {
                _context.CustLogInfos.Remove(custLogInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustLogInfoExists(int id)
        {
          return (_context.CustLogInfos?.Any(e => e.LogId == id)).GetValueOrDefault();
        }
    }
}
