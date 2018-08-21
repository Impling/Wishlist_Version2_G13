using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Wishlist_Version2_G13.Data;
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.Repository;

namespace Wishlist_Version2_G13.Controllers
{
    public sealed class RuntimeInfo
    {
        private static readonly RuntimeInfo _instance = new RuntimeInfo();
        public int LoggedInUserId { get; set; }
        public User LoggedInUser { get; set; }
        public AppController AppController { get; set; }
        public TestRepository TestRepos { get; set; }
        public WishlistDbContext Context { get; set; }
        public double ScreenHeight { get; set; }
        public double ScreenWidth { get; set; }


        public static RuntimeInfo Instance
        {
            get { return _instance; }
        }

        private RuntimeInfo()
        {
            //TestRepos = new TestRepository();
            AppController = new AppController();

            //get screen dimensions
            //RefreshSize();
        }

        public void SetUserInApp(User u)
        {
            LoggedInUser = u;
            LoggedInUserId = u.UserId;
            AppController.SetupLoggedInUser(u);
            
        }

        public int RegisterUser(String firstname, String lastname, string email, string password)
        {

            //If user already exists 
            if (AppController.GetUserByEmail(email) != null) {
                return 1;
            }
            User registeredUser = new User(firstname, lastname, email, password);
            AppController.CreateUser(registeredUser);
            //Check on error in db transaction
            if (registeredUser == null)
            {
                return 2;
            }
            else {
                SetUserInApp(registeredUser);
                return 3;
            }


        }

        public void SetupSelectedWishlist(Wishlist w) {
            AppController.SetupSelectedWishlist(w);
        }

        public int LoginUser(string email, string password) {  //Throw error on failure

            //Check if user exists in database
            User user = AppController.LoginUser(email, password);

            if (user == null)
            {
                //Message: User not found in database please try again
                return 1;
            }
            else if (!user.Password.Equals(password))
            {
                //Message: The given password was incorrect.
                return 2;
            }
            else
            {
                //Message: Login successfull
                SetUserInApp(user);
                return 3;
            }

        }

        public void ClearLoggedInUser() {
            LoggedInUser = null;

        }

        public void RefreshSize() {
            //Set screen dimensions
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            SetBounds(bounds.Height * scaleFactor, bounds.Width * scaleFactor);
        }

        public void SetBounds(double height, double width)
        {
            ScreenHeight = height;
            ScreenWidth = width;
        }

        //TESTCODE for sql server manipulation widouth using DbContext, DELETE LATER(fallback if context does not work later on)
        public SqlConnection GetSqlServerConnection() {
            return new SqlConnection((App.Current as App).ConnectionString);

        }


    }
}
