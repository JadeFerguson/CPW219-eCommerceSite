
using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<IActionResult> Index()
        {
            // Get all games from the DB Method syntax
            //List<Game> games = _context.Games.ToList();

            // query syntax
            List<Game> games = await (from game in _context.Games
                                      select game).ToListAsync();

            // Show them on the page
            return View(games);

        }

        [HttpGet]
        // To add razor view right click on create then will show up
        public IActionResult Create()
        {
            
            return View();
        }


        [HttpPost]
        //await and async added keywords make more efficient for traffic
        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/performance/using-asynchronous-methods-in-aspnet-mvc-4
        public async Task<IActionResult> Create(Game game)
        {
            if (ModelState.IsValid)
            {
                // Add game to DB
                _context.Games.Add(game); // Prepares insert
                await _context.SaveChangesAsync(); // Executes pending insert

                // Show success message on page
                ViewData["Message"] = $"{game.Title} was added successfully";
                //TempData - keeps data when redirected to a new page

                return View();
            }
            return View(game);
        }

        //async needs task 
        public async Task<IActionResult> Edit(int id)
        {
            Game? gameToEdit = await _context.Games.FindAsync(id);

            if (gameToEdit == null)
            {
                return NotFound();
            }

            return View(gameToEdit);
        }

        // Model binding 
        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Update(gameModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{gameModel.Title} was updated successfully";
                return RedirectToAction("Index");
            }
            return View(gameModel);
        }

    }
}
