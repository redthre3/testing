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
    public class SwipeListsController : Controller
    {
        private readonly FoodFightContext _context;

        public SwipeListsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: SwipeLists
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.SwipeLists.Include(s => s.MatchSession).Include(s => s.Restaurant);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: SwipeLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swipeList = await _context.SwipeLists
                .Include(s => s.MatchSession)
                .Include(s => s.Restaurant)
                .FirstOrDefaultAsync(m => m.SwipeListId == id);
            if (swipeList == null)
            {
                return NotFound();
            }

            return View(swipeList);
        }

        // GET: SwipeLists/Create
        public IActionResult Create()
        {
            ViewData["MatchSessionId"] = new SelectList(_context.MatchSessions, "MatchSessionId", "MatchSessionId");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId");
            return View();
        }

        // POST: SwipeLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SwipeListId,RestaurantId,MatchSessionId")] SwipeList swipeList)
        {
            if (ModelState.IsValid)
            {
                swipeList.SwipeListId = Guid.NewGuid();
                _context.Add(swipeList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchSessionId"] = new SelectList(_context.MatchSessions, "MatchSessionId", "MatchSessionId", swipeList.MatchSessionId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", swipeList.RestaurantId);
            return View(swipeList);
        }

        // GET: SwipeLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swipeList = await _context.SwipeLists.FindAsync(id);
            if (swipeList == null)
            {
                return NotFound();
            }
            ViewData["MatchSessionId"] = new SelectList(_context.MatchSessions, "MatchSessionId", "MatchSessionId", swipeList.MatchSessionId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", swipeList.RestaurantId);
            return View(swipeList);
        }

        // POST: SwipeLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SwipeListId,RestaurantId,MatchSessionId")] SwipeList swipeList)
        {
            if (id != swipeList.SwipeListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swipeList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwipeListExists(swipeList.SwipeListId))
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
            ViewData["MatchSessionId"] = new SelectList(_context.MatchSessions, "MatchSessionId", "MatchSessionId", swipeList.MatchSessionId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", swipeList.RestaurantId);
            return View(swipeList);
        }

        // GET: SwipeLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swipeList = await _context.SwipeLists
                .Include(s => s.MatchSession)
                .Include(s => s.Restaurant)
                .FirstOrDefaultAsync(m => m.SwipeListId == id);
            if (swipeList == null)
            {
                return NotFound();
            }

            return View(swipeList);
        }

        // POST: SwipeLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var swipeList = await _context.SwipeLists.FindAsync(id);
            _context.SwipeLists.Remove(swipeList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwipeListExists(Guid id)
        {
            return _context.SwipeLists.Any(e => e.SwipeListId == id);
        }
    }
}
