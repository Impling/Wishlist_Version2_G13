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

                //Wishlist w1 = new Wishlist("Happy times", "Anniversery", false, new DateTime(2018, 12, 16));
                //Wishlist w2 = new Wishlist("New Garden", "Garden renovations finished", true, new DateTime(2018, 9, 2));
                //Wishlist w3 = new Wishlist("Birthday", "Its my birhtday", false, new DateTime(2018, 11, 30));
                //Wishlist w4 = new Wishlist("Wedding", "Me and hubby getting married", true, new DateTime(2019, 6, 3));



                User[] users = new User[] { u1, u2, u3 };
                _context.Users.AddRange(users);

                //u1.AddContact(u2);
                //u1.AddContact(u3);
                
                //u1.AddOwnWishlist(w1);
                //u1.AddOwnWishlist(w2);
                //u2.AddOwnWishlist(w3);
                //u3.AddOwnWishlist(w4);

                //w3.addParticipant(u1);

                _context.SaveChanges();

                //Add wishlists to user

                u1.AddContact(u2);

                _context.SaveChanges();
                

            }

        }

        #endregion
    }
}
