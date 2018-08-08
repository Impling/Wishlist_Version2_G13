using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class Wishlist
    {

        #region Properties
        public int WishlistId { get; set; }
        public string Title { get; set; }           //Title,name of wishlist
        public string Occasion { get; set; }        //Descriptor of event
        public bool IsOpen { get; set; }            //Bool for accesslevel of wishlist
        public DateTime Deadline { get; set; }      //Deadline wishlist event, ?Does this need to be a string for web service

        public virtual UserWishlist WishlistOwner { get; set; }
        public virtual ICollection<WishlistParticipant> Participants { get; set; }
        public virtual List<Item> Gifts { get; set; }

        public int NrOfParticipants => Participants.Count;

        #endregion

        #region Constructors
        protected Wishlist()
        {
            IsOpen = true;                  //already set in buildmodel
            Deadline = new DateTime();      //If not set use default datetime
            Participants = new List<WishlistParticipant>();
            Gifts = new List<Item>();
        }

        public Wishlist(string title, string description) : this()
        {
            Title = title;
            Occasion = description;
        }
        public Wishlist(string title, string description, bool isOpen) : this()
        {
            Title = title;
            Occasion = description;
            IsOpen = isOpen;
        }
        public Wishlist(string title, string description, bool isOpen, DateTime deadline) : this()
        {
            Title = title;
            Occasion = description;
            IsOpen = isOpen;
            Deadline = deadline;
        }

        #endregion

        #region Methods
        /*
        public void addParticipant(User user) {
            Participants.Add(user);
        }

        public void addParticipants(List<User> users) {
            users.ForEach(u => Participants.Add(u));
        }
        */
        public void AddGift(Item gift) {
            Gifts.Add(gift);
            //gift.WishlistId = this.WishlistId;
        }

        #endregion
    }

    //Extra class for the joined table between Wishlists and their Participants
    public class WishlistParticipant
    {
        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }

        public int ParticipantId { get; set; }
        public virtual User Participant { get; set; }

        public WishlistParticipant(int wishlistId, int participantId, Wishlist wishlist, User participant)
        {

            WishlistId = wishlistId;
            ParticipantId = participantId;

            Wishlist = wishlist;
            Participant = participant;

        }

    }

    //Extra class for the joined table between Wishlists and their owner allowing differentiation between favorite wishlist and created wishlists
    public class UserWishlist
    {
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }

        public bool IsFavorite { get; set; }

        public UserWishlist(int ownerId, int wishlistId, User owner, Wishlist wishlist, bool isFavorite)
        {

            OwnerId = ownerId;
            WishlistId = wishlistId;

            Owner = Owner;
            Wishlist = wishlist;

            IsFavorite = isFavorite;

        }

    }
}
