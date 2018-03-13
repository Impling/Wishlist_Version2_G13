using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.ViewModels.Commands;

namespace Wishlist_Version2_G13.ViewModels
{
    class ContactViewModel
    {
        public User activeUser { get; set; }
        public User selectedContact { get; set; }
        public Message selectedMessage { get; set; }
        public Wishlist relatedWishlist { get; set; }
        public ObservableCollection<User> PotentialBuyers { get; set; }
        public ObservableCollection<User> selectedBuyers { get; set; }
        public AcceptMessageCommand acceptRequest { get; set; }
        public AddBuyersCommand addBuyers { get; set; }



        public DenyMessageCommand denyRequest { get; set; }



        public ContactViewModel(User u)
        {
            activeUser = u;
            acceptRequest = new AcceptMessageCommand(this);
            denyRequest = new DenyMessageCommand(this);
            addBuyers = new AddBuyersCommand(this);
            selectedBuyers = new ObservableCollection<User>();
        }

        public void SetPotentialBuyers()
        {

            PotentialBuyers = new ObservableCollection<User>();
            foreach (User contact in activeUser.Contacts)
            {
                if (relatedWishlist.Buyers.FirstOrDefault(buyer => contact == buyer) == null)
                {    //potentialbuyers are all contacts that are not yet buyers for wishlist
                    PotentialBuyers.Add(contact);
                }
            }


        }

        public void AddBuyers()
        {
            foreach (User b in selectedBuyers)
            {
                Message m = new Message(activeUser, b, true, relatedWishlist);
                b.addNotification(m);
            }

        }


        public void AcceptRequest()
        {
            selectedMessage.Sender.MyWishlists.FirstOrDefault(w => w == selectedMessage.RelatedWishlist).addBuyer(activeUser);
            selectedMessage.IsAccepted = true;
        }

        public void DenyRequest()
        {
            selectedMessage.IsAccepted = true;  //is accepted means that the message was responded to with noting else done
        }



    }
}
