//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Rg.Plugins.Popup.Pages;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//using Xamarin.Essentials;

//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;
//using System.Web;
//using System.Net;
//using Plugin.Media;

//using System.IO;
//using Plugin.Media.Abstractions;
//using System.Text.RegularExpressions;

//using Rg.Plugins.Popup.Services;

//namespace ProgressApp.View
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class PhotoOptions : PopupPage
//    {
//        public MediaFile _mediaFile;
//        private string URL { get; set; }

//        static string imageURL;

//        private bool animate;

//        public PhotoOptions()
//        {
//            InitializeComponent();

//            takeLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => TakePhotoEvent()));
//            ChooseLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => ChoosePhotoEvent()));
//            cancelLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => CancelEvent()));

//            cancelLabel.IsEnabled = false;
//            cancelLabel.Opacity = 0.2;
//        }

//        public async void TakePhotoEvent()
//        {
//            //popupPhotoOptions.IsVisible = false;
//            await CrossMedia.Current.Initialize();
//            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
//            {
//                await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
//                return;
//            }
//            else
//            {
//                await PopupNavigation.Instance.PushAsync(new loading());
//                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
//                {
//                    Directory = "Sample",
//                    Name = "myImage.jpg"
//                });

//                if (_mediaFile == null) return;
//                previewImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
//                previewImage.IsVisible = true;
//                cancelLabel.IsEnabled = true;

//                cancelLabel.Opacity = 0.7;
//                var mediaOption = new PickMediaOptions()
//                {
//                    PhotoSize = PhotoSize.Medium,
//                    CompressionQuality = 10,
//                    CustomPhotoSize = 30

//                };
//                await PopupNavigation.Instance.PopAsync();

//            }
//        }

//        //Method Block to Choose an (single) image from the device file storage, Compress and upload to an Image Control
//        public async void ChoosePhotoEvent()
//        {
//            //popupPhotoOptions.IsVisible = false;

//            await CrossMedia.Current.Initialize();
//            if (!CrossMedia.Current.IsPickPhotoSupported)
//            {
//                await DisplayAlert("Error", "This is not support on your device.", "OK");
//                return;
//            }
//            else
//            {
//                var mediaOption = new PickMediaOptions()
//                {
//                    PhotoSize = PhotoSize.Medium,
//                    CompressionQuality = 10,
//                    CustomPhotoSize = 30
//                };

//                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
//                if (_mediaFile == null) return;
//                previewImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
//                previewImage.IsVisible = true;

//                cancelLabel.IsEnabled = true;
//                cancelLabel.Opacity = 0.7;
//            }
//        }

//        //Method block to close the pop-up view
//        public async void CancelEvent()
//        {
//            var source = ImageSource.FromStream(() =>
//            {
//                var stream = _mediaFile.GetStream();
//                return stream;
//            });

//            MessagingCenter.Send<Object, MediaFile>(this, "finish", _mediaFile);

//            await PopupNavigation.Instance.PopAsync(animate = true);
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Web;
using System.Net;
using Plugin.Media;

using System.IO;
using Plugin.Media.Abstractions;
using System.Text.RegularExpressions;

using Rg.Plugins.Popup.Services;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoOptions : PopupPage
    {
        public MediaFile _mediaFile;
        private string URL { get; set; }

        static string imageURL;

        private bool animate;

        public PhotoOptions()
        {
            InitializeComponent();

            takeLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => TakePhotoEvent()));
            ChooseLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => ChoosePhotoEvent()));
            cancelLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => CancelEvent()));

            cancelLabel.IsEnabled = false;
            cancelLabel.Opacity = 0.2;
        }

        public async void TakePhotoEvent()
        {
            //popupPhotoOptions.IsVisible = false;
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
                return;
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new loading());
                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "myImage.jpg"
                });

                if (_mediaFile == null) return;
                previewImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                previewImage.IsVisible = true;
                cancelLabel.IsEnabled = true;

                cancelLabel.Opacity = 0.7;
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 10,
                    CustomPhotoSize = 30

                };
                await PopupNavigation.Instance.PopAsync();

            }
        }

        //Method Block to Choose an (single) image from the device file storage, Compress and upload to an Image Control
        public async void ChoosePhotoEvent()
        {
            //popupPhotoOptions.IsVisible = false;

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 10,
                    CustomPhotoSize = 30
                };

                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;
                previewImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                previewImage.IsVisible = true;

                cancelLabel.IsEnabled = true;
                cancelLabel.Opacity = 0.7;
            }
        }

        //Method block to close the pop-up view
        public async void CancelEvent()
        {
            var source = ImageSource.FromStream(() =>
            {
                var stream = _mediaFile.GetStream();
                return stream;
            });

            MessagingCenter.Send<Object, MediaFile>(this, "finish", _mediaFile);

            PopupNavigation.Instance.PopAsync(animate = true);
        }
    }
}