using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoListsController : Controller
    {
        private readonly ToDoContext _context;

        public ToDoListsController(ToDoContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(int id, bool isDone)
        {
            var item = await _context.toDoLists.FirstOrDefaultAsync(i => i.ToDoId == id);
            if(item != null)
            {
                item.IsDone = isDone;
                _context.Update(item);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        // GET: ToDoLists
        public async Task<IActionResult> Index()
        {
              return _context.toDoLists != null ? 
                          View(await _context.toDoLists.ToListAsync()) :
                          Problem("Entity set 'ToDoContext.toDoLists'  is null.");
        }

        // GET: ToDoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.toDoLists == null)
            {
                return NotFound();
            }

            var toDoList = await _context.toDoLists
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoId,ToDoListItem,Created,DueDate,IsDone")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.toDoLists == null)
            {
                return NotFound();
            }

            var toDoList = await _context.toDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoId,ToDoListItem,Created,DueDate,IsDone")] ToDoList toDoList)
        {
            if (id != toDoList.ToDoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoListExists(toDoList.ToDoId))
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
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.toDoLists == null)
            {
                return NotFound();
            }

            var toDoList = await _context.toDoLists
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.toDoLists == null)
            {
                return Problem("Entity set 'ToDoContext.toDoLists'  is null.");
            }
            var toDoList = await _context.toDoLists.FindAsync(id);
            if (toDoList != null)
            {
                _context.toDoLists.Remove(toDoList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoListExists(int id)
        {
          return (_context.toDoLists?.Any(e => e.ToDoId == id)).GetValueOrDefault();
        }
    }
}
