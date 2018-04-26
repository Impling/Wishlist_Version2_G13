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
        //public int  UserId { get; set; }          //Wishlist owner
        public User User { get; set; }
        public DateTime Deadline { get; set; }      //Deadline wishlist event, ?Does this need to be a string for web service
        public ICollection<User> Participants { get; set; }
        //public List<WishItem> Gifts { get; set; } = new List<WishItem>();
        public string Occasion { get; set; }        //Descriptor of event
        public bool IsOpen { get; set; }            //Bool for accesslevel of wishlist

        public int NrOfParticipants => Participants.Count;

        #endregion

        #region Constructors
        protected Wishlist()
        {
            IsOpen = true; //already set in buildmodel
            Deadline = new DateTime(); //If not set use default datetime
            Participants = new HashSet<User>();
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
        public void addParticipant(User user) {
            Participants.Add(user);
        }

        public void addParticipants(List<User> users) {
            users.ForEach(u => Participants.Add(u));
        }

        #endregion
    }
}
