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
