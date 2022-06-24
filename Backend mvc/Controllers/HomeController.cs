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

        public class tasklist
        {
            public string? naam { get; set; }
            public string? lijst { get; set; }
            public string? besch { get; set; }
            public string? status { get; set; }
            public int duur { get; set; }
        }
        public class tasklistupdate
        {   
            public string? naam { get; set; }
            public string? lijst { get; set; }
            public string? besch { get; set; }
            public string? status { get; set; }
            public int duur { get; set; }

            public int Id { get; set; }
        }
        public class listupdate
        {
            public int Id { get; set; }
            public string? naam { get; set; }
        }


        // functie om een task te deleten
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
        // functie om lijst te deleten
        [HttpPost]
        public IActionResult Deletelist([FromBody] int id)
        {

            List<Lijstentable> removefunct = _context.Lijstentable.Where(a => a.IdLijst == id).ToList();
            _context.Lijstentable.Remove(removefunct[0]);

            _context.SaveChanges();

            if (_context.SaveChanges() > 0)
            {

            }
            return Index();
        }
        // index pagina laden 
        public IActionResult Index()
        {
            Lijsten = _context.Lijstentable.Where(m => m.IdLijst >= 0).ToList();
            
            List<Tasks> Taken = new List<Tasks>();
            List<Lijstentable> Lijstenfront = new List<Lijstentable>();
            foreach (var item in Lijsten)
            {

                Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.NaamLijst).ToList());
                Lijstenfront.AddRange(_context.Lijstentable.Where(t => t.NaamLijst == item.NaamLijst).ToList());
            }
            ViewBag.item = Taken;
            ViewBag.items = Lijstenfront ;
            return View();
        }
        public IActionResult SortedBytime()
        {

                Lijsten = _context.Lijstentable.Where(m => m.IdLijst >= 0).ToList();

                List<Tasks> Taken = new List<Tasks>();
                List<Lijstentable> Lijstenfront = new List<Lijstentable>();
                foreach (var item in Lijsten)
                {

                    Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.NaamLijst).ToList());
                    Lijstenfront.AddRange(_context.Lijstentable.Where(t => t.NaamLijst == item.NaamLijst).ToList());
                }

            List <Tasks> Taken2 = Taken.OrderBy(t => t.Duur).ToList();

            ViewBag.item = Taken2;
            ViewBag.items = Lijstenfront;



                return View();
        }
        public IActionResult Status()
        {

            Lijsten = _context.Lijstentable.Where(m => m.IdLijst >= 0).ToList();

            List<Tasks> Taken = new List<Tasks>();
            List<Lijstentable> Lijstenfront = new List<Lijstentable>();
            foreach (var item in Lijsten)
            {

                Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.NaamLijst).ToList());
                Lijstenfront.AddRange(_context.Lijstentable.Where(t => t.NaamLijst == item.NaamLijst).ToList());
            }
            List<Tasks>Taken3 = Taken.OrderBy(t => t.Status).ToList();


            ViewBag.item = Taken3;
            ViewBag.items = Lijstenfront;



            return View();
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

        // lijst toe te voegen
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
        // task toe te voegen
        [HttpPost]
        public IActionResult Tasksaddto([FromBody]tasklist tasklist1)
        {

            _context.Tasks.Add(new Tasks()
            {
                Naam = tasklist1.naam,
                Lijst =tasklist1.lijst,
                Beschrijving = tasklist1.besch,
                Status = tasklist1.status,
                Duur = tasklist1.duur
            });

            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks ON;");
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks OFF;");
            if (_context.SaveChanges() > 0)
            {

            }
            tasklist1.naam = "";
            tasklist1.lijst = "";
            tasklist1.besch = "";
            tasklist1.status = "";
            tasklist1.duur = 0;
            return Index();
        }

        // task updaten
        public IActionResult Tasksupdateto([FromBody] tasklistupdate tasklist2)
        {
            _context.Tasks.Update(new Tasks()
            { Id = tasklist2.Id,

                Naam = tasklist2.naam,
                Lijst = tasklist2.lijst,
                Beschrijving = tasklist2.besch,
                Status = tasklist2.status,
                Duur = tasklist2.duur
            });

            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks ON;");
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Tasks OFF;");
            if (_context.SaveChanges() > 0)
            {

            }
            tasklist2.naam = "";
            tasklist2.lijst = "";
            tasklist2.besch = "";
            tasklist2.status = "";
            tasklist2.duur = 0;
            return Index();
        }

        // lijst updaten
        [HttpPost]
        public IActionResult ListUpdate([FromBody] listupdate lijst)
        {
            var list = new Lijstentable
            {
                IdLijst = lijst.Id,
                NaamLijst = lijst.naam,

            };

            var updatedlist = _context.Lijstentable.SingleOrDefault(b => b.IdLijst == lijst.Id);
            _context.Lijstentable.AttachRange(list) ;
            _context.SaveChanges();
            return Index();
        }
    }
}