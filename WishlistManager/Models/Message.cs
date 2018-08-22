using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class Message
    {

        public int MessageId { get; set; }
        public int IdSender { get; set; }               //Sender as id (multiple relations with same class not supported) - try foreign key
        public MessageUser Receiver { get; set; }
        //public Boolean IsInvite { get; set; }              //Is invite to join wishlist or is request by friend to join wishlist, check if needed as property
        public Boolean? IsAccepted { get; set; }            //not really needed if we delete messages that have been handled, however if we want to keep messagelog but not allow another accept we could use this

        public int? WishlistId { get; set; }
        public virtual Wishlist RelatedWishlist { get; set; }      //wishlist to join or invite to
        public String MessageContent { get; set; }
        public DateTime DateCreated { get; set; }

        #region Constructors
        public Message(User sender, User receiver) {
            IdSender = sender.UserId;

            IsAccepted = false;
        }

        //Constructor for invite/request to add to contact list
        public Message(User sender, User receiver, Boolean isInvite) : this(sender, receiver)
        {
            //IsInvite = isInvite;

            GenerateMessageContact(isInvite, sender);
            DateCreated = System.DateTime.Now;
        }

        public Message(User sender, User receiver, Boolean isInvite, Wishlist relatedWishlist) : this(sender, receiver)
        {
            //IsInvite = isInvite;
            RelatedWishlist = relatedWishlist;

            GenerateMessageWishlist(isInvite, sender);
            DateCreated = System.DateTime.Now;
        }
        #endregion

        #region Methods
        private void GenerateMessageContact(bool isInvite, User sender)
        {

            if (isInvite)
            {
                MessageContent = String.Format("{0} {1}: Will u toevoegen aan zijn contacten. ", sender.Firstname, sender.Lastname);
            }
            else
            {
                MessageContent = String.Format("{0} {1}: Zou graag toegevoegd worden aan uw contacten.", sender.Firstname, sender.Lastname);
            }

        }
        private void GenerateMessageWishlist(bool isInvite, User sender)
        {

            if (isInvite)
            {
                MessageContent = String.Format("{0} {1}: Heeft u uitgenodigd om deel te nemen aan wishlist {2}. ", sender.Firstname, sender.Lastname, RelatedWishlist.Title);
            }
            else
            {
                MessageContent = String.Format("{0} {1} wenst deel te nemen aan wishlist {2}. ", sender.Firstname, sender.Lastname, RelatedWishlist.Title);
            }

        }
        #endregion

    }
}
