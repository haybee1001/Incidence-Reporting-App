using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnableGPS : PopupPage
    {
        public EnableGPS()
        {
            InitializeComponent();
        }

        public async void EnableUserGPS(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await PopupNavigation.Instance.PopAsync();
        }
    }
}