using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TestRepos = new TestRepository();
            AppController = new AppController();

        }

        public void SetUserInApp(User u)
        {
            LoggedInUser = u;
            LoggedInUserId = u.UserId;
            AppController.SetupLoggedInUser(u);
            
        }

        public bool LoginUser(string email, string password) {  //Throw error on failure

            //Check if user exists in database
            User user = AppController.LoginUser(email, password);

            if (user == null)
            {
                //Message: User not found in database please try again
                return false;
            }
            else if (!user.Password.Equals(password))
            {
                //Message: The given password was incorrect.
                return false;
            }
            else
            {
                //Message: Login successfull
                SetUserInApp(user);
                return true;
            }

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
