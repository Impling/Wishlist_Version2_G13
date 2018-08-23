using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Wishlist_Version2_G13.Controllers;
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.ViewModels.Commands;
using Wishlist_Version2_G13.Views.Social;

namespace Wishlist_Version2_G13.ViewModels
{
    class ContactViewModel
    {
        RuntimeInfo Runtime { get; }
        public User activeUser { get; set; }
        public User selectedContact { get; set; }
        public Message selectedMessage { get; set; }
        public Wishlist relatedWishlist { get; set; }
        public ObservableCollection<User> PotentialBuyers { get; set; }
        public ObservableCollection<User> selectedBuyers { get; set; }
        public AcceptMessageCommand acceptRequest { get; set; }
        public AddBuyersCommand addBuyers { get; set; }
        public Frame SocialFrame { get; set; }



        public DenyMessageCommand denyRequest { get; set; }

        public ContactViewModel()
        {
            Runtime = RuntimeInfo.Instance;
            activeUser = Runtime.LoggedInUser;
            acceptRequest = new AcceptMessageCommand(this);
            denyRequest = new DenyMessageCommand(this);
            addBuyers = new AddBuyersCommand(this);
            selectedBuyers = new ObservableCollection<User>();
        }

        public void SetPotentialBuyers()
        {
            PotentialBuyers = new ObservableCollection<User>();
            //Edit to only show contacts not yet participating
            foreach (User b in Runtime.AppController.GetPotentialBuyers(relatedWishlist.WishlistId)) {
                PotentialBuyers.Add(b);
            }
        }

        public void AddBuyers()
        {
            foreach (User b in selectedBuyers)
            {
                Message m = new Message(activeUser, b, true, relatedWishlist);
                b.addNotification(m);
                //If no request pending send message
                if (Runtime.AppController.CheckIfMessageExists(m) == null)
                {
                    Runtime.AppController.SendJoinWishlistInvite(b.Email, relatedWishlist.WishlistId);
                }
            }

        }


        //Check if contact can be added, if so send request message
        public string AddContact(string email) {
            string msg = "";

            //Check if email field has been filled in
            if (email == null || email.Equals(""))
            {
                msg += "Fill in your Email name.\n";
                return msg;
            }
            //Check if valid email format - left out since testpresentation can use invalid emails
            /*
            else if (IsValidEmail(email))
            {
                msg += "Invalid email format.\n";
                return msg;
            }
            */

            //If no input errors found
            if (msg == "")
            {
                //Check if email exists in database
                if (Runtime.AppController.GetUserByEmail(email) != null)
                {
                    //Check if already one of your contacts
                    if (Runtime.AppController.CheckContactList(email))
                    {
                        msg += "Request has been send to contact\n";
                        Runtime.AppController.SendContactInvite(email);
                    }
                    else
                    {
                        msg += "This is already one of your contacts\n";
                    }
                }
                else
                {
                    msg += "User does not exist in database";
                }

            }
            else {
                msg += "Unknown error.\n";
                return msg;
            }
            return msg;

        }


        public void AcceptRequest()
        {
            //selectedMessage.Sender.MyWishlists.FirstOrDefault(w => w == selectedMessage.RelatedWishlist).addBuyer(activeUser);
            selectedMessage.IsAccepted = true;
            
            Runtime.AppController.RespondToMessage(selectedMessage);
            //Update view
            activeUser.Notifications.Remove(selectedMessage);
            selectedMessage = null;
            SocialFrame.Navigate(typeof(Notifications), SocialFrame);

        }

        public void DenyRequest()
        {
            selectedMessage.IsAccepted = false;  //is accepted means that the message was responded to with noting else done
            //update message in db
            Runtime.AppController.UpdateMessage(selectedMessage);
            //Update view
            activeUser.Notifications.Remove(selectedMessage);
            selectedMessage = null;
            SocialFrame.Navigate(typeof(Notifications), SocialFrame);
        }

        //Is valid email
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
