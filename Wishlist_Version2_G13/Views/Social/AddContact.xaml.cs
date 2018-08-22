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
using Wishlist_Version2_G13.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.Social
{
    public sealed partial class AddContact : Page
    {
        ContactViewModel ContactViewModel;

        public AddContact()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ContactViewModel = e.Parameter as ContactViewModel;
            DataContext = ContactViewModel;
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = ContactViewModel.AddContact(txtEmail.Text);
            txtError.Visibility = Visibility.Visible;
        }


    }
}
