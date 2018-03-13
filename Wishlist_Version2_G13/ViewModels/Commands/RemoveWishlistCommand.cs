using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wishlist_Version2_G13.ViewModels.Commands
{
    class RemoveWishlistCommand : ICommand
    {
        WishlistsViewModel _wishesViewModel;

        public RemoveWishlistCommand(WishlistsViewModel wvm)
        {
            _wishesViewModel = wvm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _wishesViewModel.RemoveWishlistCommand();
        }

    }
}
