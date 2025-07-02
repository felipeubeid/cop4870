using System.Windows.Input;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{

    public class ItemViewModel
    {
        public Item Model { get; set; }
        
        public ICommand? AddCommand { get; set; }
        
        private void DoAdd()
        {
            ShoppingCartService.Current.AddOrUpdate(Model);
        }

        void SetupCommand()
        {
            AddCommand = new Command(DoAdd);
        }

        public ItemViewModel()
        {
            Model = new Item();
            SetupCommand();
        }

        public ItemViewModel(Item model)
        {
            Model = model;
            SetupCommand();
        }
    }
    
}