using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProgressApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();

           // abtLabel.Text = reportType.ToString();
        }

        public About( string reportType)
        {
            InitializeComponent();

             //abtLabel.Text = reportType.ToString();
        }

        private void backClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void backTapped(object sender, EventArgs e)
        {
          //  Navigation.PopAsync();
        }

        private void backButtonClicked(object sender, EventArgs e)
        {

        }
    }
}