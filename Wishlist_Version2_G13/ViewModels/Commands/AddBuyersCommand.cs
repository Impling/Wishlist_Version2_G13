using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wishlist_Version2_G13.ViewModels.Commands
{
    class AddBuyersCommand : ICommand
    {
        //migth need to be put into itemviewmodel
        ContactViewModel _contactViewModel;

        public AddBuyersCommand(ContactViewModel cvm)
        {
            _contactViewModel = cvm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object item)
        {
            _contactViewModel.AddBuyers();
        }
    }
}
