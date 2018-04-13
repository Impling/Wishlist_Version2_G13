using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string RestUrl { get; } = "https://wishlistmanager.azurewebsites.net/api/";
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

        public void SetUserInApp()
        {
            AppController.User = this.LoggedInUser;
        }

        public void SetBounds(double height, double width)
        {
            ScreenHeight = height;
            ScreenWidth = width;
        }

    }
}
