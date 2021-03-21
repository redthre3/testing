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
    public class FavoriteRestaurantsController : Controller
    {
        private readonly FoodFightContext _context;

        public FavoriteRestaurantsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: FavoriteRestaurants
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.FavoriteRestaurants.Include(f => f.Restaurant).Include(f => f.User);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: FavoriteRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteRestaurant = await _context.FavoriteRestaurants
                .Include(f => f.Restaurant)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FavoriteRestaurantId == id);
            if (favoriteRestaurant == null)
            {
                return NotFound();
            }

            return View(favoriteRestaurant);
        }

        // GET: FavoriteRestaurants/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: FavoriteRestaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FavoriteRestaurantId,RestaurantId,UserId")] FavoriteRestaurant favoriteRestaurant)
        {
            if (ModelState.IsValid)
            {
                favoriteRestaurant.FavoriteRestaurantId = Guid.NewGuid();
                _context.Add(favoriteRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", favoriteRestaurant.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", favoriteRestaurant.UserId);
            return View(favoriteRestaurant);
        }

        // GET: FavoriteRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteRestaurant = await _context.FavoriteRestaurants.FindAsync(id);
            if (favoriteRestaurant == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", favoriteRestaurant.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", favoriteRestaurant.UserId);
            return View(favoriteRestaurant);
        }

        // POST: FavoriteRestaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FavoriteRestaurantId,RestaurantId,UserId")] FavoriteRestaurant favoriteRestaurant)
        {
            if (id != favoriteRestaurant.FavoriteRestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteRestaurantExists(favoriteRestaurant.FavoriteRestaurantId))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", favoriteRestaurant.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", favoriteRestaurant.UserId);
            return View(favoriteRestaurant);
        }

        // GET: FavoriteRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteRestaurant = await _context.FavoriteRestaurants
                .Include(f => f.Restaurant)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FavoriteRestaurantId == id);
            if (favoriteRestaurant == null)
            {
                return NotFound();
            }

            return View(favoriteRestaurant);
        }

        // POST: FavoriteRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var favoriteRestaurant = await _context.FavoriteRestaurants.FindAsync(id);
            _context.FavoriteRestaurants.Remove(favoriteRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteRestaurantExists(Guid id)
        {
            return _context.FavoriteRestaurants.Any(e => e.FavoriteRestaurantId == id);
        }
    }
}
