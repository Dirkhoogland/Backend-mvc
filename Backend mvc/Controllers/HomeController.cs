using Backend_mvc.Models;
using Backend_mvc.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Backend_mvc.Controllers
{
    [BindProperties(SupportsGet = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HomeController(ILogger<HomeController> logger, SchoolContext context)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _logger = logger;
            _context = context;
        }
        public List<Lijstentable> Lijsten { get; set; }
        public string Lijst { get; private set; }




        [HttpPost]
        public IActionResult Deletetask([FromBody]int id)
        {
            Debug.WriteLine(id);
            List<Tasks> removefunct = _context.Tasks.Where(a => a.Id == id).ToList();
            _context.Tasks.Remove(removefunct[0]);

            _context.SaveChanges();

            if (_context.SaveChanges() > 0)
            {

            }
            return Index();
        }
        public IActionResult Index()
        {
            Lijsten = _context.Lijstentable.Where(m => m.IdLijst >= 0).ToList();
           
            List<Tasks> Taken = new List<Tasks>();
            foreach (var item in Lijsten)
            {

                Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.NaamLijst).ToList()) ;
            }
            ViewBag.item = Taken;

            return View(Taken);
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
        [HttpPost]
        public IActionResult Listadd([FromBody] string lijst)
        {
                 _context.Lijstentable.Add(new Lijstentable()
                {
                    NaamLijst = lijst
                });
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks ON;");
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks OFF;");
            if (_context.SaveChanges() > 0)
            {
            }
            return Index();
        }

        [HttpPost]
        public IActionResult Tasksaddto([FromBody] List<Tasks> tasklist)
        {
            
            //_context.Tasks.Add(new Tasks()
            //{
            //    Naam = tasklist,
            //    Lijst = tasklist,
            //    Beschrijving = tasklist,
            //    Status = tasklist,
            //    Duur = 
            //});

            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks ON;");
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks OFF;");
            if (_context.SaveChanges() > 0)
            {
                
            }
            return Index();
        }
    }
}