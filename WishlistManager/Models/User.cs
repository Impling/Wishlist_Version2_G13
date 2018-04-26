using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class User
    {

        #region Properties
        public int UserId { get; set; }
        public string Firstname { get; set; }                               //name of user
        public string Lastname { get; set; }
        public string Email { get; set; }                                   //email of user, can be used to add user to contacts/friendlist
        //public List<UserItem> Contacts { get; set; } = new List<UserItem>();                        //list of others the user has in contacts - one to many
        //public List<MessageItem> Messages { get; set; } = new List<MessageItem>();                     //list of messages user recieved - could be determined by get by recipant id in messages
        public ICollection<Wishlist> MyWishlists { get; set; }                //Can be done from wishlist context get by ownerid
        public ICollection<Wishlist> OtherWishlists { get; set; }           //id list of closed wishlist the user is participating in - should have this list in those wishlists for easy lookup, can be left out here
        public Wishlist FavoriteWishlist { get; set; }                  //Single wishlist of favorites

        public int NrOfMyWishlists => MyWishlists.Count;
        public int NrOfOtherWishlists => OtherWishlists.Count;

        #endregion

        #region Constructors
        protected User() {
            MyWishlists = new HashSet<Wishlist>();
            OtherWishlists = new HashSet<Wishlist>();
            FavoriteWishlist = new Wishlist("My favorite gifts", "These are gifts I appreciate receiving on any occasion.");    //Every user has a wishlist for item he likes to get on multiple occasions, like favorite flowers or wines.
        }

        public User(string firstname, string lastname, string email) : this() {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }


        #endregion

        #region Methods


        #endregion


        /*
         *HashSet<T> -> no duplicates allowed, no order of elements, add returns boolean, quicker (add, contians)
         * List<T> -> allows duplicates, elements are ordered, slower (allows for insert or indexof)
         * 
         */
    }
}
