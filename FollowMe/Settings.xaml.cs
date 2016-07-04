using FollowMe.Common;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FollowMe
{

    public sealed partial class Settings : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public Settings()
        {
            this.InitializeComponent();

            if (cmbLang.SelectedValue == null) cmbLang.SelectedIndex = 1;//seleziono inglese di default

            this.navigationHelper = new NavigationHelper(this);

            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }


         #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var ap = ApplicationData.Current.LocalSettings;

            txtWebUri.Text = txtWebUri.Text == "http://localhost" ? "http://localhost/MyTest/PHP_webserver/" : txtWebUri.Text;
            txtWebUri.Text = txtWebUri.Text == "" ? "http://localhost/MyTest/PHP_webserver/" : txtWebUri.Text;
            ap.Values["WebURI"] = txtWebUri.Text;
            
            ap.Values["Language"] = cmbLang.SelectedValue;
            
            //Frame.GoBack();
            //NavigationHelper.GoBack();

            Frame.Navigate(typeof(MainPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var ap = ApplicationData.Current.LocalSettings;
            try
            {
                txtWebUri.Text = ap.Values["WebURI"].ToString();
                cmbLang.SelectedValue = ap.Values["Language"].ToString();
            }
            catch {
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
