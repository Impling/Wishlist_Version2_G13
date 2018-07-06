using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Models;

namespace Wishlist_Version2_G13.Data
{
    public class WishlistDbInitializer
    {
        #region Properties
        private readonly WishlistDbContext _context;
        #endregion

        #region Constructors
        public WishlistDbInitializer(WishlistDbContext context)
        {
            _context = context;
        }

        public void InitializeData()
        {

            if (!_context.Users.Any())
            {

                //Create default users
                User u1 = new User("Timo", "Spanhove", "Timo.spanhove@Hotmail.com", "Password1");
                User u2 = new User("Victor", "Van Weyenberg", "Vic.VW@hotmail.com", "Password2");
                User u3 = new User("Sander", "De Sutter", "Sander.desutter@hotmail.com", "Password3");



                User[] users = new User[] { u1, u2, u3 };
                _context.Users.AddRange(users);

                _context.SaveChanges();

                //Add wishlists to user


            }

        }

        #endregion
    }
}
