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
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.ViewModels;
using Wishlist_Version2_G13.Views.OwnWishlists;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.Profile
{
    public sealed partial class ProfileView : Page
    {
        RuntimeInfo Runtime { get; set; }
        WishlistViewModel WishlistViewModel { get; set; }

        public ProfileView()
        {
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User selectedUser = e.Parameter as User;
            Wishlist f = Runtime.AppController.GetFavoritesByUserId(selectedUser.UserId);
            f = Runtime.AppController.SetupSelectedWishlist(f);
            f.Owner = selectedUser;

            WishlistViewModel = new WishlistViewModel(f);
            WishlistViewModel.selectedUser = selectedUser;

            FavoriteFrame.Navigate(typeof(FavoriteWishes), selectedUser);

            DataContext = WishlistViewModel;
        }



    }
}
