using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // testDbAsync();


            Runtime.LoggedInUserId = 1;
            Runtime.LoggedInUser = Runtime.TestRepos.GetUsers().FirstOrDefault(u => u.UserId == 1);
            Runtime.SetUserInApp();
            Frame.Navigate(typeof(MainPage)); //mainpage is own wishlists
        }

        public async void testDbAsync()
        {
            TodoItem item = new TodoItem
            {
                Text = "Awesome item",
                Complete = false
            };
            //await App.WishlistVM8ServiceFinalClient.GetTable<TodoItem>().InsertAsync(item);
        }

    }

    public class TodoItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}
