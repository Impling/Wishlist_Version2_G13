using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wishlist_Version2_G13.Models
{
    public class Message
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Boolean IsInvite { get; set; }              //Is invite to join wishlist or is request by friend to join wishlist
        public Boolean IsAccepted { get; set; }            //not really needed if we delete messages that have been handled, however if we want to keep messagelog but not allow another accept we could use this
        public Wishlist RelatedWishlist { get; set; }      //wishlist to join or invite to
        public String MessageContent { get; set; }
        public DateTime DateCreated { get; set; }


        //Constructors
        public Message(User loggedInUser, User selectedContact, Boolean isInvite, Wishlist relatedWishlist)
        {
            Sender = loggedInUser;
            Receiver = selectedContact;
            IsInvite = isInvite;
            RelatedWishlist = relatedWishlist;

            IsAccepted = false;
            GenerateMessage();
            DateCreated = System.DateTime.Now;
        }

        private void GenerateMessage()
        {

            if (IsInvite)
            {
                MessageContent = String.Format("{0} {1}: Heeft u uitgenodigd om deel te nemen aan wishlist {2} ", Sender.Firstname, Sender.Lastname, RelatedWishlist.Title);
            }
            else
            {
                MessageContent = String.Format("{0} {1} wenst deel te nemen aan wishlist {2} ", Sender.Firstname, Sender.Lastname, RelatedWishlist.Title);
            }

        }

    }
}
