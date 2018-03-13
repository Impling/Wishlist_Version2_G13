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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views.Social
{
    public sealed partial class SocialView : Page
    {
        RuntimeInfo Runtime;

        public SocialView()
        {
            //dual view top 2 tabs 1 contacts and 1 invites, contacts shows list of people known and other view shows messages sent to user 
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
            SocialFrame.Navigate(typeof(Contacts), Runtime.LoggedInUser);
        }



        public void ViewContacts_Click(object sender, RoutedEventArgs e)
        {
            SocialFrame.Navigate(typeof(Contacts), Runtime.LoggedInUser);
        }
        public void ViewNotifications_Click(object sender, RoutedEventArgs e)
        {
            SocialFrame.Navigate(typeof(Notifications), Runtime.LoggedInUser);
        }
    }
}
