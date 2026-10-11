using ASP_HW_5.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_HW_5.Controllers
{
    public class GameController : Controller
    {
        public static List<Game> Games = new List<Game>();

        [HttpGet]
        public IActionResult Index(string genre)
        {
            if (genre == null)
            {
                return View(Games);
            }

            var result = Games.Where(x => x.Genre == genre).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = Games.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return Content("Game not found");
            }

            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Game game)
        {
            if (game.Rating < 0 || game.Rating > 10)
            {
                return Content("Rating must be between 0 and 10");
            }

            game.Id = Games.Count == 0 ? 1 : Games.Max(x => x.Id) + 1;

            Games.Add(game);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var game = Games.FirstOrDefault(x => x.Id == id);

            if (game != null)
            {
                Games.Remove(game);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Completed()
        {
            var result = Games.Where(x => x.IsCompleted == true).ToList();

            return View(result);
        }
    }
}
