﻿using Backend_mvc.Models;
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
            public int lijst { get; set; }
            public string? besch { get; set; }
            public string? status { get; set; }
            public int duur { get; set; }
        }
        public class tasklistupdate
        {
            public int Id { get; set; }
            public string? Naam { get; set; }

            public string? Status { get; set; }
            public int Duur { get; set; }

            public string? Besch { get; set; }
            public int Lijst { get; set; }
            
            
            

            
        }
        public class listupdate
        {
            public int Id { get; set; }
            public string Lijst { get; set; }
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

                Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.IdLijst).ToList());
                Lijstenfront.AddRange(_context.Lijstentable.Where(t => t.NaamLijst == item.NaamLijst).ToList());
            }
            ViewBag.item = Taken;
            ViewBag.items = Lijstenfront ;
            return View();
        }
        // ordered op tijd
        public IActionResult SortedBytime()
        {

                Lijsten = _context.Lijstentable.Where(m => m.IdLijst >= 0).ToList();

                List<Tasks> Taken = new List<Tasks>();
                List<Lijstentable> Lijstenfront = new List<Lijstentable>();
                foreach (var item in Lijsten)
                {

                    Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.IdLijst).ToList());
                    Lijstenfront.AddRange(_context.Lijstentable.Where(t => t.NaamLijst == item.NaamLijst).ToList());
                }

            List <Tasks> Taken2 = Taken.OrderBy(t => t.Duur).ToList();

            ViewBag.item = Taken2;
            ViewBag.items = Lijstenfront;



                return View();
        }
        // ordered op status klaar/bezig
        public IActionResult Status()
        {

            Lijsten = _context.Lijstentable.Where(m => m.IdLijst >= 0).ToList();

            List<Tasks> Taken = new List<Tasks>();
            List<Lijstentable> Lijstenfront = new List<Lijstentable>();
            foreach (var item in Lijsten)
            {

                Taken.AddRange(_context.Tasks.Where(t => t.Lijst == item.IdLijst).ToList());
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
            tasklist1.lijst = 0;
            tasklist1.besch = "";
            tasklist1.status = "";
            tasklist1.duur = 0;
            return Index();
        }


        [HttpPost]
        public IActionResult Updatetask([FromBody] tasklistupdate tasklist2)
        {
            var task = new Tasks
            {
                Beschrijving = tasklist2.Besch,
                Naam = tasklist2.Naam,
                Lijst = tasklist2.Lijst,
                Status = tasklist2.Status,
                Duur = tasklist2.Duur
            };

            var updatedlist = _context.Tasks.SingleOrDefault(b => b.Id ==task.Id);
            updatedlist.Naam = task.Naam;
            updatedlist.Status = task.Status;
            updatedlist.Duur = task.Duur;
            updatedlist.Lijst = task.Lijst;


            _context.SaveChanges();
            if (_context.SaveChanges() > 0)
            {

            }
            return Index();
        }

        // lijst updaten
        [HttpPost]
        public IActionResult ListUpdate([FromBody] listupdate lijsttesten)
        {
            var list = new Lijstentable
            {
                IdLijst = lijsttesten.Id,
                NaamLijst = lijsttesten.Lijst

            };

            var updatedlist = _context.Lijstentable.SingleOrDefault(b => b.IdLijst == lijsttesten.Id);
            updatedlist.NaamLijst = list.NaamLijst;
            _context.SaveChanges();
            return Index();
        }
    }
}