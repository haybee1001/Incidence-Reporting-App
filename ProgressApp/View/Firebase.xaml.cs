using Firebase.Storage;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;




using static Xamarin.Essentials.Permissions;



namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Firebase : ContentPage
    {
        MediaFile imagebytes;
        public Firebase()
        {
            InitializeComponent();
        }


        //public async void PerformStorage(Guid guid)
        //{
        //    try
        //    {
        //        string username = Guid.NewGuid().ToString();

        //        Stream stream = new MemoryStream(imagebytes);
        //        var task = await new FirebaseStorage("eedcmobileapp.appspot.com")
        //            .Child("incidence")
        //            .Child(username + "")
        //            .PutAsync(stream);
        //        MyImage.Source = null;
        //        await App.Current.MainPage.DisplayAlert("Alert", "Successfully Added", "Ok");

        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
        //    }
        //}
        //string filepath;
        //private async void upload_clicked(object sender, EventArgs e)
        //{
        //    string[] fileTypes = null;

        //    if(Device.RuntimePlatform == Device.Android)
        //    {
        //        fileTypes = new string[] { "Image/Png", "Image/jpeg" };
        //    }
        //}

        //private async Task PickAndShowFile(string[] fileTypes)
        //{
        //    try
        //    {
        //        var pickedFile = await CrossFilePicker.Current.PickFile(fileTypes);

        //        if (pickedFile != null)
        //        {
        //            if (pickedFile.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase)
        //                || pickedFile.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
        //            {
        //                MyImage.Source = ImageSource.FromStream(() => pickedFile.GetStream());

        //                filepath = pickedFile.FilePath;
        //                imagebytes = pickedFile.DataArray;
                        
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


    }
}