using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wishlist_Version2_G13.Models
{
    public class User : INotifyPropertyChanged
    {
        //Variable declaration with getters and setters
        [Key]
        public int UserId { get; set; }                    //id of user    //unique generated on creation
        public string Firstname { get; set; }              //name of user
        public string Lastname { get; set; }
        public string Email { get; set; }                  //email of user, can be used to add user to contacts/friendlist
        public string Password { get; set; }
        public virtual ICollection<UserContact> UserContacts { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();                     //list of messages user recieved - could be determined by get by recipant id in messages
        public virtual ICollection<UserWishlist> OwnWishlists { get; set; }                //Can be done from wishlist context get by ownerid
        public virtual ICollection<WishlistParticipant> OtherWishlists { get; set; }           //id list of closed wishlist the user is participating in - should have this list in those wishlists for easy lookup, can be left out here


        [NotMapped]
        public ObservableCollection<User> Contacts { get; set; }           //list of people the user can add to his wishlist (get from phone contact list or facebook account)
        [NotMapped]
        public ObservableCollection<Message> Notifications { get; set; }
        [NotMapped]
        public ObservableCollection<Wishlist> MyWishlists { get; set; }    //Wishlists of the user - functionality(wishlist stays for owner even after deadline, and all the buyers become visible to him)
        [NotMapped]
        public ObservableCollection<Wishlist> OthersWishlists { get; set; }//Wishlists currently participating in
        [NotMapped]
        public Wishlist Favorites { get; set; }            //Single wishlist containing gift that fit in any occasion, like favorite flowers, choclate, candy, wine, giftcards of specific stores, favorite authors for books...

        //STill needs image added once db in order

        //Constructors
        public User() {
            Contacts = new ObservableCollection<User>();
            Notifications = new ObservableCollection<Message>();
            MyWishlists = new ObservableCollection<Wishlist>();
            OthersWishlists = new ObservableCollection<Wishlist>();
        }

        public User(string firstname, string lastname, string email, string password) : this()
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
            Favorites = new Wishlist("Mijn favoriete cadeau's", "General");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //Functions
        //Function 1)GetFullName
        public string getFullName()
        {
            string fullname = String.Format("{0} {1}", Firstname, Lastname);
            return fullname;
        }

        //Function 2)AddContact - add single user to contact list
        public void addContact(User contact)
        {
            //AppController gets given email string, looks in database for user retrieves it and calls this function
            //Contacts.Add(contact);
            foreach (Wishlist w in contact.MyWishlists)
            {
                OthersWishlists.Add(w);
            }

            //!!!!Make sure that when we add contact it is added to the DbContext, + Added to observable list, (should reload correctly on page reload) 
        }

        public void addNotification(Message m)
        {
            Notifications.Add(m);
        }

        //Function 3)AddWishlist
        public void addWishlist(Wishlist wishlist)
        {
            MyWishlists.Add(wishlist);
        }
        public void removeWishlist(Wishlist wishlist)
        {
            MyWishlists.Remove(wishlist);
        }

        //Function 4)CheckIfOwner - check if user is owner of wishlist
        public bool isOwner(Wishlist w)
        {
            if (MyWishlists.Contains(w))
                return true;
            return false;
        }

        public ObservableCollection<Wishlist> getMyWishlists()
        {
            return MyWishlists;
        }

        public void FillFavorites(ObservableCollection<Item> gifts)
        {
            Favorites.Items = gifts;
        }

    }

    //Extra class for the joined table between user and his contacts
    public class UserContact
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ContactId { get; set; }
        public virtual User Contact { get; set; }

        public UserContact(){}

        public UserContact(int userId, int contactId, User user, User contact)
        {

            UserId = userId;
            ContactId = contactId;

            User = user;
            Contact = contact;

        }


    }
}
