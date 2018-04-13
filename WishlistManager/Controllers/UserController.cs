using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WishlistManager.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WishlistManager.Controllers
{

    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserContext _context;


        //Constructor
        public UserController(UserContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0) //Generate basic value incase context empty - For demonstration and testpurposes - a few things will be generated (easier to clear DB and refill with base values)
            {
                _context.Users.Add(new UserItem { Firstname = "Timo", Lastname = "Spanhove", Email = "Timo.spanhove@Hotmail.com" });
                _context.Users.Add(new UserItem { Firstname = "Victor", Lastname = "Van Weyenberg", Email = "Vic.VW@hotmail.com" });
                _context.Users.Add(new UserItem { Firstname = "Sander", Lastname = "De Sutter", Email = "Sander.desutter@hotmail.com" });
                _context.SaveChanges();
            }
        }


        //GET CALLS
        [HttpGet]
        public IEnumerable<UserItem> GetAll()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var item = _context.Users.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

    }
}
