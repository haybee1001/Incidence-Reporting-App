using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageFromXamarinUI;
using Rg.Plugins.Popup.Services;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Success : PopupPage
    {
        public Success(string Tracker)
        {
            InitializeComponent();
            //reportDate.Text = System.DateTime.Today.ToString();
            trackNo.Text = Tracker.ToString();
            //statusCode.Text = "Success!";
        }

        private async void buttonCLicked(object sender, EventArgs e)
        {
             await PopupNavigation.Instance.PopAsync();
            //var imageStream = await root.CaptureImageAsync();
            //resultImage.Source = ImageSource.FromStream( () => imageStream);
        }
    }
}