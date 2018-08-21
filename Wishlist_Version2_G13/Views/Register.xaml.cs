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

namespace Wishlist_Version2_G13.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {

        RuntimeInfo Runtime { get; set; }

        public Register()
        {
            this.InitializeComponent();
            Runtime = RuntimeInfo.Instance;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            txtError.Text = "";
            txtError.Visibility = Visibility.Collapsed;

            //Check if passwords match
            if (!txtPassword.Password.ToString().Equals(txtPasswordCheck.Password.ToString())) {
                txtError.Text+= "Passwords do not match.\n";
            //Check if Firstname is filled in
            } if (txtFirstname.Text == null || txtFirstname.Text.Equals("")) {
                txtError.Text += "Fill in your first name.\n";
            } if (txtLastname.Text == null || txtLastname.Text.Equals("")) {
                txtError.Text += "Fill in your Last name.\n";
            } if (txtPassword.Password.ToString() == null || txtPassword.Password.Equals("") || txtPassword.Password.ToString().Length <=7) {
                txtError.Text += "Fill in Both password fields (min 8 characters long).\n";
            } if (txtEmail.Text == null || txtEmail.Text.Equals("")) {
                txtError.Text += "Fill in your Email name.\n";
            }

            //If no errors in input found continue with creating user
            if (txtError.Text == "") {

                int success = Runtime.RegisterUser(txtFirstname.Text, txtLastname.Text, txtEmail.Text, txtPassword.Password.ToString());

                if (success == 1)
                {
                    txtError.Text += "User already exists.\n";
                }
                else if (success == 2)
                {
                    txtError.Text += "Failed to create User.\n";

                }
                else if (success == 3)
                {
                    Frame.Navigate(typeof(MainPage)); //mainpage is own wishlists

                }
                else {
                    txtError.Text += "Unknown Error.\n";
                }


            }
            txtError.Visibility = Visibility.Visible;
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
