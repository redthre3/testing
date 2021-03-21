using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodFIghtAdmin.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodFIghtAdmin
{
    [Authorize]
    public class ConnectedUsersController : Controller
    {
        private readonly FoodFightContext _context;

        public ConnectedUsersController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: ConnectedUsers
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.ConnectedUsers.Include(c => c.BaseUser).Include(c => c.FriendUser);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: ConnectedUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectedUser = await _context.ConnectedUsers
                .Include(c => c.BaseUser)
                .Include(c => c.FriendUser)
                .FirstOrDefaultAsync(m => m.ConnectedUserId == id);
            if (connectedUser == null)
            {
                return NotFound();
            }

            return View(connectedUser);
        }

        // GET: ConnectedUsers/Create
        public IActionResult Create()
        {
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["FriendUserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: ConnectedUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConnectedUserId,BaseUserId,FriendUserId")] ConnectedUser connectedUser)
        {
            if (ModelState.IsValid)
            {
                connectedUser.ConnectedUserId = Guid.NewGuid();
                _context.Add(connectedUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email", connectedUser.BaseUserId);
            ViewData["FriendUserId"] = new SelectList(_context.Users, "UserId", "Email", connectedUser.FriendUserId);
            return View(connectedUser);
        }

        // GET: ConnectedUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectedUser = await _context.ConnectedUsers.FindAsync(id);
            if (connectedUser == null)
            {
                return NotFound();
            }
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email", connectedUser.BaseUserId);
            ViewData["FriendUserId"] = new SelectList(_context.Users, "UserId", "Email", connectedUser.FriendUserId);
            return View(connectedUser);
        }

        // POST: ConnectedUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ConnectedUserId,BaseUserId,FriendUserId")] ConnectedUser connectedUser)
        {
            if (id != connectedUser.ConnectedUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(connectedUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConnectedUserExists(connectedUser.ConnectedUserId))
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
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email", connectedUser.BaseUserId);
            ViewData["FriendUserId"] = new SelectList(_context.Users, "UserId", "Email", connectedUser.FriendUserId);
            return View(connectedUser);
        }

        // GET: ConnectedUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connectedUser = await _context.ConnectedUsers
                .Include(c => c.BaseUser)
                .Include(c => c.FriendUser)
                .FirstOrDefaultAsync(m => m.ConnectedUserId == id);
            if (connectedUser == null)
            {
                return NotFound();
            }

            return View(connectedUser);
        }

        // POST: ConnectedUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var connectedUser = await _context.ConnectedUsers.FindAsync(id);
            _context.ConnectedUsers.Remove(connectedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConnectedUserExists(Guid id)
        {
            return _context.ConnectedUsers.Any(e => e.ConnectedUserId == id);
        }
    }
}
