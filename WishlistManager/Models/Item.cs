using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class Item
    {
        #region Properties
        public int ItemId { get; set; }                //Id of wishlist item
        public string Name { get; set; }            //Name of item
        public string Description { get; set; }     //Description of item
        public string WebLink { get; set; }         //String of url to item website
        public string Image { get; set; }           //String of url to item image
        public string CategoryName { get; set; }       //String of category enum

        public int? BuyerId { get; set; }
        public virtual User Buyer { get; set; }           //Id of user that bought the item
        public WishlistItem  Wishlist{ get; set; }        //Id of Wishlist item belongs to, item should not be shared between wishlists as the users create their own, so even for the same gift the information should vary.
        #endregion

        #region Constructors
        public Item(string name, string category)
        {
            Name = name;
            CategoryName = category;
            Image = "/Images/testImage.png"; //placeholder value to prevent uri conversion error
            WebLink = "https://stackoverflow.com/questions/2552853/how-to-bind-multiple-values-to-a-single-wpf-textblock";  //placeholder weblink to prevent uri conversion error - look into how to disable when no weblink given
        }
        public Item(string name, string category, string description) : this(name, category)
        {
            Description = description;
        }

        public Item(string name, string category, string description, string imageLink) : this(name, category, description)
        {
            Image = imageLink;
        }

        #endregion

    }

}
