using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace GetDataTest
{
    [Activity(Label = "GetDataTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static int MYREQUEST = 555;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            Button btn_StartMRU4u = FindViewById<Button>(Resource.Id.MRU_Button);
            btn_StartMRU4u.Click += (sender, args) =>
            {
                string xmlSendData =
                    "42<?xml version=\"1.0\" encoding=\"utf-8\"?><SingleStart xmlns=\"urn:ZIV-schema\" TransmissionId=\"2016-10-13\"> <Version>1.00</Version> <FuelId>FUEL_NAT_GAS</FuelId> <RequiredMeasurements>REQ_MEAS_BIMSCHV</RequiredMeasurements> </SingleStart>";
                string urlString = "mru4urequest://mru/" + System.Net.WebUtility.UrlEncode(xmlSendData);
                Intent intent = new Intent(Intent.ActionDefault, Uri.Parse(urlString));


                //TODO habe beide probiert
                //StartActivity(intent);
                StartActivityForResult(intent, MYREQUEST);
            };
        }


        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            if (intent.Extras != null)
            {
                foreach (string key in intent.Extras.KeySet())
                {
                    var value = intent.Extras.Get(key);
                    if (value != null)
                    {
                        Log.Debug("EXTRAS", "Key: " + key + " Value: " + value.ToString() + " Type: " + value.GetType().Name);
                    }
                }
            }
        }

        
        protected override void OnActivityResult(int requestCode, Result result, Intent data)
        {
            Log.Debug("MESSUNGDETAILS", "Received Result! ");
            if (requestCode == MYREQUEST)
            {
                Log.Debug("MESSUNGDETAILS", "Result is for me! Result = " + result);
                if (result == Result.Ok)
                {
                    if (data != null && data.Extras != null)
                    {
                        foreach (string key in data.Extras.KeySet())
                        {
                            var value = data.Extras.Get(key);
                            if (value != null)
                            {
                                Log.Debug("EXTRAS",
                                    "Key: " + key + " Value: " + value.ToString() + " Type: " + value.GetType().Name);
                            }
                        }
                    }
                }
            }
        }
    }
}

