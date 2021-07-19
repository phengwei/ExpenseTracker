using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ExpenseTrackerContext _context;

        public CategoryController(ExpenseTrackerContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            id = int.Parse(userId);

            var category = await _context.Category
                 .Include(s => s.SubCategory)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.UserID == id);

            if (category == null)
            {
                return RedirectToAction("CreateCategory", "Category");
            }

            return View(category);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        public IActionResult CreateSubCategory()
        {
            return View();
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(
        [Bind("Housing,Transportation,Food,Utilities,Insurance")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var id = int.Parse(userId);

                    category.UserID = id;

                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Category", new { id = 1 });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubCategory(
        [Bind("PersonalSpending,Entertainment")] SubCategory sCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var id = int.Parse(userId);

                    var category = await _context.Category
                     .Include(s => s.SubCategory)
                     .AsNoTracking()
                     .FirstOrDefaultAsync(m => m.UserID == id);

                    sCategory.CategoryID = category.CategoryID;

                    _context.Add(sCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Category", new { id = 1 });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(sCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Category.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryToUpdate = await _context.Category.FirstOrDefaultAsync(s => s.CategoryID == id);
            if (await TryUpdateModelAsync<Category>(
                categoryToUpdate,
                "",
                s => s.Housing, s => s.Transportation, s => s.Food, s => s.Utilities, s => s.Insurance))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Category", new { id = 1 });
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(categoryToUpdate);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.SubCategory.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction("Details", "Category", new { id = 1 });
            }

            try
            {
                _context.SubCategory.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Category", new { id = 1 });
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
            }
        }

    }
}
