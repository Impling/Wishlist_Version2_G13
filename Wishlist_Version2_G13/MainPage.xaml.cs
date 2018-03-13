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
using Wishlist_Version2_G13.Views.OtherWishlists;
using Wishlist_Version2_G13.Views.OwnWishlists;
using Wishlist_Version2_G13.Views.Profile;
using Wishlist_Version2_G13.Views.Social;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13
{
    public sealed partial class MainPage : Page
    {

        RuntimeInfo Runtime { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
            Runtime.SetBounds(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
            MainFrame.Navigate(typeof(Wishlists), Runtime.LoggedInUser);
        }

        //THERE CAN ONLY BE ONE!
        public void SideBarButton_Click(object sender, RoutedEventArgs e)
        {
            SplitNav.IsPaneOpen = !SplitNav.IsPaneOpen;
        }
        public void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(ProfileView), Runtime.LoggedInUser);
        }
        public void ButtonMyWishlists_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Wishlists), Runtime.LoggedInUser);
        }
        public void ButtonOtherWishlists_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(WishlistsOthers), Runtime.LoggedInUser);
        }
        public void ButtonSocial_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(SocialView));
        }



    }
}
