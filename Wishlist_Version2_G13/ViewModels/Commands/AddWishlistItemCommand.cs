using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wishlist_Version2_G13.ViewModels.Commands
{
    class AddWishlistItemCommand : ICommand
    {
        //migth need to be put into itemviewmodel
        WishlistViewModel _wishViewModel;

        public AddWishlistItemCommand(WishlistViewModel wvm)
        {
            _wishViewModel = wvm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object item)
        {
            _wishViewModel.AddItem(item as Item);
        }
    }
}
