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
    public sealed partial class FavoriteWishes : Page
    {
        RuntimeInfo Runtime { get; }
        WishlistViewModel WishlistViewModel { get; set; }


        public FavoriteWishes()
        {

            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;


        }

        private void SelectionChanged_Item(object sender, SelectionChangedEventArgs e)
        {
            if (FavoriteGifts.SelectedItem != null)
            {
                WishlistViewModel.seletedItem = (Item)FavoriteGifts.SelectedItem;
            }

            var listBox = sender as ListBox;
            //get unselected item
            var unselectedGift = e.RemovedItems.FirstOrDefault() as Item;
            if (unselectedGift != null)
            {
                ButtonRemove.Visibility = Visibility.Collapsed;
                //get unselected item container
                var unselectedItem = listBox.ContainerFromItem(unselectedGift) as ListBoxItem;
                //set ContentTemplate
                if(unselectedItem != null)
                    unselectedItem.ContentTemplate = (DataTemplate)this.Resources["ItemView"];
            }
            //check if remove button should be shown
            if (CheckIfLoggedInUser())
                ButtonRemove.Visibility = Visibility.Visible;
            //get selected item container
            var selectedItem = listBox.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;
            if(selectedItem != null)
                selectedItem.ContentTemplate = (DataTemplate)this.Resources["SelectedItemView"];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetupLayout();

            //Extra read from db in case of contact user
            User selectedUser = e.Parameter as User;
            Wishlist f = Runtime.AppController.GetFavoritesByUserId(selectedUser.UserId);
            f = Runtime.AppController.SetupSelectedWishlist(f);

            WishlistViewModel = new WishlistViewModel(f);
            WishlistViewModel.selectedUser = selectedUser;

            CheckIfLoggedInUser();

            DataContext = WishlistViewModel;
        }

        public void AddFavorite_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(NewItem), WishlistViewModel);
        }

        public void SetupLayout()
        {
            FavoriteGifts.Width = Double.NaN;//Do to listbox that changes based on selection, width=auto does not really work, needs this to be set correctly
            FavoriteGifts.Height = Double.NaN;

            Runtime.RefreshSize();
            FavoriteGifts.MaxHeight = Runtime.ScreenHeight - 100;
        }

        public bool CheckIfLoggedInUser() {
            //Check if logged in user is checking own
            if (WishlistViewModel.selectedWishlist.Owner.UserId == Runtime.LoggedInUserId)
            {
                ButtonAdd.Visibility = Visibility.Visible;
                return true;
            }
            else
            {
                ButtonAdd.Visibility = Visibility.Collapsed;
                return false;
            }
        }
    }
}
