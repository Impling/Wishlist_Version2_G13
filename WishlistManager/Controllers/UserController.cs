using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WishlistManager.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WishlistManager.Controllers
{
    [Produces("application/json")]
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
                //long[] contacts = { 2, 3 };
                //List<long> contacts = new List<long>(){ 2, 3 };
                List<UserItem> contacts = new List<UserItem>();
                contacts.Add(new UserItem { Firstname = "Victor", Lastname = "Van Weyenberg", Email = "Vic.VW@hotmail.com", FavoriteId = 2 });

                _context.Users.Add(new UserItem { Firstname = "Timo", Lastname = "Spanhove", Email = "Timo.spanhove@Hotmail.com", FavoriteId=1 , Contacts = contacts });
                /*
                _context.Users.Add(new UserItem { Firstname = "Victor", Lastname = "Van Weyenberg", Email = "Vic.VW@hotmail.com", FavoriteId = 2 });
                //contacts = new long[] { 1 };
                _context.Users.Add(new UserItem { Firstname = "Sander", Lastname = "De Sutter", Email = "Sander.desutter@hotmail.com", FavoriteId = 3 , Contacts = contacts });
            */
                _context.SaveChanges();
            }
        }


        //GET CALLS
        [HttpGet]
        public IEnumerable<UserItem> GetAll()
        {
            return _context.Users.Include(t => t.Contacts).ToList();
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
