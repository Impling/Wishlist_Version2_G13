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
    public sealed partial class AddBuyers : Page
    {
        RuntimeInfo Runtime { get; }
        ContactViewModel ContactViewModel { get; set; }

        public AddBuyers()
        {
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Wishlist w = e.Parameter as Wishlist;
            //Check if passed
            if (w != null)
            {
                ContactViewModel = new ContactViewModel();    //pass a selected wishlist to model so both contacts and wishlist can get updated
                ContactViewModel.relatedWishlist = w;
                ContactViewModel.SetPotentialBuyers();
                DataContext = ContactViewModel;
            }

        }

        private void SelectionChanged_buyer(object sender, SelectionChangedEventArgs e)
        {

            if (myContacts.SelectedItem != null)
            {

            }

            var listBox = sender as ListBox;
            //get unselected item
            var unselectedPerson = e.RemovedItems.FirstOrDefault() as User;
            if (unselectedPerson != null)
            {
                //get unselected item container
                var unselectedItemContainer = listBox.ContainerFromItem(unselectedPerson) as ListBoxItem;
                //set ContentTemplate
                if (unselectedItemContainer != null)//To prevent crash on attempt to unselect removed object
                {
                    ContactViewModel.selectedBuyers.Remove(unselectedPerson);
                }
            }
            else
            {
                //get selected item container
                var selectedItemContainer = listBox.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;
                if (selectedItemContainer != null)//To prevent crash on removing the selected object
                {
                    ContactViewModel.selectedBuyers.Add(ContactViewModel.selectedContact);
                }
            }

        }

        private void Button_AddBuyers(object sender, RoutedEventArgs e)
        {

            //Call Contactview model function Addbuyer -> check if users already participating

            Frame.Navigate(typeof(WishListPage), ContactViewModel.relatedWishlist);
        }

        public void SetupLayout()
        {
            myContacts.Width = Double.NaN;//Do to listbox that changes based on selection, width=auto does not really work, needs this to be set correctly
            myContacts.Height = Double.NaN;

            Runtime.RefreshSize();
            myContacts.MaxHeight = Runtime.ScreenHeight - 100;
        }

    }
}
