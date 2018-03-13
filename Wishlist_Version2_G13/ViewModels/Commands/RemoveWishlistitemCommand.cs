using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wishlist_Version2_G13.ViewModels.Commands
{
    class RemoveWishlistItemCommand : ICommand
    {
        WishlistViewModel _wishViewModel;

        public RemoveWishlistItemCommand(WishlistViewModel wvm)
        {
            _wishViewModel = wvm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _wishViewModel.RemoveItem();
        }

    }
}
