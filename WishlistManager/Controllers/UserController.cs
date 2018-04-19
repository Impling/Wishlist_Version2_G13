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

                //List<UserItem> contacts = new List<UserItem>();
                //contacts.Add(new UserItem { Firstname = "Victor", Lastname = "Van Weyenberg", Email = "Vic.VW@hotmail.com", FavoriteId = 2 });
                //Declare users
                UserItem u1 = new UserItem { Firstname = "Timo", Lastname = "Spanhove", Email = "Timo.spanhove@Hotmail.com"/*, FavoriteWishlist=f1 */};
                UserItem u2 = new UserItem { Firstname = "Victor", Lastname = "Van Weyenberg", Email = "Vic.VW@hotmail.com"/*, FavoriteWishlist = f1 */};
                UserItem u3 = new UserItem { Firstname = "Sander", Lastname = "De Sutter", Email = "Sander.desutter@hotmail.com"/*, FavoriteWishlist = f1 */};

                WishlistItem f1 = new WishlistItem { Title = "My Favorites", Occasion = "General", IsOpen = true };
                WishlistItem f2 = new WishlistItem { Title = "My Favorites", Occasion = "General", IsOpen = true };
                WishlistItem f3 = new WishlistItem { Title = "My Favorites", Occasion = "General", IsOpen = true };

                //Init users
                _context.Users.Add(u1);
                _context.Users.Add(u2);
                _context.Users.Add(u3);
                // init contacts
                u1.Contacts.Add(u2);
                u1.Contacts.Add(u3);
                u2.Contacts.Add(u1);
                u3.Contacts.Add(u1);
                //init favorites
                u1.FavoriteWishlist = f1;
                u2.FavoriteWishlist = f2;
                u3.FavoriteWishlist = f3;


                //_context.Users.FirstOrDefault(t => t.Id == 1).Contacts.Append(u2);
                //_context.Users.FirstOrDefault(t => t.Id == 1).Contacts.Add(u3);
                //_context.Users.FirstOrDefault(t => t.Id == 2).Contacts.Add(u1);
                //_context.Users.FirstOrDefault(t => t.Id == 3).Contacts.Add(u1);

                _context.SaveChanges();
            }
        }


        //GET CALLS
        [HttpGet]
        public IEnumerable<UserItem> GetAll()
        {
            return _context.Users   .Include(t => t.Contacts)
                                    .Include(t => t.Messages)
                                    //.Include(t => t.MyWishlists)
                                    //.Include(t => t.OtherWishlists)
                                    //.Include(t => t.FavoriteWishlist)
                                    .ToList();
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
