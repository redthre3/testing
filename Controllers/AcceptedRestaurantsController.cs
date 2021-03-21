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
    public class AcceptedRestaurantsController : Controller
    {
        private readonly FoodFightContext _context;

        public AcceptedRestaurantsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: AcceptedRestaurants
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.AcceptedRestaurants.Include(a => a.SwipeList);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: AcceptedRestaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acceptedRestaurant = await _context.AcceptedRestaurants
                .Include(a => a.SwipeList)
                .FirstOrDefaultAsync(m => m.AcceptedRestaurantId == id);
            if (acceptedRestaurant == null)
            {
                return NotFound();
            }

            return View(acceptedRestaurant);
        }

        // GET: AcceptedRestaurants/Create
        public IActionResult Create()
        {
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId");
            return View();
        }

        // POST: AcceptedRestaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AcceptedRestaurantId,SwipeListId,DateTime,UserId")] AcceptedRestaurant acceptedRestaurant)
        {
            if (ModelState.IsValid)
            {
                acceptedRestaurant.AcceptedRestaurantId = Guid.NewGuid();
                _context.Add(acceptedRestaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId", acceptedRestaurant.SwipeListId);
            return View(acceptedRestaurant);
        }

        // GET: AcceptedRestaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acceptedRestaurant = await _context.AcceptedRestaurants.FindAsync(id);
            if (acceptedRestaurant == null)
            {
                return NotFound();
            }
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId", acceptedRestaurant.SwipeListId);
            return View(acceptedRestaurant);
        }

        // POST: AcceptedRestaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AcceptedRestaurantId,SwipeListId,DateTime,UserId")] AcceptedRestaurant acceptedRestaurant)
        {
            if (id != acceptedRestaurant.AcceptedRestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acceptedRestaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcceptedRestaurantExists(acceptedRestaurant.AcceptedRestaurantId))
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
            ViewData["SwipeListId"] = new SelectList(_context.SwipeLists, "SwipeListId", "RestaurantId", acceptedRestaurant.SwipeListId);
            return View(acceptedRestaurant);
        }

        // GET: AcceptedRestaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acceptedRestaurant = await _context.AcceptedRestaurants
                .Include(a => a.SwipeList)
                .FirstOrDefaultAsync(m => m.AcceptedRestaurantId == id);
            if (acceptedRestaurant == null)
            {
                return NotFound();
            }

            return View(acceptedRestaurant);
        }

        // POST: AcceptedRestaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var acceptedRestaurant = await _context.AcceptedRestaurants.FindAsync(id);
            _context.AcceptedRestaurants.Remove(acceptedRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcceptedRestaurantExists(Guid id)
        {
            return _context.AcceptedRestaurants.Any(e => e.AcceptedRestaurantId == id);
        }
    }
}
