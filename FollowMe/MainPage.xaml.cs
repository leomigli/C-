using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Geolocation;
using Windows.Web.Http;
using Windows.UI.Popups;
using System.Globalization;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/p/?LinkID=234238

namespace FollowMe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Geolocator geo = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        //private void TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        //{
        //    if (e.Key == Windows.System.VirtualKey.Enter)
        //    {
        //        Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
        //    }
        //}
        //TODO vedere come richiamare gli snippet tipo quello qui sotto il quale è accedibile tramite
        //Strumenti->Gestione frammenti di codice->Altro- ...-> Connettività di rete->Converte un URI relativo in un URI assoluto
        //provato ad aprire gli cìscript presenti all'indirizzo C:\Program Files (x86)\Microsoft Visual Studio 14.0\Vb\Snippets\1040\other\connectivity .... si apre un mondo da esplorare
        //C:\Program Files (x86)\Microsoft Visual Studio 14.0\Vb\Snippets\1040\other\connectivity\ConvertaRelativeUritoanAbsoluteUri.snippet


        // nello script relativo C:\Program Files (x86)\Microsoft Visual Studio 14.0\Vb\Snippets\1040\other\connectivity\DetermineiftheNetworkisAvailable
        //ho trovato il seguente pezzettino di dodice
        //Dim isAvailable = My.Computer.Network.IsAvailable
        //proviamo a renderlo c#
        //var isAvailable = System.Net. boooo fda approfondire

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            txtNotes.IsEnabled = false; //nascndo la tastiera su schermo

            geo = new Geolocator();
            bool isErr = false;
            string errMsg = "";

            button1.IsEnabled = false;

            var ap = ApplicationData.Current.LocalSettings;

            //ap.Values["Language"] = null;//la inserisco per test hh

            if (ap.Values["Language"] == null)
            {
                AppBarButton_Click(this, null);
            }
            else
            { 
                try
                {
                    Geoposition pos = await geo.GetGeopositionAsync();
                    double lat = pos.Coordinate.Point.Position.Latitude;
                    double lon = pos.Coordinate.Point.Position.Longitude;
                    //double lat = 45.5117327;coordinate bedizzole
                    //double lon = 10.4086893;
                    lblLatitude.Text = lat.ToString();
                    lblLongitude.Text = lon.ToString();

                    DateTime tt = DateTime.Now.Date;

                    CultureInfo cI = new CultureInfo(ap.Values["Language"].ToString());
                    String devName = new EasClientDeviceInformation().FriendlyName;

                    HttpClient hwc = new HttpClient();
                    Uri myAddress = new Uri(ap.Values["WebURI"].ToString() + 
                        "?lt=" + lat.ToString(cI.NumberFormat) +
                        "&ln=" + lon.ToString(cI.NumberFormat) +
                        "&d=" + devName + 
                        "&n=" + txtNotes.Text
                         + "&tt=" + tt.ToString("yyyy-MM-dd h:mm")
                         );

                    HttpResponseMessage x = await hwc.GetAsync(myAddress);
                }
                catch (Exception ex)
                {
                    isErr = true;
                    errMsg = ex.Message;
                }
            }

            if (isErr)
            {
                var dialog = new MessageDialog(errMsg);
                await dialog.ShowAsync();
            }

            button1.IsEnabled = true;
            geo = null;

            txtNotes.IsEnabled = true;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

    }
}