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
    public class BlockedRestaurantsController : Controller
    {
        private readonly FoodFightContext _context;

        public BlockedRestaurantsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: BlockedRestaurants
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.BlockedRestaurants.Include(b => b.Restaurant).Include(b => b.User);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: BlockedRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockedRestaurant = await _context.BlockedRestaurants
                .Include(b => b.Restaurant)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlockedRestaurantId == id);
            if (blockedRestaurant == null)
            {
                return NotFound();
            }

            return View(blockedRestaurant);
        }

        // GET: BlockedRestaurants/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: BlockedRestaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlockedRestaurantId,RestaurantId,UserId")] BlockedRestaurant blockedRestaurant)
        {
            if (ModelState.IsValid)
            {
                blockedRestaurant.BlockedRestaurantId = Guid.NewGuid();
                _context.Add(blockedRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", blockedRestaurant.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", blockedRestaurant.UserId);
            return View(blockedRestaurant);
        }

        // GET: BlockedRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockedRestaurant = await _context.BlockedRestaurants.FindAsync(id);
            if (blockedRestaurant == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", blockedRestaurant.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", blockedRestaurant.UserId);
            return View(blockedRestaurant);
        }

        // POST: BlockedRestaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BlockedRestaurantId,RestaurantId,UserId")] BlockedRestaurant blockedRestaurant)
        {
            if (id != blockedRestaurant.BlockedRestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blockedRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockedRestaurantExists(blockedRestaurant.BlockedRestaurantId))
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", blockedRestaurant.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", blockedRestaurant.UserId);
            return View(blockedRestaurant);
        }

        // GET: BlockedRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockedRestaurant = await _context.BlockedRestaurants
                .Include(b => b.Restaurant)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlockedRestaurantId == id);
            if (blockedRestaurant == null)
            {
                return NotFound();
            }

            return View(blockedRestaurant);
        }

        // POST: BlockedRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var blockedRestaurant = await _context.BlockedRestaurants.FindAsync(id);
            _context.BlockedRestaurants.Remove(blockedRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockedRestaurantExists(Guid id)
        {
            return _context.BlockedRestaurants.Any(e => e.BlockedRestaurantId == id);
        }
    }
}
