using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Controllers;
using Wishlist_Version2_G13.Models;
using Wishlist_Version2_G13.ViewModels.Commands;

namespace Wishlist_Version2_G13.ViewModels
{
    class WishlistViewModel
    {
        //Variables
        RuntimeInfo Runtime;

        public User activeUser { get; set; }
        public User selectedUser { get; set; }  //for profileview
        public Wishlist selectedWishlist { get; set; }
        public Item seletedItem { get; set; }
        public RemoveWishlistItemCommand removeItemCommand { get; set; }
        public BuyItemCommand buyItemCommand { get; set; }

        public WishlistViewModel(Wishlist w)
        {
            Runtime = RuntimeInfo.Instance;

            activeUser = Runtime.LoggedInUser;
            selectedWishlist = Runtime.SetupSelectedWishlist(w);
            removeItemCommand = new RemoveWishlistItemCommand(this);
            buyItemCommand = new BuyItemCommand(this);
        }

        public void AddItem(Item item)
        {
            selectedWishlist.Items.Add(item);
        }

        public void AddBuyers(List<User> buyers)
        {
            foreach (User b in buyers)
            {
                selectedWishlist.Buyers.Add(b);
            }
        }

        public void RemoveItem()
        {
            selectedWishlist.Items.Remove(seletedItem);
        }

        public void BuyItem()
        {
            //validations
            //A single user can only buy an item once - should already be checked when creating buy item button but just in case -> buttonvisibility checks if bought in general so this should never be necesary, but button remains after is pressed so user could push it multiple times not that that would have any effect as he would litarly be setting himself
            if (!CheckUserAlreadyBought())   //if user hasnt bought anything yet he can buy
                seletedItem.Buyer = activeUser;
            //Small update 
        }

        public bool CheckUserAlreadyBought()
        {
            if (selectedWishlist.Items.FirstOrDefault(item => item.Buyer == activeUser) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }





    }
}
