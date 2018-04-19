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
        public long OwnerId { get; set; }           //Id of wishlist owner
        public DateTime Deadline { get; set; }      //Deadline wishlist event, ?Does this need to be a string for web service

        //List of id's of users participating in wishlist
        //List of id's of items in wishlist

        public string Occasion { get; set; }        //Descriptor of event
        public bool IsOpen { get; set; }            //Bool for accesslevel of wishlist
    }

}
