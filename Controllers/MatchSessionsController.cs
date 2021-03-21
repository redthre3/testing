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
    public class MatchSessionsController : Controller
    {
        private readonly FoodFightContext _context;

        public MatchSessionsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: MatchSessions
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.MatchSessions.Include(m => m.ConnectedUser);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: MatchSessions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchSession = await _context.MatchSessions
                .Include(m => m.ConnectedUser)
                .FirstOrDefaultAsync(m => m.MatchSessionId == id);
            if (matchSession == null)
            {
                return NotFound();
            }

            return View(matchSession);
        }

        // GET: MatchSessions/Create
        public IActionResult Create()
        {
            ViewData["ConnectedUserId"] = new SelectList(_context.ConnectedUsers, "ConnectedUserId", "ConnectedUserId");
            return View();
        }

        // POST: MatchSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchSessionId,ConnectedUserId,DateTime,Lat,Lng")] MatchSession matchSession)
        {
            if (ModelState.IsValid)
            {
                matchSession.MatchSessionId = Guid.NewGuid();
                _context.Add(matchSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConnectedUserId"] = new SelectList(_context.ConnectedUsers, "ConnectedUserId", "ConnectedUserId", matchSession.ConnectedUserId);
            return View(matchSession);
        }

        // GET: MatchSessions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchSession = await _context.MatchSessions.FindAsync(id);
            if (matchSession == null)
            {
                return NotFound();
            }
            ViewData["ConnectedUserId"] = new SelectList(_context.ConnectedUsers, "ConnectedUserId", "ConnectedUserId", matchSession.ConnectedUserId);
            return View(matchSession);
        }

        // POST: MatchSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MatchSessionId,ConnectedUserId,DateTime,Lat,Lng")] MatchSession matchSession)
        {
            if (id != matchSession.MatchSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchSessionExists(matchSession.MatchSessionId))
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
            ViewData["ConnectedUserId"] = new SelectList(_context.ConnectedUsers, "ConnectedUserId", "ConnectedUserId", matchSession.ConnectedUserId);
            return View(matchSession);
        }

        // GET: MatchSessions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchSession = await _context.MatchSessions
                .Include(m => m.ConnectedUser)
                .FirstOrDefaultAsync(m => m.MatchSessionId == id);
            if (matchSession == null)
            {
                return NotFound();
            }

            return View(matchSession);
        }

        // POST: MatchSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var matchSession = await _context.MatchSessions.FindAsync(id);
            _context.MatchSessions.Remove(matchSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchSessionExists(Guid id)
        {
            return _context.MatchSessions.Any(e => e.MatchSessionId == id);
        }
    }
}
