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
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.OwnWishlists
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListCreation : Page
    {
        WishlistsViewModel WishlistsViewModel { get; set; }

        public ListCreation()
        {
            this.InitializeComponent();
        }


        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            checkboxinfo.Text = "Enkel deelname via uitnodeging.";
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            checkboxinfo.Text = "Vrienden kunnen deelname aanvragen.";

        }


        //NAVIGATION FUNCTIONS
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WishlistsViewModel = e.Parameter as WishlistsViewModel;
            if (WishlistsViewModel != null)//check if logged in
            {
                DataContext = WishlistsViewModel;
            }

        }

        //Onclick funtions
        public void ButtonAdd_Click(object sender, RoutedEventArgs e)   //Could be done with data binding - review later to use binding to also change frame
        {
            //Maybe relocate wishlist creatin into viewmodel and change addwishlist parameters to the data from the screen
            //Create wishlist for user and add to the logged in user, appcontroller connects to database and adds it there as well
            Wishlist w = new Wishlist(WishlistsViewModel.activeUser, Namelist.Text, NameOccasion.Text, eventDatePicker.Date.UtcDateTime);
            w.IsOpen = !checkboxPublic.IsChecked.Value;
            WishlistsViewModel.AddWishlist(w);
            Frame.Navigate(typeof(Wishlists), WishlistsViewModel.activeUser);
        }
        public void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Wishlists), WishlistsViewModel.activeUser);
        }

    }
}
