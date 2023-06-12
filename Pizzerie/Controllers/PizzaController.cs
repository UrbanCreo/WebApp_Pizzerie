using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzerie.Data;
using Pizzerie.Models;

namespace Pizzerie.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: PizzaController
        public async Task<IActionResult> Index()
        {
            return _context.PizzaViewModel != null ?
                View(await _context.PizzaViewModel.ToListAsync()) :
                Problem("Entity set 'ApplicationDbContext.PizzaViewModel'  is null.");
        }

        // GET: PizzaController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PizzaViewModel == null)
            {
                return NotFound();
            }

            var pizzaViewModel = await _context.PizzaViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaViewModel == null)
            {
                return NotFound();
            }

            return View(pizzaViewModel);
        }

        // GET: PizzaController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PizzaController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageFile,Name,Weight,Descripton,Price,Diameter")] PizzaViewModel pizzaViewModel)
        {
            if (ModelState.IsValid)
            {
                if (pizzaViewModel.ImageFile != null)
                {
                    string folder = "images/pizza/";
                    folder += Guid.NewGuid().ToString() + "_" + pizzaViewModel.ImageFile.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await pizzaViewModel.ImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                _context.Add(pizzaViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pizzaViewModel);
        }

        // GET: PizzaController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PizzaViewModel == null)
            {
                return NotFound();
            }

            var pizzaViewModel = await _context.PizzaViewModel.FindAsync(id);
            if (pizzaViewModel == null)
            {
                return NotFound();
            }
            return View(pizzaViewModel);
        }

        // POST: PizzaController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageFile,Name,Weight,Descripton,Price,Diameter")] PizzaViewModel pizzaViewModel)
        {
            if (id != pizzaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaViewModelExists(pizzaViewModel.Id))
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
            return View(pizzaViewModel);
        }

        // GET: PizzaController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PizzaViewModel == null)
            {
                return NotFound();
            }

            var pizzaViewModel = await _context.PizzaViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizzaViewModel == null)
            {
                return NotFound();
            }

            return View(pizzaViewModel);
        }

        // POST: PizzaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PizzaViewModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PizzaViewModel'  is null.");
            }
            var pizzaViewModel = await _context.PizzaViewModel.FindAsync(id);
            if (pizzaViewModel != null)
            {
                _context.PizzaViewModel.Remove(pizzaViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaViewModelExists(int id)
        {
            return (_context.PizzaViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}