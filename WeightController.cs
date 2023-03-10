using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyTracker.Data;
using BodyTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace BodyTracker.Controllers
{
    public class WeightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeightController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weight
        public async Task<IActionResult> Index()
        {
              return _context.DailyWeight != null ? 
                          View(await _context.DailyWeight.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DailyWeight'  is null.");
        }

        // GET: Weight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DailyWeight == null)
            {
                return NotFound();
            }

            var dailyWeight = await _context.DailyWeight
                .FirstOrDefaultAsync(m => m.id == id);
            if (dailyWeight == null)
            {
                return NotFound();
            }

            return View(dailyWeight);
        }

        // GET: Weight/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,date,weight,lossgain,totalloss,target,deviation,bmi")] DailyWeight dailyWeight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyWeight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyWeight);
        }

        // GET: Weight/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DailyWeight == null)
            {
                return NotFound();
            }

            var dailyWeight = await _context.DailyWeight.FindAsync(id);
            if (dailyWeight == null)
            {
                return NotFound();
            }
            return View(dailyWeight);
        }

        // POST: Weight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,date,weight,lossgain,totalloss,target,deviation,bmi")] DailyWeight dailyWeight)
        {
            if (id != dailyWeight.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyWeight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyWeightExists(dailyWeight.id))
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
            return View(dailyWeight);
        }

        // GET: Weight/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DailyWeight == null)
            {
                return NotFound();
            }

            var dailyWeight = await _context.DailyWeight
                .FirstOrDefaultAsync(m => m.id == id);
            if (dailyWeight == null)
            {
                return NotFound();
            }

            return View(dailyWeight);
        }

        // POST: Weight/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DailyWeight == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DailyWeight'  is null.");
            }
            var dailyWeight = await _context.DailyWeight.FindAsync(id);
            if (dailyWeight != null)
            {
                _context.DailyWeight.Remove(dailyWeight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyWeightExists(int id)
        {
          return (_context.DailyWeight?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
