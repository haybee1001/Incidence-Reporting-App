
using ProgressApp.Model;
using ProgressApp.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


[assembly: ExportFont("Roboto-Regular.ttf", Alias = "NormalFont")]
[assembly: ExportFont("Roboto-Medium.ttf", Alias = "MediumFont")]
[assembly: ExportFont("Roboto-Light.ttf", Alias = "LightFont")]
[assembly: ExportFont("Roboto-Bold.ttf", Alias = "BoldFont")]

namespace ProgressApp
{

    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "MediaElement_Experimental", "Brush_Experimental" });
            
            MainPage = new NavigationPage(new MainPage());
           // MainPage = new Storage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
