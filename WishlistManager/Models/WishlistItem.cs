using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class WishlistItem
    {
        public long Id { get; set; }
        public string Title { get; set; }           //Title,name of wishlist
        public UserItem Owner { get; set; }          //Wishlist owner
        public DateTime Deadline { get; set; }      //Deadline wishlist event, ?Does this need to be a string for web service
        public List<UserItem> Participants { get; set; } = new List<UserItem>();
        public List<WishItem> Gifts { get; set; } = new List<WishItem>();
        public string Occasion { get; set; }        //Descriptor of event
        public bool IsOpen { get; set; }            //Bool for accesslevel of wishlist
    }

}
