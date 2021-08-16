using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Game> AllGames = _context.Games.ToList();
            ViewBag.AllGames = AllGames;
            return View();
        }

        [HttpGet("AddGame")]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Game newGame)
        {
            if(ModelState.IsValid)
            {
                //I want to add it to my database
                _context.Add(newGame);
                _context.SaveChanges();
                return RedirectToAction("Index");
            } else {
                return View("AddGame");
            }
        }
        [HttpGet("onegame/{GameId}")]        
            public IActionResult OneGame(int GameId)
            {
                ViewBag.OneGame = _context.Games.FirstOrDefault(a => a.GameId == GameId);
                return View();
            }

            [HttpGet("delete/{GameId}")]        
            public IActionResult Delete(int GameId)
            {
                Game gameToDelete = _context.Games.SingleOrDefault(a => a.GameId == GameId);
                _context.Games.Remove(gameToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            [HttpGet("edit/{GameId}")]
            public IActionResult UpdateGame(int GameId)
            {
                Game GameToEdit = _context.Games.FirstOrDefault(g => g.GameId == GameId);
                return View(GameToEdit);
            }

            [HttpPost("Update/{GameId}")]
            public IActionResult PerformUpdate(int GameId, Game EditGame)
            {   
                Game GameToEdit = _context.Games.FirstOrDefault(g => g.GameId == GameId);  
                GameToEdit.Title = EditGame.Title;
                GameToEdit.Price = EditGame.Price;
                GameToEdit.Rating = EditGame.Rating;
                GameToEdit.ReleaseDate = EditGame.ReleaseDate;
                GameToEdit.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
