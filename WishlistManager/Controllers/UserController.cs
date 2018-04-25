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
                UserItem u1 = new UserItem { Firstname = "Timo", Lastname = "Spanhove", Email = "Timo.spanhove@Hotmail.com", Id = 1};
                UserItem u2 = new UserItem { Firstname = "Victor", Lastname = "Van Weyenberg", Email = "Vic.VW@hotmail.com", Id = 2};
                UserItem u3 = new UserItem { Firstname = "Sander", Lastname = "De Sutter", Email = "Sander.desutter@hotmail.com", Id= 3};

                WishlistItem f1 = new WishlistItem { Title = "My Favorites", Occasion = "General", IsOpen = true, Owner = u1, Id = 1};
                WishlistItem f2 = new WishlistItem { Title = "My Favorites", Occasion = "General", IsOpen = true, Owner = u2, Id = 2 };
                WishlistItem f3 = new WishlistItem { Title = "My Favorites", Occasion = "General", IsOpen = true, Owner = u3, Id = 3 };

                WishlistItem w1 = new WishlistItem { Title = "Happy times", Occasion = "Anniversery", IsOpen = false, Deadline= new DateTime(2018, 12, 16), Owner = u1, Id = 4 };
                WishlistItem w2 = new WishlistItem { Title = "New Garden", Occasion = "Garden renovations finished", IsOpen = true, Deadline = new DateTime(2018, 9, 2), Owner = u1, Id = 5};
                WishlistItem w3 = new WishlistItem { Title = "Birthday", Occasion = "Its my birhtday", IsOpen = false, Deadline = new DateTime(2018, 11, 30), Owner = u2, Id = 6, Participants = { u1 } };
                WishlistItem w4 = new WishlistItem { Title = "Wedding", Occasion = "Me and hubby getting married", IsOpen = true, Deadline = new DateTime(2019, 6, 3), Owner =u3, Id = 7  };


                // init contacts
                u1.Contacts = new List<UserItem> { u2, u3 };
                u2.Contacts = new List<UserItem> { u1 };  //Why does this keep dissappearing
                u3.Contacts = new List<UserItem> { u1 };
                //init favorites
                u1.FavoriteWishlist = f1;
                u2.FavoriteWishlist = f2;
                u3.FavoriteWishlist = f3;
                //Init Wishlists
                u1.MyWishlists = new List<WishlistItem>{ w1, w2 };
                u2.MyWishlists = new List<WishlistItem> { w3 };
                u3.MyWishlists = new List<WishlistItem> { w4 };
                u1.OtherWishlists = new List<WishlistItem> { w3 };

                //Init users
                u2.Contacts = new List<UserItem> { u1 };
                _context.Users.Add(u1);
                _context.Users.Add(u2);
                _context.Users.Add(u3);

                _context.SaveChanges();
            }
        }


        //GET CALLS
        [HttpGet]
        public IEnumerable<UserItem> GetAll()
        {
            return _context.Users   //.Include(t => t.Contacts)
                                    //.Include(t => t.Messages)
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
