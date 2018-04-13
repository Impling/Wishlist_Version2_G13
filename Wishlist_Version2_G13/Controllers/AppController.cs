using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public AppController() { }

        public AppController(User loggedInUser)
        {
            User = loggedInUser;
        }


        //Functions
        //Function 1)AddContact - add contact to contactlist of logged in user via email address
        public bool addContact(string email)
        {
            //check email ignore case
            //1) check if email same as email of loggedInUser, if so dont add and return false          [Errormessage: You cannot add yourself to your contactlist]
            //2) check if new contact allready exists in contactlist, if so returen false               [Errormessage: You allready have contact {contactname} in your list]
            //3) check if contact is a registered user (check if contact exists), if so return false    [Errormessage: The contact {email} you wish to add was not found in the user database]

            //Add funtion to retrieve user from DB using email address here
            //User newContact = new User();   //TEMP UNTIL WE CAN ACTUALY GET USER -  User newContact = GetContact(email); 

            //add contact to contactlist of user
            //User.addContact(newContact);

            //returen false on failure, true on success
            return true;
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

    }

}
