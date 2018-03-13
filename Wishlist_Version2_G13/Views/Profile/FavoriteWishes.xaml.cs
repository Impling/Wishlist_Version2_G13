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

            FavoriteGifts.Height = Runtime.ScreenHeight / 1.5;
            FavoriteGifts.Width = Runtime.ScreenWidth - 40;

        }

        private void SelectionChanged_Item(object sender, SelectionChangedEventArgs e)
        {
            if (FavoriteGifts.SelectedItem != null)
            {
                WishlistViewModel.seletedItem = (Item)FavoriteGifts.SelectedItem;
            }

            var listBox = sender as ListBox;
            //get unselected item
            var unselectedPerson = e.RemovedItems.FirstOrDefault() as Item;
            if (unselectedPerson != null)
            {
                //get unselected item container
                var unselectedItem = listBox.ContainerFromItem(unselectedPerson) as ListBoxItem;
                //set ContentTemplate
                unselectedItem.ContentTemplate = (DataTemplate)this.Resources["ItemView"];
            }
            //get selected item container
            var selectedItem = listBox.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;
            selectedItem.ContentTemplate = (DataTemplate)this.Resources["SelectedItemView"];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            WishlistViewModel = e.Parameter as WishlistViewModel;
            DataContext = WishlistViewModel;
        }
    }
}
