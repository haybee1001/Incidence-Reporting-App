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
    public partial class submitSuccess : PopupPage
    {
        public submitSuccess(string Tracker)
        {
            InitializeComponent();

            reportDate.Text = System.DateTime.Today.ToString();
            trackNo.Text = Tracker.ToString();
            statusCode.Text = "Success!";

        }

        private void BtnCloseTicket(object sender, EventArgs e)
        {
           // PopupNavigation.Instance.PopAsync();
        }
    }
}