
using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{  
    public class GamesController : Controller
    {
        // added this with constructor by right clicking it added parameters and set to it
        // readonly makes it so we cannot reassign to else where in class
        private readonly VideoGameContext _context;

        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        [HttpGet]
        // To add razor view right click on create then will show up
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game game)
        {
            if (ModelState.IsValid)
            {
                // Add game to DB
                _context.Games.Add(game); // Prepares insert
                _context.SaveChanges(); // Executes pending insert

                // Show success message on page
                ViewData["Message"] = $"{game.Title} was added successfully";
                //TempData - keeps data when redirected to a new page

                return View();
            }
            return View(game);
        }
    }
}
