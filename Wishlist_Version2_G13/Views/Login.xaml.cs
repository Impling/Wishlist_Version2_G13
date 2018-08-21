using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Wishlist_Version2_G13.Controllers;
using Wishlist_Version2_G13.Data;
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.Service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wishlist_Version2_G13.Views
{
    public sealed partial class Login : Page
    {
        RuntimeInfo Runtime { get; set; }

        public Login()
        {

            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
            Runtime.RefreshSize();

        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            txtError.Visibility = Visibility.Collapsed;
            int success = Runtime.LoginUser(txtEmail.Text, txtPassword.Password.ToString());

            if (success == 1)
            {
                txtError.Text = "User not found in Database.";
            }
            else if (success == 2)
            {
                txtError.Text = "Password incorrect.";
            }
            else if (success == 3)
            {
                Frame.Navigate(typeof(MainPage)); //mainpage is own wishlists
            }
            else {
                txtError.Text = "Unknown Error";
            }
            txtError.Visibility = Visibility.Visible;
   
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            Runtime.RefreshSize();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Register));
        }
    }

}
