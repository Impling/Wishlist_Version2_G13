using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
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
            Runtime.SetupSelectedWishlist(w);
            selectedWishlist = Runtime.AppController.SelectedWishlist;
            removeItemCommand = new RemoveWishlistItemCommand(this);
            buyItemCommand = new BuyItemCommand(this);
        }

        public void AddItem(Item item)
        {
            //selectedWishlist.Items.Add(item);
            Runtime.AppController.AddItem(item);
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
            //selectedWishlist.Items.Remove(seletedItem);
            
            Runtime.AppController.RemoveItem(seletedItem);
            selectedWishlist.Items.Remove(seletedItem);
        }

        public void BuyItem()
        {
            //validations
            //A single user can only buy an item once - should already be checked when creating buy item button but just in case -> buttonvisibility checks if bought in general so this should never be necesary, but button remains after is pressed so user could push it multiple times not that that would have any effect as he would litarly be setting himself
            if (!CheckUserAlreadyBought()) //if user hasnt bought anything yet he can buy
            {   
                seletedItem.BuyerId = activeUser.UserId;
                Runtime.AppController.UpdateItem(seletedItem);
            }
            //Small update 
        }

        public bool CheckUserAlreadyBought()
        {
            return Runtime.AppController.CheckIfBought(seletedItem.ItemId);
        }

        
        public async Task SaveToTextAsync(ListBox wishlist) {

            string filename = "Wishlist_" + selectedWishlist.Title + ".txt";
            string listdata = "";

            foreach (Item item in wishlist.Items) {
                string formattext = "----------------------\n";
                formattext += "Item: " + item.Name+"\n";
                formattext += "Category: " + item.CategoryName + "\n";
                if (item.BuyerId != null) 
                    formattext += "ITEM BOUGHT\n";
                formattext += "----------------------\n\n";

                listdata += formattext + "\n";
            }

            //Create file
            StorageFolder storageFolder = KnownFolders.SavedPictures;
            StorageFile sampleFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            //Write file
            await FileIO.WriteTextAsync(sampleFile, listdata);

            /*//Interop not supported
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            string path = storageFolder.Path + filename;
            Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(path);
            object missing = System.Reflection.Missing.Value;
            doc.Content.Text += listdata;
            app.Visible = true;    //Optional
            doc.Save();
            */

        }






    }
}
