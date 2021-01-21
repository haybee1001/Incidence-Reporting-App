using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;


namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class network : PopupPage
    {
        public network()
        {
            InitializeComponent();
        }

        private async void btnClosed(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}