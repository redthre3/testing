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
    public class RejectedRestaurantsController : Controller
    {
        private readonly FoodFightContext _context;

        public RejectedRestaurantsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: RejectedRestaurants
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.RejectedRestaurants.Include(r => r.SwipeList);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: RejectedRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejectedRestaurant = await _context.RejectedRestaurants
                .Include(r => r.SwipeList)
                .FirstOrDefaultAsync(m => m.RejectedRestaurantId == id);
            if (rejectedRestaurant == null)
            {
                return NotFound();
            }

            return View(rejectedRestaurant);
        }

        // GET: RejectedRestaurants/Create
        public IActionResult Create()
        {
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId");
            return View();
        }

        // POST: RejectedRestaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RejectedRestaurantId,DateTime,SwipeListId,UserId")] RejectedRestaurant rejectedRestaurant)
        {
            if (ModelState.IsValid)
            {
                rejectedRestaurant.RejectedRestaurantId = Guid.NewGuid();
                _context.Add(rejectedRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId", rejectedRestaurant.SwipeListId);
            return View(rejectedRestaurant);
        }

        // GET: RejectedRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejectedRestaurant = await _context.RejectedRestaurants.FindAsync(id);
            if (rejectedRestaurant == null)
            {
                return NotFound();
            }
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId", rejectedRestaurant.SwipeListId);
            return View(rejectedRestaurant);
        }

        // POST: RejectedRestaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RejectedRestaurantId,DateTime,SwipeListId,UserId")] RejectedRestaurant rejectedRestaurant)
        {
            if (id != rejectedRestaurant.RejectedRestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rejectedRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RejectedRestaurantExists(rejectedRestaurant.RejectedRestaurantId))
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
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId", rejectedRestaurant.SwipeListId);
            return View(rejectedRestaurant);
        }

        // GET: RejectedRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rejectedRestaurant = await _context.RejectedRestaurants
                .Include(r => r.SwipeList)
                .FirstOrDefaultAsync(m => m.RejectedRestaurantId == id);
            if (rejectedRestaurant == null)
            {
                return NotFound();
            }

            return View(rejectedRestaurant);
        }

        // POST: RejectedRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rejectedRestaurant = await _context.RejectedRestaurants.FindAsync(id);
            _context.RejectedRestaurants.Remove(rejectedRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RejectedRestaurantExists(Guid id)
        {
            return _context.RejectedRestaurants.Any(e => e.RejectedRestaurantId == id);
        }
    }
}
