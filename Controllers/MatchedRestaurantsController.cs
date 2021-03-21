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
    public class MatchedRestaurantsController : Controller
    {
        private readonly FoodFightContext _context;

        public MatchedRestaurantsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: MatchedRestaurants
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.MatchedRestaurants.Include(m => m.AcceptedRestaurant);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: MatchedRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchedRestaurant = await _context.MatchedRestaurants
                .Include(m => m.AcceptedRestaurant)
                .FirstOrDefaultAsync(m => m.MatchRestaurantId == id);
            if (matchedRestaurant == null)
            {
                return NotFound();
            }

            return View(matchedRestaurant);
        }

        // GET: MatchedRestaurants/Create
        public IActionResult Create()
        {
            ViewData["AcceptedRestaurantId"] = new SelectList(_context.AcceptedRestaurants, "AcceptedRestaurantId", "UserId");
            return View();
        }

        // POST: MatchedRestaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchRestaurantId,DateTime,AcceptedRestaurantId")] MatchedRestaurant matchedRestaurant)
        {
            if (ModelState.IsValid)
            {
                matchedRestaurant.MatchRestaurantId = Guid.NewGuid();
                _context.Add(matchedRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcceptedRestaurantId"] = new SelectList(_context.AcceptedRestaurants, "AcceptedRestaurantId", "UserId", matchedRestaurant.AcceptedRestaurantId);
            return View(matchedRestaurant);
        }

        // GET: MatchedRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchedRestaurant = await _context.MatchedRestaurants.FindAsync(id);
            if (matchedRestaurant == null)
            {
                return NotFound();
            }
            ViewData["AcceptedRestaurantId"] = new SelectList(_context.AcceptedRestaurants, "AcceptedRestaurantId", "UserId", matchedRestaurant.AcceptedRestaurantId);
            return View(matchedRestaurant);
        }

        // POST: MatchedRestaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MatchRestaurantId,DateTime,AcceptedRestaurantId")] MatchedRestaurant matchedRestaurant)
        {
            if (id != matchedRestaurant.MatchRestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchedRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchedRestaurantExists(matchedRestaurant.MatchRestaurantId))
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
            ViewData["AcceptedRestaurantId"] = new SelectList(_context.AcceptedRestaurants, "AcceptedRestaurantId", "UserId", matchedRestaurant.AcceptedRestaurantId);
            return View(matchedRestaurant);
        }

        // GET: MatchedRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchedRestaurant = await _context.MatchedRestaurants
                .Include(m => m.AcceptedRestaurant)
                .FirstOrDefaultAsync(m => m.MatchRestaurantId == id);
            if (matchedRestaurant == null)
            {
                return NotFound();
            }

            return View(matchedRestaurant);
        }

        // POST: MatchedRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var matchedRestaurant = await _context.MatchedRestaurants.FindAsync(id);
            _context.MatchedRestaurants.Remove(matchedRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchedRestaurantExists(Guid id)
        {
            return _context.MatchedRestaurants.Any(e => e.MatchRestaurantId == id);
        }
    }
}
