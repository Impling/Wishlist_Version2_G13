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
    [Route("api/Wish")]
    public class WishController : Controller
    {

        private readonly WishContext _context;

        //Constructor
        public WishController(WishContext context)
        {
            _context = context;

            if (_context.Wishes.Count() == 0) //Generate basic value incase context empty - For demonstration and testpurposes - a few things will be generated (easier to clear DB and refill with base values)
            {
                _context.Wishes.Add(new WishItem { Name = "Red Whine", Description="Best time to get drunk in a fancy way.", Catergory = "Consumable", WishlistId=1});
                _context.Wishes.Add(new WishItem { Name = "Marshmellows", Description = "We'll have an open fire for roasting.", Catergory = "Consumable", WishlistId=2});
                _context.Wishes.Add(new WishItem { Name = "Bug zapping drone", Description = "Keep the bugs out of our garden.", Catergory = "Tech", WishlistId = 2 });
                _context.Wishes.Add(new WishItem { Name = "Citadel paint set", Description = "I'd like to have some more drybrush paint", Catergory = "Tools", WishlistId = 3 });
                _context.SaveChanges();

                /*
                        public string Weblink { get; set; }         //String of url to item website
                        public string Image { get; set; }           //String of url to item image
                        public long BuyerId { get; set; }           //Id of user that bought the item
                */
            }
        }
        
        //GET CALLS
        [HttpGet]
        public IEnumerable<WishItem> GetAll()
        {
            return _context.Wishes.ToList();
        }
        
    }
}