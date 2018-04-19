using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishlistManager.Models;

namespace WishlistManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Wishlist")]
    public class WishlistController : Controller
    {
        private readonly WishlistContext _context;

        //Constructor
        public WishlistController(WishlistContext context)
        {
            _context = context;

            if (_context.Wishlists.Count() == 0) //Generate basic value incase context empty - For demonstration and testpurposes - a few things will be generated (easier to clear DB and refill with base values)
            {
                //Favorite wishlists
                //_context.Wishlists.Add(new WishlistItem { Title = "My Favorites", OwnerId = 1, Occasion = "General", IsOpen = true });
                //_context.Wishlists.Add(new WishlistItem { Title = "My Favorites", OwnerId = 2, Occasion = "General", IsOpen = true });
                //_context.Wishlists.Add(new WishlistItem { Title = "My Favorites", OwnerId = 3, Occasion = "General", IsOpen = true });
                //Created Wislists
                //_context.Wishlists.Add(new WishlistItem { Title = "Happy times", Occasion = "Anniversery", IsOpen = false, Deadline= new DateTime(2018, 12, 16) });
                //_context.Wishlists.Add(new WishlistItem { Title = "New Garden", Occasion = "Garden renovations finished", IsOpen = true, Deadline = new DateTime(2018, 9, 2) });
                //_context.Wishlists.Add(new WishlistItem { Title = "Birthday", Occasion = "Its my birhtday", IsOpen = false, Deadline = new DateTime(2018, 11, 30) });
                //_context.Wishlists.Add(new WishlistItem { Title = "Wedding", Occasion = "Me and hubby getting married", IsOpen = true, Deadline = new DateTime(2019, 6, 3) });
                _context.SaveChanges();
            }
        }


        //GET CALLS
        [HttpGet]
        public IEnumerable<WishlistItem> GetAll()
        {
            return _context.Wishlists.ToList();
        }

    }
}