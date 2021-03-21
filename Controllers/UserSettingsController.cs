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
    public class UserSettingsController : Controller
    {
        private readonly FoodFightContext _context;

        public UserSettingsController(FoodFightContext context)
        {
            _context = context;
        }

        // GET: UserSettings
        public async Task<IActionResult> Index()
        {
            var foodFightContext = _context.UserSettings.Include(u => u.Settings).Include(u => u.User);
            return View(await foodFightContext.ToListAsync());
        }

        // GET: UserSettings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings
                .Include(u => u.Settings)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserSettingsId == id);
            if (userSetting == null)
            {
                return NotFound();
            }

            return View(userSetting);
        }

        // GET: UserSettings/Create
        public IActionResult Create()
        {
            ViewData["SettingsId"] = new SelectList(_context.Settings, "SettingsId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: UserSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserSettingsId,SettingsId,UserId")] UserSetting userSetting)
        {
            if (ModelState.IsValid)
            {
                userSetting.UserSettingsId = Guid.NewGuid();
                _context.Add(userSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SettingsId"] = new SelectList(_context.Settings, "SettingsId", "Name", userSetting.SettingsId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", userSetting.UserId);
            return View(userSetting);
        }

        // GET: UserSettings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings.FindAsync(id);
            if (userSetting == null)
            {
                return NotFound();
            }
            ViewData["SettingsId"] = new SelectList(_context.Settings, "SettingsId", "Name", userSetting.SettingsId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", userSetting.UserId);
            return View(userSetting);
        }

        // POST: UserSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserSettingsId,SettingsId,UserId")] UserSetting userSetting)
        {
            if (id != userSetting.UserSettingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSettingExists(userSetting.UserSettingsId))
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
            ViewData["SettingsId"] = new SelectList(_context.Settings, "SettingsId", "Name", userSetting.SettingsId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", userSetting.UserId);
            return View(userSetting);
        }

        // GET: UserSettings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings
                .Include(u => u.Settings)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserSettingsId == id);
            if (userSetting == null)
            {
                return NotFound();
            }

            return View(userSetting);
        }

        // POST: UserSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userSetting = await _context.UserSettings.FindAsync(id);
            _context.UserSettings.Remove(userSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSettingExists(Guid id)
        {
            return _context.UserSettings.Any(e => e.UserSettingsId == id);
        }
    }
}
