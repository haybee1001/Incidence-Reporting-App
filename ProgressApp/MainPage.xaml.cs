using ProgressApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.PancakeView;
using System.Threading;
using Rg.Plugins.Popup.Services;
using Plugin.Connectivity;
using System.Net.Http;


namespace ProgressApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        double lat = 0, lon;
        public MainPage()
        {
            //ObtainDeviceLocation();
            InitializeComponent();
            lbldate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;
            var height = mainDisplayInfo.Height;
            var deviceManufacturer = DeviceInfo.Manufacturer;
            var density = mainDisplayInfo.Density;

            if (height <= 1440 && density >= 2.25)
            {
                blueBar.Height = 75;
                lblReportStack.FontSize = 10;
                lbldate.FontSize = 12;
            }
            else if (height < 1560)
            {
                //Set the height of the blue bar
                blueBar.Height = 80;

            }
            else if (height == 1920 && width == 720)
            {
                blueBar.Height = 75;
            }
            else if (height >= 1560 && density == 3)
            {
                blueBar.Height = 75;
            }
            else if (height >= 1560)
            {
                blueBar.Height = 150;
            }

        }


        //public async void AnimateBackground()
        //{
        //    Action<double> forward = input => MenuOverlay.AnchorY = input;
        //    Action<double> backward = input => MenuOverlay.AnchorX = input;

        //    while (true)
        //    {
        //        MenuOverlay.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
        //        await Task.Delay(5000);
        //        MenuOverlay.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
        //        await Task.Delay(5000);
        //    }

        //}


        public async void ObtainDeviceLocation()
        {

                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High);
                    var lc = await Geolocation.GetLocationAsync(request);

                    if (lc != null)
                    {
                        if (lc.IsFromMockProvider)
                        {
                            // location is from a mock provider
                            await DisplayAlert("Mock", "Mock Location Detected!", "Close");
                        }
                        else
                        {
                            lat = lc.Latitude;
                            lon = lc.Longitude;
                            // verifyUserLocation();
                            lbldate.Text = lat.ToString();
                        }
                    }

                }
                catch (FeatureNotSupportedException featureNotSupportedException)
                {

                }
                catch (FeatureNotEnabledException featureNotEnabledException)
                {
                    //> I would force to enable device's gps in this line
                    // await PopupNavigation.Instance.PushAsync(new EnableGPS());
                }
                catch (PermissionException permissionException)
                {
                    AppInfo.ShowSettingsUI();
                }
                catch (Exception exception)
                {

                }
        }

        public void OpenMenu()
        {
            MenuGrid.IsVisible = true;

            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, -260, 0, 16, 360, Easing.CubicInOut);

        }
        public void CloseMenu()
        {
            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, 0, -260, 16, 360, Easing.CubicInOut);
            MenuGrid.IsVisible = false;
        }


        public void OpenMenu2()
        {
            MenuGrid2.IsVisible = true;

            Action<double> callback = input => MenuView2.TranslationX = input;
            MenuView2.Animate("anim", callback, 20, 0, 30, 360, Easing.SinIn);

        }


        public void CloseMenu2()
        {
            Action<double> callback = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callback, 0, -260, 16, 360, Easing.BounceIn);
            MenuGrid2.IsVisible = false;
        }


        public void OpenMenu3()
        {
            MenuGrid3.IsVisible = true;

            Action<double> callback = input => MenuView3.TranslationY = input;
            MenuView3.Animate("anim", callback, 400, 0, 600, 700, Easing.BounceIn);

            AnimationView.IsPlaying = true;

        }

        public void CloseMenu3()
        {
            Action<double> callback = input => MenuView3.TranslationY = input;
            MenuView3.Animate("anim", callback, 0, -260, 16, 360, Easing.BounceOut);
            MenuGrid3.IsVisible = false;
            txtTrackID.Text = "";
        }

        private void overlayTapped(object sender, EventArgs e)
        {
            CloseMenu();
        }

        private void overlayTapped2(object sender, EventArgs e)
        {
            CloseMenu2();
        }
        private void overlayTapped3(object sender, EventArgs e)
        {
            CloseMenu3();
        }

        private void MenuTapped(object sender, EventArgs e)
        {
            OpenMenu();
        }

        private async void MenuItemTapped(object sender, EventArgs e)
        {
            var stack = sender as StackLayout;

            stack.BackgroundColor = Color.FromHex("#A2BEF6");

            await Task.Delay(70); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#3C8DF6");

            var item = ((sender as StackLayout).BindingContext) as Model.Menu;

          
            if (item.TargetType == typeof(MainPage))
            {
                CloseMenu();
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                await Navigation.PushAsync(page);
            }
          

           CloseMenu();

        }

        private void setTapped(object sender, EventArgs e)
        {
            OpenMenu2();
           // OpenMenu3();
        }



        private async void FaultItemTapped(object sender, EventArgs e)
        {
            var stack = sender as PancakeView;

            stack.BackgroundColor = Color.FromHex("#F2F1F1");

            await Task.Delay(100); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#FFFFFF");

            Navigation.PushAsync(new IncidenceReport("Fault"));
        }

        private async void OutageItemTapped(object sender, EventArgs e)
        {
            var stack = sender as PancakeView;

            stack.BackgroundColor = Color.FromHex("#F2F1F1");

            await Task.Delay(100); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#FFFFFF");

            await Navigation.PushAsync(new IncidenceReport("Outage"));
        }

        private async void TrackItemTapped(object sender, EventArgs e)
        {
            var stack = sender as PancakeView;

            stack.BackgroundColor = Color.FromHex("#F2F1F1");

            await Task.Delay(100); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#FFFFFF");
            await Navigation.PushAsync(new Track());
        }

        private async void WhistleItemTapped(object sender, EventArgs e)
        {
            var stack = sender as PancakeView;

            stack.BackgroundColor = Color.FromHex("#F2F1F1");

            await Task.Delay(100); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#FFFFFF");

            await Navigation.PushAsync(new IncidenceReport("Whistle"));
        }

        private async void HazardItemTapped(object sender, EventArgs e)
        {
            var stack = sender as PancakeView;

            stack.BackgroundColor = Color.FromHex("#F2F1F1");

            await Task.Delay(100); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#FFFFFF");
            await Navigation.PushAsync(new IncidenceReport("Hazard"));
        }

        private async void exitButtonClick(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Confirmation());
        }

        private void phoneClicked(object sender, EventArgs e)
        {
            PhoneDialer.Open("084700100");
        }

        [Obsolete]
        private void MessageClicked(object sender, EventArgs e)
        {
            string toEmail = "customerservice@enugudisco.com";
            string emailSubject = "Incidence Report Complain";
            string emailBody = "";

            if (string.IsNullOrEmpty(toEmail))
            {
                return;
            }

            Device.OpenUri(new Uri(String.Format("mailto:{0}?subject={1}&body={2}", toEmail, emailSubject, emailBody)));
        }

        private async void AccountVerification(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                await PopupNavigation.Instance.PushAsync(new loading());

                try
                {
                    using (var client = new HttpClient())
                    {

                        string no = txtTrackID.Text.ToString();
                        client.BaseAddress = new Uri("http://denceapp.somee.com/api/incidence/validateMeterNo/");
                        var responseTask = client.GetAsync(no);

                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {

                            transactionStatus.IsVisible = true;
                            transactionStatus.Text = "Verification Not Successful!";
                            transactionStatus.TextColor = System.Drawing.Color.Red;
                            // Stop the activity indicator
                            //await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            transactionStatus.Text = "Verified Successfully!";
                            await PopupNavigation.Instance.PopAsync();
                            CloseMenu3();
                            await Navigation.PushAsync(new IncidenceReport("Complain"));
                        }

                    }
                }
                catch (Exception ex)
                {
                    await PopupNavigation.Instance.PushAsync(new no_internet());
                    await Navigation.PopAsync();
                    // await PopupNavigation.Instance.PopAsync();
                }
            }


            //    if (txtTrackID.Text == "11111")
            //{
            //    CloseMenu3();
            //    await Navigation.PushAsync(new IncidenceReport("Complain"));
            //}
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            CloseMenu3();
        }

        private void nextClicked(object sender, EventArgs e)
        {
            
        }

        private async void ComplainItemTapped(object sender, EventArgs e)
        {
            var stack = sender as PancakeView;

            stack.BackgroundColor = Color.FromHex("#F2F1F1");

            await Task.Delay(100); // delay 0.1s

            stack.BackgroundColor = Color.FromHex("#FFFFFF");

            OpenMenu3();

            transactionStatus.Text = "";
          
        }


    }
}
