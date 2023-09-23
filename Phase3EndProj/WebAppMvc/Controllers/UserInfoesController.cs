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
    public class UserInfoesController : Controller
    {
        private readonly CustomerLogInfoContext _context;

        public UserInfoesController(CustomerLogInfoContext context)
        {
            _context = context;
        }


        // GET: UserInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInfoes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Password")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(userInfo);
        }
        private bool UserInfoExists(int id)
        {
            return (_context.UserInfos?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        
        public void AddUser(UserInfo userInfo)
        {
            if (userInfo == null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }

            // You can perform additional validation if needed

            try
            {
                // Add the user to the UserInfo DbSet
                _context.UserInfos.Add(userInfo);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle any database-specific exceptions here
                throw new Exception("An error occurred while adding the user to the database.", ex);
            }
        }
    }
}
