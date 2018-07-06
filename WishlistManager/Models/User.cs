using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WishlistManager.Data.WishlistDbContext;

namespace WishlistManager.Models
{
    public class User
    {

        #region Properties
        public int UserId { get; set; }
        //public int IdContact { get; set; }
        public string Firstname { get; set; }                               //name of user
        public string Lastname { get; set; }
        public string Email { get; set; }                                   //email of user, can be used to add user to contacts/friendlist
        public string Password { get; set; }
        //public ICollection<UserContact> Contacts { get; set; }                        //list of others the user has in contacts - one to many
        //public List<MessageItem> Messages { get; set; } = new List<MessageItem>();                     //list of messages user recieved - could be determined by get by recipant id in messages
        //public ICollection<Wishlist> MyWishlists { get; set; }                //Can be done from wishlist context get by ownerid
        //public ICollection<Wishlist> OtherWishlists { get; set; }           //id list of closed wishlist the user is participating in - should have this list in those wishlists for easy lookup, can be left out here
        //public Wishlist FavoriteWishlist { get; set; }                  //Single wishlist of favorites

        //public int NrOfMyWishlists => MyWishlists.Count;
        //public int NrOfOtherWishlists => OtherWishlists.Count;

        #endregion

        #region Constructors
        protected User() {
            //Contacts = new HashSet<UserContact>();
            //MyWishlists = new HashSet<Wishlist>();
            //OtherWishlists = new HashSet<Wishlist>();
            //FavoriteWishlist = new Wishlist("My favorite gifts", "These are gifts I appreciate receiving on any occasion.");    //Every user has a wishlist for item he likes to get on multiple occasions, like favorite flowers or wines.
        }

        public User(string firstname, string lastname, string email, string password) : this() {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
        }


        #endregion

        #region Methods
        //Add personal wishlist
        public void AddOwnWishlist(Wishlist wishlist) {
            //MyWishlists.Add(wishlist);
        }

        //Add wishlist to closed wishlist you participate in
        public void AddOtherWishlist(Wishlist wishlist)
        {
            //OtherWishlists.Add(wishlist);
        }

        //Add concact to contact list
        public void AddContact(User contact) {
            //check if user already exists
            //UserContact uc = new UserContact {UserId = this.UserId, User = this, ContactId = contact.UserId, Contact = contact };
            UserContact uc1 = new UserContact { Contact = contact.UserId, User = UserId };
            //Contacts.Add(uc1);
            UserContact uc2 = new UserContact { Contact = UserId, User = contact.UserId };
            //contact.Contacts.Add(uc2);

        }
        //Add contacts to contact list
        public void AddContacts(List<User> contacts)
        {
            //UserContact uc;
            //check if user already exists
            //contacts.ForEach(c => Contacts.Add(c));
        }


        #endregion


        /*
         *HashSet<T> -> no duplicates allowed, no order of elements, add returns boolean, quicker (add, contians)
         * List<T> -> allows duplicates, elements are ordered, slower (allows for insert or indexof)
         * 
         */
    }
}
