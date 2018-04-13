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
        //public ObservableCollection<User> Contacts { get; set; }             
        //public ObservableCollection<Message> Notifications { get; set; }
        //public ObservableCollection<Wishlist> MyWishlists { get; set; }    
        //public ObservableCollection<Wishlist> OthersWishlists { get; set; }
        public long FavoriteId { get; set; }                                 //Single wishlist of favorites

    }
}
