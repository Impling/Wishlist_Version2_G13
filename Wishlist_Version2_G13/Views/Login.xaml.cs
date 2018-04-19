using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            testDbAsync();

            Runtime.LoggedInUserId = 1;
            Runtime.LoggedInUser = Runtime.TestRepos.GetUsers().FirstOrDefault(u => u.UserId == 1);
            Runtime.SetUserInApp();
            Frame.Navigate(typeof(MainPage)); //mainpage is own wishlists
        }

        public async void testDbAsync()
        {

            /*
            TodoItem item = new TodoItem
            {
                Text = "Awesome item",
                Complete = false
            };
            try
            {
                await App.MobileService.GetTable<TodoItem>().InsertAsync(item);
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            */
            

            

            //GET TEST
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri(Runtime.RestUrl + "user"));

            //Post TEST
            var item = new TodoItem()
            {
                Name = "Awesome item",
                IsComplete = false
            };

            //Post works
            //var itemJson = JsonConvert.SerializeObject(item);
            //var res = await client.PostAsync(Runtime.RestUrl +"todo", new StringContent(itemJson, System.Text.Encoding.UTF16, "application/json"));
            
            //UPDATE TEST


            //DELETE TEST
            
        }

    }

}
