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
    public class BlockedUsersController : Controller
    {
        private readonly FoodFightContext _context;

        public BlockedUsersController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: BlockedUsers
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.BlockedUsers.Include(b => b.BaseUser).Include(b => b.BlockedUserNavigation);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: BlockedUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockedUser = await _context.BlockedUsers
                .Include(b => b.BaseUser)
                .Include(b => b.BlockedUserNavigation)
                .FirstOrDefaultAsync(m => m.BlockUserId == id);
            if (blockedUser == null)
            {
                return NotFound();
            }

            return View(blockedUser);
        }

        // GET: BlockedUsers/Create
        public IActionResult Create()
        {
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email");
            ViewData["BlockedUserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: BlockedUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlockUserId,BaseUserId,BlockedUserId")] BlockedUser blockedUser)
        {
            if (ModelState.IsValid)
            {
                blockedUser.BlockUserId = Guid.NewGuid();
                _context.Add(blockedUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email", blockedUser.BaseUserId);
            ViewData["BlockedUserId"] = new SelectList(_context.Users, "UserId", "Email", blockedUser.BlockedUserId);
            return View(blockedUser);
        }

        // GET: BlockedUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockedUser = await _context.BlockedUsers.FindAsync(id);
            if (blockedUser == null)
            {
                return NotFound();
            }
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email", blockedUser.BaseUserId);
            ViewData["BlockedUserId"] = new SelectList(_context.Users, "UserId", "Email", blockedUser.BlockedUserId);
            return View(blockedUser);
        }

        // POST: BlockedUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BlockUserId,BaseUserId,BlockedUserId")] BlockedUser blockedUser)
        {
            if (id != blockedUser.BlockUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blockedUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockedUserExists(blockedUser.BlockUserId))
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
            ViewData["BaseUserId"] = new SelectList(_context.Users, "UserId", "Email", blockedUser.BaseUserId);
            ViewData["BlockedUserId"] = new SelectList(_context.Users, "UserId", "Email", blockedUser.BlockedUserId);
            return View(blockedUser);
        }

        // GET: BlockedUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockedUser = await _context.BlockedUsers
                .Include(b => b.BaseUser)
                .Include(b => b.BlockedUserNavigation)
                .FirstOrDefaultAsync(m => m.BlockUserId == id);
            if (blockedUser == null)
            {
                return NotFound();
            }

            return View(blockedUser);
        }

        // POST: BlockedUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var blockedUser = await _context.BlockedUsers.FindAsync(id);
            _context.BlockedUsers.Remove(blockedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockedUserExists(Guid id)
        {
            return _context.BlockedUsers.Any(e => e.BlockUserId == id);
        }
    }
}
