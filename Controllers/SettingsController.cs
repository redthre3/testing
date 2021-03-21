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
    public class SettingsController : Controller
    {
        private readonly FoodFightContext _context;

        public SettingsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: Settings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }

        // GET: Settings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .FirstOrDefaultAsync(m => m.SettingsId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SettingsId,Name,Enabled")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                setting.SettingsId = Guid.NewGuid();
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SettingsId,Name,Enabled")] Setting setting)
        {
            if (id != setting.SettingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.SettingsId))
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
            return View(setting);
        }

        // GET: Settings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .FirstOrDefaultAsync(m => m.SettingsId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var setting = await _context.Settings.FindAsync(id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SettingExists(Guid id)
        {
            return _context.Settings.Any(e => e.SettingsId == id);
        }
    }
}
