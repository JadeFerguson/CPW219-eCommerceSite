
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
        
        public async Task<IActionResult> Index(int? id) // int id has to do with mapping
        {
            const int NumGamesToDisplayPerPage = 3;
            const int PageOffSet = 1; // Need a page offset to use current page and figure out, nums game to skip
            

            // Set currPage to id if it has a value, otherwise use 1
            int currPage = id ?? 1; // sort cut way of writing of id id.hasvalue then currPage == id.value

            int totalNumOfProducts = await _context.Games.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumOfProducts / NumGamesToDisplayPerPage);
            int lastPage = Convert.ToInt32(maxNumPages); // Rounding pages up to next whole number

            // query syntax
            List<Game> games = await (from game in _context.Games
                                      select game)
                                      .Skip(NumGamesToDisplayPerPage * (currPage - PageOffSet))
                                      .Take(NumGamesToDisplayPerPage)
                                      .ToListAsync();

            GameCatelogViewModel catalogModel = new(games, lastPage, currPage);


            // Show them on the page
            return View(catalogModel);

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

        public async Task<IActionResult> Delete(int id)
        {
            // find game by id
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete == null)
            {
                return NotFound();
            }
            return View(gameToDelete);
        }

        // so when post it's like posting to a method called post so c# doestn yell at us
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete != null)
            {
                _context.Games.Remove(gameToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = gameToDelete.Title + " was deleted successfully";
                return RedirectToAction("index");
            }
            TempData["Message"] = "This game was already deleted";
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Details(int id)
        {
            Game? gameDetails = await _context.Games.FindAsync(id);

            if (gameDetails == null)
            {
                return NotFound();
            }

            return View(gameDetails);

        }
    }
}
