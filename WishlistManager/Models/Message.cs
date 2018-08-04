using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class Message
    {

        public int MessageId { get; set; }
        public int Sender { get; set; }               //Sender as id (multiple relations with same class not supported) - try foreign key
        public User Receiver { get; set; }
        public Boolean IsInvite { get; set; }              //Is invite to join wishlist or is request by friend to join wishlist
        public Boolean IsAccepted { get; set; }            //not really needed if we delete messages that have been handled, however if we want to keep messagelog but not allow another accept we could use this
        public Wishlist RelatedWishlist { get; set; }      //wishlist to join or invite to
        public String MessageContent { get; set; }
        public DateTime DateCreated { get; set; }

        #region Constructors
        public Message(int sender, User receiver) {
            Sender = sender;
            Receiver = receiver;

            IsAccepted = false;
        }

        //Constructor for invite/request to add to contact list
        public Message(int sender, User receiver, Boolean isInvite) : this(sender, receiver)
        {
            IsInvite = isInvite;

            GenerateMessageContact();
            DateCreated = System.DateTime.Now;
        }

        public Message(int sender, User receiver, Boolean isInvite, Wishlist relatedWishlist) : this(sender, receiver)
        {
            IsInvite = isInvite;
            RelatedWishlist = relatedWishlist;

            GenerateMessageWishlist();
            DateCreated = System.DateTime.Now;
        }
        #endregion

        #region Methods
        private void GenerateMessageContact()
        {

            if (IsInvite)
            {
                MessageContent = String.Format("{0} {1}: Will u toevoegen aan zijn contacten. ", Sender.Firstname, Sender.Lastname);
            }
            else
            {
                MessageContent = String.Format("{0} {1}: Zou graag toegevoegd worden aan uw contacten.", Sender.Firstname, Sender.Lastname);
            }

        }
        private void GenerateMessageWishlist()
        {

            if (IsInvite)
            {
                MessageContent = String.Format("{0} {1}: Heeft u uitgenodigd om deel te nemen aan wishlist {2}. ", Sender.Firstname, Sender.Lastname, RelatedWishlist.Title);
            }
            else
            {
                MessageContent = String.Format("{0} {1} wenst deel te nemen aan wishlist {2}. ", Sender.Firstname, Sender.Lastname, RelatedWishlist.Title);
            }

        }
        #endregion

    }
}
