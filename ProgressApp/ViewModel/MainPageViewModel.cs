using ProgressApp.Model;
using ProgressApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProgressApp.ViewModel
{
    public class MainPageViewModel :baseViewModel
    { 
        public MainPageViewModel()
        {
            MenuList = GetMenus();
        }
    
        private ObservableCollection<Menu> menuList;

        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set { menuList = value; }
        }

        public ObservableCollection<Menu> GetMenus()
        {
            return new ObservableCollection<Menu>
            {
                new Menu { Name="Home", Icon ="home.png", TargetType = typeof(MainPage)},
                new Menu {Name="Check Status", Icon="view.png", TargetType = typeof(Track)},
                new Menu {Name="About App", Icon="about.png", TargetType = typeof(About)},
                new Menu {Name="Settings", Icon="setting.png", TargetType = typeof(settings)}
            };
        }

    }
}
