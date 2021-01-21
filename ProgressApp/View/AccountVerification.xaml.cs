using Plugin.Connectivity;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountVerification : PopupPage
    {
        public AccountVerification()
        {
            InitializeComponent();
        }

        async void btnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await PopupNavigation.Instance.PopAsync();


        }

        public async void btnVerifyClicked(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                await PopupNavigation.Instance.PushAsync(new loading());

                try
                {
                    using (var client = new HttpClient())
                    {

                        string no = txtNumber.Text.ToString();
                        client.BaseAddress = new Uri("http://denceapp.somee.com/api/incidence/validateMeterNo/");
                        var responseTask = client.GetAsync(no);

                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {

                            lblStatus.IsVisible = true;
                            lblStatus.Text = "Verification Not Successful!";
                            lblStatus.TextColor = System.Drawing.Color.Red;
                            // Stop the activity indicator
                            await PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            lblStatus.Text = "Verified Successfully!";
                            await PopupNavigation.Instance.PopAsync();
                            await PopupNavigation.Instance.PopAsync();
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
            else
            {
                await PopupNavigation.Instance.PushAsync(new no_internet());
                await Navigation.PopAsync();
                // await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}