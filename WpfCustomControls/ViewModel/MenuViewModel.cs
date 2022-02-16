
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WpfCustomControls.CustomControls;
using WpfCustomControls.LanguageModel;
namespace WpfCustomControls.ViewModel
{
   public class MenuViewModel :BaseViewModel
    {
        private ObservableCollection<MenuItem> _MenuItems;
        public ObservableCollection<MenuItem> MenuItems { get { if (_MenuItems == null) { _MenuItems = new ObservableCollection<MenuItem>(); } return _MenuItems; } set { _MenuItems = value; OnPropertyChanged(); } }
        private Languages _Languages;
        public Languages Languages { get { return _Languages; } set { _Languages = value; OnPropertyChanged(); } }
        public MenuViewModel(string addin,Languages languages)
        {
            Languages = languages;
            GetMenu(addin);
            
        }
        private void GetMenu(string addin)
        {
            switch (addin)
            {
                case "R01": break;
                case "R02": break;
                case "R03": break;
                case "R04": break;
                case "R06": break;
                case "R08": break;
                case "R09": break;
                case "R10": break;
                case "R11": GetMenuItemR11(); break;
                default: break;
            }
        }
        private void GetMenuItemR01()
        {
            MenuItems.Add(new MenuItem(Languages.MenuLanguage.Setting));
        }
        private void GetMenuItemR02()
        {

        }
        private void GetMenuItemR03()
        {

        }
        private void GetMenuItemR04()
        {

        }
        private void GetMenuItemR06()
        {

        }
        private void GetMenuItemR08()
        {

        }
        private void GetMenuItemR09()
        {

        }
        private void GetMenuItemR10()
        {

        }
        private void GetMenuItemR11()
        {
            MenuItems.Add(new MenuItem(Languages.MenuLanguage.Setting));
            MenuItems.Add(new MenuItem(Languages.MenuLanguage.Geometry));
            MenuItems.Add(new MenuItem(Languages.MenuLanguage.PileDetail));
            MenuItems.Add(new MenuItem(Languages.MenuLanguage.Reinforcement));
        }
        private Canvas GetCanvas(MenuControl menu)
        {
            IEnumerable<Canvas> canvas = VisualTreeHelper.GetVisualChildren<Canvas>(menu);
            return canvas.Where(x => x.Tag == Languages.MenuLanguage.Setting as object).FirstOrDefault();
        }
    }
    public class MenuItem:BaseViewModel
    {

        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        public MenuItem(string name)
        {
            Name = name;
        }
    }
}
