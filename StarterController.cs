using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BodyTracker.Data;
using BodyTracker.Models;

namespace BodyTracker.Controllers
{
    public class StarterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StarterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Starter
        public async Task<IActionResult> Index()
        {
              return _context.StarterWeight != null ? 
                          View(await _context.StarterWeight.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StarterWeight'  is null.");
        }

        // GET: Starter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StarterWeight == null)
            {
                return NotFound();
            }

            var starterWeight = await _context.StarterWeight
                .FirstOrDefaultAsync(m => m.id == id);
            if (starterWeight == null)
            {
                return NotFound();
            }

            return View(starterWeight);
        }

        // GET: Starter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Starter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,startdate,startweight,startbmi,targetweight,height")] StarterWeight starterWeight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(starterWeight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(starterWeight);
        }

        // GET: Starter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StarterWeight == null)
            {
                return NotFound();
            }

            var starterWeight = await _context.StarterWeight.FindAsync(id);
            if (starterWeight == null)
            {
                return NotFound();
            }
            return View(starterWeight);
        }

        // POST: Starter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,startdate,startweight,startbmi,targetweight,height")] StarterWeight starterWeight)
        {
            if (id != starterWeight.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(starterWeight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarterWeightExists(starterWeight.id))
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
            return View(starterWeight);
        }

        // GET: Starter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StarterWeight == null)
            {
                return NotFound();
            }

            var starterWeight = await _context.StarterWeight
                .FirstOrDefaultAsync(m => m.id == id);
            if (starterWeight == null)
            {
                return NotFound();
            }

            return View(starterWeight);
        }

        // POST: Starter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StarterWeight == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StarterWeight'  is null.");
            }
            var starterWeight = await _context.StarterWeight.FindAsync(id);
            if (starterWeight != null)
            {
                _context.StarterWeight.Remove(starterWeight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarterWeightExists(int id)
        {
          return (_context.StarterWeight?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
