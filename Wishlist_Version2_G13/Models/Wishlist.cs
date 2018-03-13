using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wishlist_Version2_G13.Models
{
    public class Wishlist : INotifyPropertyChanged
    {
        //Variable declaration with getters and setters
        public int WishlistId { get; set; }                //id of whislist
        private String _title = "testtitle";
        public String Title { get { return _title; } set { _title = value; NotifyPropertyChanged("Title"); } }                   //name of wishlist
        public User Owner { get; set; }                    //user that made the wishlist
        public DateTime Deadline { get; set; }         //deadline of event, when it takes place, maybe allow for days before so everything is in order before the deadline
        public string DeadlineS { get; set; }
        private ObservableCollection<User> _buyers = new ObservableCollection<User>();
        //private List<Item> _items = new List<Item>();
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public string Occasion { get; set; }

        public ObservableCollection<User> Buyers { get { return _buyers; } set { _buyers = value; NotifyPropertyChanged("Buyers"); } }              //Users invited to wishlists
        public ObservableCollection<Item> Items { get { return _items; } set { _items = value; NotifyPropertyChanged("Items"); } }
        public Boolean IsOpen { get; set; }


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

        //Constructors
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
            DeadlineS = "Deadline: " + deadline.ToString("ddd dd/MM/yyyy");
        }


        //Functions

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



    }
}
