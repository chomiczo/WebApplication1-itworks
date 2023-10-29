using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserStatistics
        public async Task<IActionResult> Index()
        {
            // Pobierz dane (userStatistics) z bazy danych
            var userStatistics = _context.UserStatistic.ToList();

            // Przekaz dane do widoku
            return View(userStatistics);

        }

        // GET: UserStatistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserStatistic == null)
            {
                return NotFound();
            }

            var userStatistic = await _context.UserStatistic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userStatistic == null)
            {
                return NotFound();
            }

            return View(userStatistic);
        }


        // GET: UserStatistics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserStatistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ExerciseTypeId,SessionsInLastFourWeeks,BestResult")] UserStatistic userStatistic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userStatistic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userStatistic);
        }

        // GET: UserStatistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserStatistic == null)
            {
                return NotFound();
            }

            var userStatistic = await _context.UserStatistic.FindAsync(id);
            if (userStatistic == null)
            {
                return NotFound();
            }
            return View(userStatistic);
        }

        // POST: UserStatistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ExerciseTypeId,SessionsInLastFourWeeks,BestResult")] UserStatistic userStatistic)
        {
            if (id != userStatistic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userStatistic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStatisticExists(userStatistic.Id))
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
            return View(userStatistic);
        }

        // GET: UserStatistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserStatistic == null)
            {
                return NotFound();
            }

            var userStatistic = await _context.UserStatistic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userStatistic == null)
            {
                return NotFound();
            }

            return View(userStatistic);
        }

        // POST: UserStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserStatistic == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserStatistic'  is null.");
            }
            var userStatistic = await _context.UserStatistic.FindAsync(id);
            if (userStatistic != null)
            {
                _context.UserStatistic.Remove(userStatistic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserStatisticExists(int id)
        {
          return (_context.UserStatistic?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
