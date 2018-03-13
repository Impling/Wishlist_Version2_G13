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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.OwnWishlists
{
    public sealed partial class Wishlists : Page
    {

        RuntimeInfo Runtime;
        WishlistsViewModel WishlistsViewModel { get; set; }

        public Wishlists()
        {
            this.InitializeComponent();

            Runtime = RuntimeInfo.Instance;

            myWishlists.Height = Runtime.ScreenHeight - 150;
            myWishlists.Width = Runtime.ScreenWidth;

        }

        private void SelectionChanged_Wishlist(object sender, SelectionChangedEventArgs e)
        {
            if (myWishlists.SelectedItem != null)
            {
                WishlistsViewModel.SelectedWishlist = (Wishlist)myWishlists.SelectedItem;
                ButtonView.Visibility = Visibility.Visible;
                ButtonRemove.Visibility = Visibility.Visible;
            }

            var listBox = sender as ListBox;
            //get unselected item
            var unselectedWishlist = e.RemovedItems.FirstOrDefault() as Wishlist;
            if (unselectedWishlist != null)
            {
                //get unselected item container
                var unselectedItem = listBox.ContainerFromItem(unselectedWishlist) as ListBoxItem;
                //set ContentTemplate
                if (unselectedItem != null)
                    unselectedItem.ContentTemplate = (DataTemplate)this.Resources["ItemView"];
            }
            //get selected item container
            var selectedItem = listBox.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;
            if (selectedItem != null)
                selectedItem.ContentTemplate = (DataTemplate)this.Resources["SelectedItemView"];
        }
        private void SelectionChanged_Sort(object sender, SelectionChangedEventArgs e)
        {
            WishlistsViewModel.Sort();
        }

        //NAVIGATION FUNCTIONS
        //OnClick NAVIGATION
        public void AddWishlistButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListCreation), WishlistsViewModel);
        }

        public void ViewWishlistButton_Click(object sender, RoutedEventArgs e)
        {
            if (WishlistsViewModel.SelectedWishlist != null)
            {
                Frame.Navigate(typeof(WishListPage), WishlistsViewModel.SelectedWishlist); // replace SelectedWishlist with WishlistsViewModel.SelectedWishlist
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User ActiveUser = e.Parameter as User;
            if (ActiveUser != null)//check if logged in
            {
                WishlistsViewModel = new WishlistsViewModel(ActiveUser);
                DataContext = WishlistsViewModel;
            }
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // //eh? 
            //this.Frame.Navigate(typeof(WishListPage));
        }

        // need functions to change view filter by deadline or name


    }
}
