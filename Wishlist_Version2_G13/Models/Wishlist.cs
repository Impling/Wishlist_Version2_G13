using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wishlist_Version2_G13.Models
{
    public class Wishlist : INotifyPropertyChanged
    {
        #region Properties
        //Variable declaration with getters and setters
        public int WishlistId { get; set; }                //id of whislist
        private String _title = "testtitle";
        public String Title { get { return _title; } set { _title = value; NotifyPropertyChanged("Title"); } }                   //name of wishlist
        public string Occasion { get; set; }
        public Boolean IsOpen { get; set; }
        public DateTime Deadline { get; set; }         //deadline of event, when it takes place, maybe allow for days before so everything is in order before the deadline


        public virtual UserWishlist WishlistOwner { get; set; }
        public virtual ICollection<WishlistParticipant> Participants { get; set; }
        [NotMapped]
        public virtual List<Item> Gifts { get; set; }

        [NotMapped]
        public User Owner { get; set; }                    //user that made the wishlist

        [NotMapped]
        public string DeadlineS { get; set; }

        [NotMapped]
        private ObservableCollection<User> _buyers = new ObservableCollection<User>();
        //private List<Item> _items = new List<Item>();
        [NotMapped]
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        [NotMapped]
        public ObservableCollection<User> Buyers { get { return _buyers; } set { _buyers = value; NotifyPropertyChanged("Buyers"); } }              //Users invited to wishlists
        [NotMapped]
        public ObservableCollection<Item> Items { get { return _items; } set { _items = value; NotifyPropertyChanged("Items"); } }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //buyers can see who bought wich gift, owner can only see if a gift is bought or not
        //when deadline expires buyers lose access to the wishlist, but not the owner, owner gets to see after deadline who bought what (maybe leave some time between to not spoil things)
        //when item unbought buyer can see box to mark purchase, gives confirmation and buyers get connected to item
        #endregion

        //Constructors
        public Wishlist() {
            Gifts = new List<Item>();
            Items = new ObservableCollection<Item>();
            Buyers = new ObservableCollection<User>();
        }

        public Wishlist(string title, string occasion)  //constructor for favorite wishlist, as those dont have a deadline
        {
            Title = title;
            Occasion = occasion;
            IsOpen = false;


        }
        public Wishlist(User owner, string title, string occasion, DateTime deadline) : this(title, occasion)
        {
            Owner = owner;
            Deadline = deadline;
            SetDeadlineText();
        }


        //Functions
        #region Methods
        //Function 1)Add Event
        public void addDeadline(DateTime deadline)
        {
            Deadline = Deadline;
            DeadlineS = "Deadline: " + deadline.ToString("ddd dd/MM/yyyy");
        }

        //Function 2)AddBuyer
        public void addBuyer(User buyer)
        {
            Buyers.Add(buyer);
        }

        //Function 2)AddItem
        public void addItem(Item item)
        {
            Items.Add(item);
        }

        public void SetDeadlineText() {
            if (Deadline.ToString("ddd dd/MM/yyyy").Equals("MON 01-01-0001"))
            {
                DeadlineS = "No deadline given.";
            }
            else {
                DeadlineS = "Deadline: " + Deadline.ToString("ddd dd/MM/yyyy");
            }
                
        }

        #endregion


    }

    #region classes
    //Extra class for the joined table between Wishlists and their Participants
    public class WishlistParticipant
    {
        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }

        public int ParticipantId { get; set; }
        public virtual User Participant { get; set; }


        public WishlistParticipant(){}
        
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

        public UserWishlist() {}
       

        public UserWishlist(int ownerId, int wishlistId, User owner, Wishlist wishlist, bool isFavorite)
        {

            OwnerId = ownerId;
            WishlistId = wishlistId;

            Owner = Owner;
            Wishlist = wishlist;

            IsFavorite = isFavorite;

        }

    }
    #endregion
}
