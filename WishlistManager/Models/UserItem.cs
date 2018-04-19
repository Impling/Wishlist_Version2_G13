using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class UserItem
    {
        public long Id { get; set; }
        public string Firstname { get; set; }                                 //name of user
        public string Lastname { get; set; }
        public string Email { get; set; }                                    //email of user, can be used to add user to contacts/friendlist
        //public List<long> Contacts { get; set; }                    //id list of others the user has in contacts - one to many
        public List<UserItem> Contacts { get; set; }
        //public IEnumerable<long> MessageIds { get; set; }                   //id list of messages user recieved - could be determined by get by recipant id in messages
        //public IEnumerable<long> MyWishlistIds { get; set; }              //Can be done from wishlist context get by ownerid
        //public List<long> OtherWishlistsId { get; set; }             //id list of closed wishlist the user is participating in - should have this list in those wishlists for easy lookup, can be left out here

        public long FavoriteId { get; set; }                                 //Single wishlist of favorites

    }
}
