using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using Newtonsoft.Json;
using System.Net.Http;
using ProgressApp.Model;
using Xamarin.Essentials;

using Rg.Plugins.Popup.Pages;

using Plugin.Connectivity;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Track : ContentPage
    {
        public Track()
        {
            InitializeComponent();
           

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;
            var height = mainDisplayInfo.Height;
            var deviceManufacturer = DeviceInfo.Manufacturer;
            var density = mainDisplayInfo.Density;

            if (height <= 1440 && density >= 2.25)
            {
                bluebar.Height = 130;
                summaryView.TranslationY = -40;
            }
            else if (height < 1560)
            {
                //Set the height of the blue bar
                bluebar.Height = 150;
                summaryView.TranslationY = -40;
            }
            else if (height == 1920 && width == 720)
            {
                bluebar.Height = 150;
                summaryView.TranslationY = -40;
            }
            else if (height >= 1560 && density == 3)
            {
                bluebar.Height = 150;
                summaryView.TranslationY = -40;
            }
            else if (height >= 1560)
            {

            }


            gridCall.IsVisible = false;
         
        }

        private async void backClicked(object sender, EventArgs e)
        {

            Navigation.PopAsync();

        }

        private async void trackReport(object sender, EventArgs e)
        {
            if (txtTrackID.Text == null || txtTrackID.Text.Length <= 2)
            {
                summaryView.IsVisible = false;
                noRecord.IsVisible = true;
                //errorCrossAnimation.IsVisible = true;
                //txtNoRecord.IsVisible = true;
                //txtNoRecord.Text = "Please input a Track ID";
            }
            else
            {

                // Required: Check user connection to see when there is network or when the user gets out of network 
                await PopupNavigation.Instance.PushAsync(new loading());

                List<incidenceApp> reservationList = new List<incidenceApp>();
                string trackId = txtTrackID.Text.ToString();

                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {

                            client.BaseAddress = new Uri("http://denceapp.somee.com/api/incidence/getStatus/");
                            var responseTask = client.GetAsync(trackId);

                            responseTask.Wait();

                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                string readTask = await result.Content.ReadAsStringAsync();
                                reservationList = JsonConvert.DeserializeObject<List<incidenceApp>>(readTask);

                                if (reservationList == null)
                                {

                                     summaryView.IsVisible = false;
                                     noRecord.IsVisible = true;
                                     gridCall.IsVisible = false;
                                    //errorCrossAnimation.IsVisible = true;
                                    // errorCrossAnimation.IsPlaying = true;
                                    // txtNoRecord.IsVisible = true;
                                    //  txtNoRecord.Text = "Tracking ID searched not found!!!";
                                    await PopupNavigation.Instance.PopAsync();
                                }
                                else
                                {
                                    //await PopupNavigation.Instance.PushAsync(new loading());

                                    foreach (var data in reservationList)
                                    {

                                        lblTrackID.Text = data.incidenceId.ToString();
                                        lblReporterName.Text = data.reporterFullname.ToString().ToUpper();
                                        //lblIssue.Text = data.category.ToString().ToUpper();
                                        lblReportDate.Text = data.capturedIssueDate.ToString().ToString();
                                        lblStatus.Text = "IN PROGRESS";
                                        tDistrict.Text = "OGUI";

                                        //if (data.feederName == null)
                                        //{
                                        //    //tFeeder.Text = "N/A";
                                        //}
                                        //else
                                        //{
                                        //    // tFeeder.Text = data.feederName.ToString();
                                        //}

                                    }

                                    await PopupNavigation.Instance.PopAsync();
                                    noRecord.IsVisible = false;
                                    summaryView.IsVisible = true;
                                    gridCall.IsVisible = true;
                                }
                                //Bind data from the JsonResponse to Controls
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        //await DisplayAlert(" App Crash Resist", "An Unknown Error Occoured. Please check to ensure your connection is not lost", "Close");
                        await PopupNavigation.Instance.PushAsync(new no_internet());
                        await PopupNavigation.Instance.PopAsync();
                    }
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new no_internet());
                }
            }
        }

        private void callClicked(object sender, EventArgs e)
        {
            PhoneDialer.Open("084700100");
        }

        private void backTapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}