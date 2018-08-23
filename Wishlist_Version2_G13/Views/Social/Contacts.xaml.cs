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
using Wishlist_Version2_G13.Views.Profile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.Social
{
    public sealed partial class Contacts : Page
    {

        RuntimeInfo Runtime;
        ContactViewModel ContactViewModel;


        public Contacts()
        {
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;

        }


        private void SelectionChanged_Contact(object sender, SelectionChangedEventArgs e)
        {
            if (MyFriends.SelectedItem != null)
            {
                ContactViewModel.selectedContact = (User)MyFriends.SelectedItem;
                ButtonView.Visibility = Visibility.Visible;
            }

            var listBox = sender as ListBox;
            //get unselected item
            var unselectedPerson = e.RemovedItems.FirstOrDefault() as User;
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
            SetupLayout();

            ContactViewModel = new ContactViewModel();
            DataContext = ContactViewModel;
        }

        public void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddContact), ContactViewModel);
        }
        public void ViewDetailButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfileView), ContactViewModel.selectedContact);
        }

        public void SetupLayout()
        {
            MyFriends.Width = Double.NaN;//Do to listbox that changes based on selection, width=auto does not really work, needs this to be set correctly
            MyFriends.Height = Double.NaN;

            Runtime.RefreshSize();
            MyFriends.MaxHeight = Runtime.ScreenHeight - 100;
        }

    }
}
