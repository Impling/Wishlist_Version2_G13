using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Wishlist_Version2_G13.Controllers;
using Wishlist_Version2_G13.Data;
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views
{
    public sealed partial class Login : Page
    {
        RuntimeInfo Runtime { get; set; }

        public Login()
        {
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            Runtime.LoginUser("Timo.spanhove@Hotmail.com", "Password1"); //Current strings for testing, replace with viewbox data later on 

            /*
            //TEST DB connection
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();                    
                    
                    //DB connection tests

                    //List<User> users = context.Users.ToList();
                    //context.Users.Add(new User("Testy", "Mctestface", "T.T@gmail.com", "Test1234"));
                    //context.SaveChanges();

                    //List<UserContact> userContacts = context.Contacts.ToList(); //Test get test
                    //List<User> contacts = context.Contacts.Where(c => c.UserId == users[0].UserId).Select(t => t.Contact).ToList(); //Test get test contact
                    //User contact = contacts[0];//Data passed from contact test

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
            */

            //call get textfield password and textfield user, if validated => go to mainpage and set login user to logged in user in runtime

            //Runtime.LoggedInUserId = 1;
            //Runtime.LoggedInUser = Runtime.TestRepos.GetUsers().FirstOrDefault(u => u.UserId == 1);
            //Runtime.SetUserInApp();
            Frame.Navigate(typeof(MainPage)); //mainpage is own wishlists
        }



    }

}
