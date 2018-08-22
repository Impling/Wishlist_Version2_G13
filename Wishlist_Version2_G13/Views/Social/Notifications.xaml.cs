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

namespace Wishlist_Version2_G13.Views.Social
{
    public sealed partial class Notifications : Page
    {
        ContactViewModel ContactViewModel { get; set; }

        public Notifications()
        {
            this.InitializeComponent();

        }


        //listview layout manipulation based on selected item
        private void SelectionChanged_Message(object sender, SelectionChangedEventArgs e)
        {

            //only allow accept or deny when message has not been responded to and something is indeed selected
            if (myMessages.SelectedItem != null && ContactViewModel.selectedMessage.IsAccepted == null)
            {
                ButtonAccept.Visibility = Visibility.Visible;
                ButtonDeny.Visibility = Visibility.Visible;
            }

            var listBox = sender as ListBox;
            //get unselected item
            var unselectedItem = e.RemovedItems.FirstOrDefault() as Message;
            if (unselectedItem != null)
            {
                //get unselected item container
                var unselectedItemContainer = listBox.ContainerFromItem(unselectedItem) as ListBoxItem;
                //set ContentTemplate
                unselectedItemContainer.ContentTemplate = (DataTemplate)this.Resources["ItemView"];

            }
            //get selected item container
            var selectedItemContainer = listBox.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;
            selectedItemContainer.ContentTemplate = (DataTemplate)this.Resources["SelectedItemView"];


        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User ActiveUser = e.Parameter as User;
            if (ActiveUser != null)//check if logged in
            {
                ContactViewModel = new ContactViewModel();
                DataContext = ContactViewModel;
            }
        }
    }
}
