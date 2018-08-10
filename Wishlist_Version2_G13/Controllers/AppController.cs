using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Data;
using Wishlist_Version2_G13.Models;

namespace Wishlist_Version2_G13.Controllers
{
    public class AppController
    {

        //Variable declaration and getters and setters
        public User User { get; set; }      //currently logged in user on the app
        public Wishlist SelectedWishlist { get; set; }
        public Item SelectedItem { get; set; }

        //Constructors
        #region Constructors
        public AppController() { }

        public AppController(User loggedInUser)
        {
            User = loggedInUser;
        }
        #endregion

        //Functions
        #region Methods
        public User LoginUser(string email, string password) {

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    return context.Users.First(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }

            return null;

        }

        public void SetupLoggedInUser(User u) {

            User = u;

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    //Set Contact list
                    User.Contacts = new ObservableCollection<User>(GetContactsByUserId(User.UserId) as List<User>);

                    //Set Wishlists
                    //Set Wishlist of favorites
                    User.Favorites = GetFavoritesByUserId(User.UserId);
                    //Set Own Wishlists
                    foreach (Wishlist w in GetOwnedWishlistsByUserId(User.UserId)) {
                        User.MyWishlists.Add(w);
                    }
                    //Set all open wishlist of users contacts which user can participate in 
                    foreach (Wishlist w in GetOpenParticipatingWishlistsByUserId(User.UserId)) {
                        User.OthersWishlists.Add(w);
                    }
                    //Set closed wishlists user is participating with                  
                    foreach (Wishlist w in GetClosedParticipatingWishlistsByUserId(User.UserId)){
                        User.OthersWishlists.Add(w);
                    }

                    //Set Notifications


                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }

        }



        //Function )AddContact - add contact to contactlist of logged in user via email address
        public bool addContact(string email)//DIRECT ADDING, FIRST SEND REQUEST THEN ON CONFIRM ADD
        {

            //TO DO first Send REQUEST, only add on confirm

            //check email ignore case - Valid email check in view
          
            if (email == User.Email){ //MAKE NONCASE SENSITIVE
                //1) check if email same as email of loggedInUser, if so dont add and return false          [Errormessage: You cannot add yourself to your contactlist]
                //Messsage
                return false;
            }
            
            try
            {
                using (WishlistDbContext _context = new WishlistDbContext())
                {
                    User contact = _context.Users.FirstOrDefault(c => c.Email == email);

                    if (contact == null) {
                        //3) check if contact is a registered user (check if contact exists), if so return false    [Errormessage: The contact {email} you wish to add was not found in the user database]
                        //Message
                        return false;
                    } else if (_context.Contacts.FirstOrDefault(uc => uc.UserId == User.UserId && uc.ContactId == contact.UserId) != null) {
                        //2) check if new contact allready exists in contactlist, if so return false               [Errormessage: You allready have contact {contactname} in your list]
                        //MESSAGE
                        return false;
                    }
                    else {
                        _context.Contacts.Add(new UserContact(User.UserId, contact.UserId, User, contact)); //duplicate mirrored call for many to many relationship (EF does not support join table well enough to autmate this.)
                        _context.Contacts.Add(new UserContact(contact.UserId, User.UserId, contact, User));
                        return true;
                    }
                                        
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return false;
            }

            //returen false on failure, true on success


        }


        //Function 2)AddWishlist - add wishlist to currently logged in user widouth users or items
        public void addWishlist(Wishlist w)
        {
            this.User.addWishlist(w);
        }

        public void addWishlist(string title, string occasion, DateTime deadline)
        {
            Wishlist w = new Wishlist(this.User, title, occasion, deadline);
        }
        //Function 3)AddWishlist - add wishlist to currently logged in user with users including contacts you wish to add
        public void addWishlist(string title, string occasion, DateTime deadline, List<User> buyers)
        {
            Wishlist w = new Wishlist(this.User, title, occasion, deadline);
            foreach (User b in buyers)
            {
                w.addBuyer(b);
            }
        }
        //Function 4)AddWishlist - add wishlist to currently logged in user widouth users but with items
        public void addWishlist(string title, string occasion, DateTime deadline, List<Item> items)
        {
            Wishlist w = new Wishlist(this.User, title, occasion, deadline);
            foreach (Item i in items)
            {
                w.addItem(i);
            }

        }
        //Function 5)AddWishlist - add wishlist to currently logged in user with users but with items
        public void addWishlist(string title, string occasion, DateTime deadline, List<User> buyers, List<Item> items)
        {
            Wishlist w = new Wishlist(this.User, title, occasion, deadline);
            foreach (User b in buyers)
            {
                w.addBuyer(b);
            }
            foreach (Item i in items)
            {
                w.addItem(i);
            }
        }


        //Function 6)GetOwnWishlist

        public void AddItem(Item i)
        {
            SelectedWishlist.addItem(i);
        }

        #endregion

        //DB METHODS
        #region DBmethods GET
        public User GetUserById(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Users.FirstOrDefault(u => u.UserId == userId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public User GetUserByEmail(string email)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Users.FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public Wishlist GetFavoritesByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Wishlists.FirstOrDefault(w => w.WishlistId == context.OwnedWishlists.FirstOrDefault(ow => ow.IsFavorite && ow.OwnerId == userId).WishlistId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public List<Wishlist> GetOwnedWishlistsByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<int> wishlistIds = context.OwnedWishlists
                                                .Where(ow => !ow.IsFavorite && ow.OwnerId == User.UserId)
                                                .Select(ow => ow.WishlistId).ToList();

                    List<Wishlist> wishlists = new List<Wishlist>();
                    foreach (int id in wishlistIds)
                    {
                        context.Wishlists.FirstOrDefault(w => w.WishlistId == id).SetDeadlineText();
                        wishlists.Add(context.Wishlists.FirstOrDefault(w => w.WishlistId == id));
                    }
                    return wishlists;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public List<Wishlist> GetClosedParticipatingWishlistsByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<Wishlist> ws = new List<Wishlist>();

                    foreach (int id in context.Participants.Where(p => p.ParticipantId == User.UserId).Select(p => p.WishlistId).ToList())
                    {
                        Wishlist wl = context.Wishlists.FirstOrDefault(w => w.WishlistId == id);
                        wl.SetDeadlineText();
                        wl.Owner = context.Users.FirstOrDefault(o => o.UserId == context.OwnedWishlists.FirstOrDefault(ow => ow.WishlistId == id).OwnerId);
                        ws.Add(wl);
                    }
                    return ws;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<Wishlist> GetOpenParticipatingWishlistsByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<Wishlist> ws = new List<Wishlist>();

                    foreach (User c in User.Contacts)
                    {
                        foreach (int wi in context.OwnedWishlists.Where(ow => ow.OwnerId == c.UserId && !ow.IsFavorite).Select(wo => wo.WishlistId).ToList())
                        {
                            if (context.Wishlists.FirstOrDefault(w => w.WishlistId == wi).IsOpen)
                            {
                                Wishlist wl = context.Wishlists.FirstOrDefault(w => w.WishlistId == wi);
                                wl.SetDeadlineText();
                                wl.Owner = context.Users.FirstOrDefault(o => o.UserId == context.OwnedWishlists.FirstOrDefault(ow => ow.WishlistId == wi).OwnerId);
                                ws.Add(wl);
                            }
                        }
                    }
                    return ws;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<User> GetContactsByUserId(int userId)
        {

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Contacts
                                    .Where(c => c.UserId == User.UserId)
                                    .Select(c => c.Contact)
                                    .ToList();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }


        #endregion


    }

}
