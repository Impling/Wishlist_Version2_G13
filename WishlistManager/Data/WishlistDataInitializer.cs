using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishlistManager.Models;

namespace WishlistManager.Data
{

    public class WishlistDataInitializer
    {

        #region Properties
        private readonly WishlistDbContext _context;
        #endregion

        #region Constructors
        public WishlistDataInitializer(WishlistDbContext context) {
            _context = context;
        }

        public void InitializeData() {

            if (!_context.Users.Any()) {

                //Create default users
                User u1 = new User("Timo", "Spanhove","Timo.spanhove@Hotmail.com","Password1");
                User u2 = new User("Victor", "Van Weyenberg", "Vic.VW@hotmail.com", "Password2");
                User u3 = new User("Sander", "De Sutter", "Sander.desutter@hotmail.com", "Password3");
                User u4 = new User("testy", "McTestface", "Tester", "12345678");
                User u5 = new User("testuser", "testington", "testuser", "12345678");

                Wishlist w1 = new Wishlist("Happy times", "Anniversery", false, new DateTime(2018, 12, 16));
                Wishlist w2 = new Wishlist("New Garden", "Garden renovations finished", true, new DateTime(2018, 9, 2));
                Wishlist w3 = new Wishlist("Birthday", "Its my birhtday", false, new DateTime(2018, 11, 30));
                Wishlist w4 = new Wishlist("Wedding", "Me and hubby getting married", true, new DateTime(2019, 6, 3));

                Item i1 = new Item("Spoon", "Kitchen");


                //Add Users
                User[] users = new User[] { u1, u2, u3, u4, u5 };
                _context.Users.AddRange(users);
                _context.SaveChanges();

                //Add Contact to users
                u1.AddContact(u2);  //Needs to happen to both sides
                u2.AddContact(u1);
                u1.AddContact(u3);  //Needs to happen to both sides
                u3.AddContact(u1);
                _context.SaveChanges();

                //Add few wishlists
                Wishlist[] wishlists = new Wishlist[] { w1, w2, w3, w4 };
                _context.Wishlists.AddRange(wishlists);
                _context.SaveChanges();
          
                //Store added wishlist in relation table
                u1.AddOwnWishlist(w1);
                u1.AddOwnWishlist(w2);
                u2.AddOwnWishlist(w3);
                u3.AddOwnWishlist(w4);
                _context.SaveChanges();

                //Add Participants to closed wishlist
                //w3.addParticipant(u1);

                //Add Items to wishlist
                w1.AddGift(i1);
                _context.SaveChanges();

                //Messages done in main app.

            }

        }

        #endregion
    }
}
