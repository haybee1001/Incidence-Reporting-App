using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Web;
using System.Net;
using Plugin.Media;

using System.IO;
using Plugin.Media.Abstractions;
using System.Text.RegularExpressions;

using System.Net.Http;

using Polly;


using Rg.Plugins.Popup.Services;

using Xamarin.Essentials;
using ProgressApp.Model;

using Newtonsoft.Json;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncidenceReport : ContentPage
    {
        static string forname;
        static double lat, lon;

        public MediaFile _received;
        private string URL { get; set; }

        static string imageURL;

        string userTrackID;
        public IncidenceReport(string reporttype)
        {
            ObtainDeviceLocation();
            InitializeComponent();


            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;
            var height = mainDisplayInfo.Height;
            var deviceManufacturer = DeviceInfo.Manufacturer;
            var density = mainDisplayInfo.Density;


            if (height <= 1440 && density >= 2.25 )
            {
                tagHeight.HeightRequest = 30;
                overallRowSpacing.RowSpacing = 5;
                frameType.HeightRequest = 34;
                FrameDescription.HeightRequest = 34;
                FrameName.HeightRequest = 34;
                FramePhoneNumber.HeightRequest = 34;
                mainGridPadding.Padding = 20;
                confirmationtext.FontSize = 12;

                FrameName.HeightRequest = 36;
                frameType.HeightRequest = 36;
                FrameDescription.HeightRequest = 36;
                FramePhoneNumber.HeightRequest = 36;

                lblType.FontSize = 12;
                lblDescription.FontSize = 12;
                lblReporterName.FontSize = 12;
                lblPhoneNumber.FontSize = 12;
                
            }
            else if (height < 1560)
            {
                //Set the height of the blue bar
                tagHeight.HeightRequest = 30;
                overallRowSpacing.RowSpacing = 5;
                frameType.HeightRequest = 35;
                FrameDescription.HeightRequest = 35;
                FrameName.HeightRequest = 35;
                FramePhoneNumber.HeightRequest = 35;
            }
            else if (height >= 1560 && density == 0)
            {
                //Set the height of the blue bar
                tagHeight.HeightRequest = 30;
                overallRowSpacing.RowSpacing = 5;
                frameType.HeightRequest = 35;
                FrameDescription.HeightRequest = 35;
                FrameName.HeightRequest = 35;
                FramePhoneNumber.HeightRequest = 35;
            }
            else if (height >= 1560)
            {
               
            }

            MessagingCenter.Subscribe<Object, MediaFile>(this, "finish", (arg, source) =>
            {
               // capturedImage.Source = ImageSource.FromStream(() => source.GetStream());

                _received = source;

                if (_received != null)
                {
                   // ImgText.Text = "Image Successfully Captured";
                   // ImgText.IsVisible = true;
                   // capturedImage.Source = "checkmark";
                   // capturedImage.IsVisible = true;
                   // chkAgree.IsChecked = false;
                }

            });
            lblReportType.Text = reporttype.ToUpper();
            if (reporttype == "Fault")
            {
                var pickerList = new List<string>();
                pickerList.Add("Voltage Fluctuation");
                pickerList.Add("Fallen/Bent Poles");
                pickerList.Add("Transformer Fault");
                pickerList.Add("Phase Failure");

                PickType.ItemsSource = pickerList;

                this.BindingContext = this;
            }
            else if (reporttype == "Outage")
            {
                var pickerList = new List<string>();
                pickerList.Add("Prolonged Outage Data");

                PickType.ItemsSource = pickerList;

                this.BindingContext = this;

            }
            else if (reporttype == "Complain")
            {
                PopupNavigation.Instance.PushAsync(new AccountVerification());

                var pickerList = new List<string>();
                pickerList.Add("Burnt Meter");
                pickerList.Add("Tamper Code");
                pickerList.Add("Stolen Meter");
                pickerList.Add("Payment Issues");
                pickerList.Add("Vending Issues");
                pickerList.Add("Wrong Disconnection");

                PickType.ItemsSource = pickerList;

                this.BindingContext = this;
            }
            else if (reporttype == "Hazard")
            {
                var pickerList = new List<string>();
                pickerList.Add("Vegetation Interference");
                pickerList.Add("Live Cable Snapping");
                PickType.ItemsSource = pickerList;

                this.BindingContext = this;
            }
            else if (reporttype == "Whistle")
            {
                var pickerList = new List<string>();
                pickerList.Add("Extortion");
                pickerList.Add("Bills not Distributed");
                pickerList.Add("Paying to Marketer");
                pickerList.Add("Paying to Illegal 3rd Party");
                pickerList.Add("Double Feeding");
                pickerList.Add("Illegal Disconnection");
                pickerList.Add("Meter Byepass");
                pickerList.Add("Meter Tampering");
                PickType.ItemsSource = pickerList;

                this.BindingContext = this;
            }
            //Task.Run(ObtainDeviceLocation);
            
            
        }

        public async void ObtainDeviceLocation()
        {
            //var policy = Policy
            //        .Handle<HttpRequestException>()
            //        .RetryAsync
            //        (3);

            //await policy.ExecuteAsync(async () =>
            //{
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
                            lblLocation.Text = lat.ToString();
                            lblLocation.Text = "ENUGU";
                            locationLoader.IsRunning = false;
                            locationLoader.IsEnabled = false;
                            locationLoader.IsVisible = false;
                        //verifyUserLocation();
                    }
                    }
                }
                catch (FeatureNotSupportedException featureNotSupportedException)
                {

                }
                catch (FeatureNotEnabledException featureNotEnabledException)
                {
                       //> I would force to enable device's gps in this line
                      await PopupNavigation.Instance.PushAsync(new EnableGPS());
                       
                }
                catch (PermissionException permissionException)
                {
                    AppInfo.ShowSettingsUI();
                }
                catch (Exception exception)
                {

                }

            //});
        }

        private void TakePhoto(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PhotoOptions());
        }

        //Check user location & ensure only users within EEDC coverage area can use the app (Enugu/Abia/Anambra/Imo/Ebonyi)
        public async void verifyUserLocation()
        {
            try
            {
                //Edo State (6.347571, 5.599892)
                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {

                    if (placemark.AdminArea == "Enugu" || placemark.AdminArea == "Abia" || placemark.AdminArea == "Imo" || placemark.AdminArea == "Ebonyi" || placemark.AdminArea == "Anambra")
                    {
                        lblLocation.Text = placemark.AdminArea.ToString().ToUpper();
                        locationLoader.IsRunning = false;
                        locationLoader.IsEnabled = false;
                        locationLoader.IsVisible = false;
                    }
                    else
                    {
                        //Out of location 
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {

                // Ask for a refresh to get the user location
                ObtainDeviceLocation();
                // Handle exception that may have occurred in geocoding
                await DisplayAlert("Info", ex.Message.ToString(), "Close");


            }
        }

        public void ClearFieldsData()
        {
            //Clear or Reset All Fields to Default State
            entPhoneNumber.Text = "";
            entDescription.Text = "";
            PickType.SelectedItem = PickType.Title;
            entReporterName.Text = "";
        }

        private void backButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void backTapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public async void Submit_Data(object sender, EventArgs e)
        {
            
            // Check Connectivity

            var conect = Connectivity.NetworkAccess;
            if (conect == NetworkAccess.Internet)
            {
                //Validate the from 
                if (entDescription.Text == null)
                {
                   // errorDescription.IsVisible = true;
                }
                else
                {
                   // errorType.IsVisible = false;
                   // errorDescription.IsVisible = false;

                    forname = Guid.NewGuid().ToString();

                    if (_received == null)
                    {
                        await DisplayAlert("Error!", "There is an Error with the Image, Please Capture", "Close");
                    }
                    else
                    {

                        // UploadImage(_received.GetStream());
                        if (chkAgree.IsChecked == false)
                        {
                            await DisplayAlert("Submission Error", "Please Argree to the Information Provided by checking the box above", "Close");
                        }
                        else
                        {

                            //Create Variables to Capture
                            string received_Lat = lat.ToString();
                            string received_Lng = lon.ToString();
                            string received_Date = DateTime.Now.ToString();

                            reporter ic = new reporter()
                            {
                                ReporterFullname = entReporterName.Text.ToString(),
                                ReporterPhoneNo = entPhoneNumber.Text.ToString(),
                                Category = lblReportType.Text.ToString(),
                                Description = entDescription.Text.ToString(),
                                CategorySubType = PickType.SelectedItem.ToString(),
                                LongX = received_Lng,
                                LatY = received_Lat,
                                CapturedImage = "https://eedcstorage.blob.core.windows.net/incidentphotos/" + forname + ".png",
                                CapturedIssueDate = received_Date
                            };

                            await PopupNavigation.Instance.PushAsync(new loading());

                            // Serialize the Model object and Post the data
                            var jsonObject = JsonConvert.SerializeObject(ic);

                            var context = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                            try
                            {
                                HttpClient hc = new HttpClient();

                                var result = await hc.PostAsync("http://denceapp.somee.com/api/Incidence/reportIncident/", context);

                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    //Get the Track ID for the user to show on successful submission panel
                                    List<incidenceApp> trackData = new List<incidenceApp>();
                                    using (var client = new HttpClient())
                                    {
                                        client.BaseAddress = new Uri("http://denceapp.somee.com/api/incidence/getTrackID/");
                                        var responseTask = client.GetAsync(forname);

                                        responseTask.Wait();

                                        var res = responseTask.Result;
                                        if (res.IsSuccessStatusCode)
                                        {
                                            string readTask = await res.Content.ReadAsStringAsync();
                                            trackData = JsonConvert.DeserializeObject<List<incidenceApp>>(readTask);

                                            foreach (var dataReterived in trackData)
                                            {
                                                userTrackID = dataReterived.incidenceId.ToString().ToUpper();
                                            }
                                        }
                                    }

                                    ClearFieldsData();
                                    await PopupNavigation.Instance.PopAsync();
                                    PickType.SelectedIndex = 0;
                                    MessagingCenter.Send<Object, String>(this, "Tracker", userTrackID);
                                    await PopupNavigation.Instance.PushAsync(new submitSuccess(userTrackID));
                                    _received = null;
                                }

                                else
                                {
                                    //Internal Server Error
                                    await DisplayAlert("Server Error!", "There was an Error Conecting to the server, Please Ensure you are connected to the internet and try again!", "Close");
                                    await PopupNavigation.Instance.PushAsync(new network());

                                }
                            }
                            catch (Exception ex)
                            {
                                await DisplayAlert("Hey!!", ex.Message, "Close");
                            }

                        }
                    }
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new network());
            }
        }
    }
}