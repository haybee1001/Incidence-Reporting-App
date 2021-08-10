using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Storage;
using System.Threading.Tasks;
using System.IO;

namespace ProgressApp.Database
{
    public class DFFire
    {
        async public Task saveImage(Stream imgStream, string ImageName)
        {

            var storageImage = await new FirebaseStorage("eedcmobileapp.appspot.com")
                .Child("incidence")
                .Child(ImageName)
                .PutAsync(imgStream);

            var imgUrl = storageImage;


        }

    }

}
 