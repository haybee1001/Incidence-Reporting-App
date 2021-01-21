using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class no_internet : PopupPage
    {
        public no_internet()
        {
            InitializeComponent();
        }


        private async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            // await PopupNavigation.Instance.PopAsync();
            await PopupNavigation.Instance.PopAllAsync();

        }
    }
}