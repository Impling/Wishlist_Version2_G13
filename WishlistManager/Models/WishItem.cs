using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class WishItem
    {
        public long Id { get; set; }                //Id of wishlist item
        public string Name { get; set; }            //Name of item
        public string Description { get; set; }     //Description of item
        public string Weblink { get; set; }         //String of url to item website
        public string Image { get; set; }           //String of url to item image
        public string Catergory { get; set; }       //String of category enum
        public long BuyerId { get; set; }           //Id of user that bought the item
        public long WishlistId { get; set; }        //Id of Wishlist item belongs to, item should not be shared between wishlists as the users create their own, so even for the same gift the information should vary.

    }

}
