using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        //to acces our db we need to import context to our controller, so we can use all sorts of methods from context
        private readonly MyAppContext _context;
        public ItemsController(MyAppContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.Items.ToListAsync(); // accesing item from database using_context, from the instance created in our context
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item); // add item to the context
                await _context.SaveChangesAsync(); // save changes to the database
                return RedirectToAction("Index"); // redirect to index after saving
            }
            return View(item); // if model state is not valid, return the view with the item
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id); // find the item by id from the database
            return View(item); // return the view with the item to edit
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x=>x.Id == id);
            return View(item); // return the view with the item to delete

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }

        //public IActionResult Overview()
        //{
        //    var item = new Item() {Name= "Keyboard"};
        //    return View(item);
        //}

        //public IActionResult Edit(int ItemId)
        //{
        //    return Content("id: " + ItemId );
        //}
    }
}
