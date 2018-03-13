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
using Wishlist_Version2_G13.Views.Profile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.OwnWishlists
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewItem : Page
    {

        WishlistViewModel WishlistViewModel { get; set; }

        public NewItem()
        {
            this.InitializeComponent();
        }


        public void ButtonAdd_Click(object sender, RoutedEventArgs e)   //can only be clicked when given a 
        {
            //Create wishlist for user and add to the logged in user, appcontroller connects to database and adds it there as well
            Item i = new Item(NameWish.Text, (Category)CategoryBox.SelectedItem);
            if (!(WebLink.Text == null || WebLink.Text == ""))
                i.WebLink = WebLink.Text;
            i.Description = DescriptionItem.Text;

            WishlistViewModel.AddItem(i);
            //end testcode
            if (WishlistViewModel.selectedWishlist.Title == "Mijn favoriete cadeau's")
            {
                Frame.Navigate(typeof(FavoriteWishes), WishlistViewModel);
            }
            else
            {
                Frame.Navigate(typeof(WishListPage), WishlistViewModel.selectedWishlist);
            }

        }
        public void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            if (WishlistViewModel.selectedWishlist.Title == "Mijn favoriete cadeau's")
            {
                Frame.Navigate(typeof(FavoriteWishes), WishlistViewModel);
            }
            else
            {
                Frame.Navigate(typeof(WishListPage), WishlistViewModel.selectedWishlist);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            WishlistViewModel = e.Parameter as WishlistViewModel;
            //Check if passed
            WishlistName.Text = "Voor Wishlist: " + WishlistViewModel.selectedWishlist.Title;
            CategoryBox.ItemsSource = Enum.GetValues(typeof(Category));
            CategoryBox.SelectedIndex = 0; //can only be done here as contents of list are only initialized in the line above

        }

    }
}
