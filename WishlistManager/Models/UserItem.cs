using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class UserItem
    {
        public long Id { get; set; }
        public string Firstname { get; set; }                               //name of user
        public string Lastname { get; set; }
        public string Email { get; set; }                                   //email of user, can be used to add user to contacts/friendlist
        public List<UserItem> Contacts { get; set; } = new List<UserItem>();                        //list of others the user has in contacts - one to many
        public List<MessageItem> Messages { get; set; } = new List<MessageItem>();                     //list of messages user recieved - could be determined by get by recipant id in messages
        public List<WishlistItem> MyWishlists { get; set; } = new List<WishlistItem>();                //Can be done from wishlist context get by ownerid
        public List<WishlistItem> OtherWishlists { get; set; } = new List<WishlistItem>();           //id list of closed wishlist the user is participating in - should have this list in those wishlists for easy lookup, can be left out here

        public WishlistItem FavoriteWishlist { get; set; }                  //Single wishlist of favorites

    }
}
