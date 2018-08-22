using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static WishlistManager.Data.WishlistDbContext;

namespace WishlistManager.Models
{
    public class User
    {

        #region Properties
        public int UserId { get; set; }
        public string Firstname { get; set; }                               //name of user
        public string Lastname { get; set; }
        public string Email { get; set; }                                   //email of user, can be used to add user to contacts/friendlist
        public string Password { get; set; }
        public virtual ICollection<UserContact> Contacts { get; set; }

        public virtual ICollection<MessageUser> Messages { get; set; } = new List<MessageUser>();                     //list of messages user recieved - could be determined by get by recipant id in messages
        public virtual ICollection<UserWishlist> OwnWishlists { get; set; }                //Can be done from wishlist context get by ownerid
        public virtual ICollection<WishlistParticipant> OtherWishlists { get; set; }           //id list of closed wishlist the user is participating in - should have this list in those wishlists for easy lookup, can be left out here


        //public int NrOfMyWishlists => MyWishlists.Count;
        //public int NrOfOtherWishlists => OtherWishlists.Count;

        #endregion

        #region Constructors
        protected User() {
            Contacts = new List<UserContact>();
            OwnWishlists = new List<UserWishlist>();
            OtherWishlists = new List<WishlistParticipant>();
            AddFavoriteWishlist(new Wishlist("My favorite gifts", "These are gifts I appreciate receiving on any occasion."));//Every user has a wishlist for item he likes to get on multiple occasions, like favorite flowers or wines.
            Messages = new List<MessageUser>();
            //List has functionallity like ordening, while Hashset is quicker in removing and adding
        }

        public User(string firstname, string lastname, string email, string password) : this() {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
        }


        #endregion

        #region Methods
        //Add personal Favorite wishlist
        public void AddFavoriteWishlist(Wishlist wishlist)
        {
            UserWishlist userWishlist = new UserWishlist(this.UserId, wishlist.WishlistId, this, wishlist, true);
            OwnWishlists.Add(userWishlist);
            wishlist.WishlistOwner = userWishlist;
        }

        //Add personal wishlist
        public void AddOwnWishlist(Wishlist wishlist) {
            UserWishlist userWishlist = new UserWishlist(this.UserId, wishlist.WishlistId, this, wishlist, false);
            OwnWishlists.Add(userWishlist);
            wishlist.WishlistOwner = userWishlist;
        }

        //Add wishlist to closed wishlist you participate in
        public void AddOtherWishlist(Wishlist wishlist)
        {
            //OtherWishlists.Add(wishlist);
        }

        //Add concact to contact list
        public void AddContact(User contact) {
            //Check if contact exist and wether you allready have this contact

            Contacts.Add(new UserContact(this.UserId, contact.UserId, this, contact));
            
            //Contacts.Add(contact); //TEST 1, See if Contact also adds user in his contact list.
            //If not
            //contact.AddContact(this);
        }
        //Add contacts to contact list
        public void AddContacts(List<User> contacts)
        {
            //UserContact uc;
            //check if user already exists TO DO + If email of contact acctally exists in db allready

            //contacts.ForEach(c => AddContact(c));
        }


        #endregion


        /*
         *HashSet<T> -> no duplicates allowed, no order of elements, add returns boolean, quicker (add, contians)
         * List<T> -> allows duplicates, elements are ordered, slower (allows for insert or indexof)
         * 
         */
    }

    //Extra class for the joined table between user and his contacts
    public class UserContact
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ContactId { get; set; }
        public virtual User Contact { get; set; }

        public UserContact(int userId, int contactId, User user, User contact) {

            UserId = userId;
            ContactId = contactId;

            User = user;
            Contact = contact;

        }

    }

    public class MessageUser
    {
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }

        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public MessageUser(int messageId, int receiverId, Message message, User receiver)
        {

            MessageId = messageId;
            ReceiverId = receiverId;

            Message = message;
            Receiver = receiver;
        }

    }
}
