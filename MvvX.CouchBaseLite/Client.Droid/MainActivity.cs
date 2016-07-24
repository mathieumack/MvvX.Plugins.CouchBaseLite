using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvX.Plugins.CouchBaseLite;
using MvvX.Plugins.CouchBaseLite.Documents;
using MvvmCross.Platform;

namespace Client.Droid
{
    [Activity(Label = "Client.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {
                Mvx.Trace("Start connection.");

                //ICouchBaseLite service = new MvvX.Plugins.CouchBaseLite.Platform.CouchBaseLite();
                //if (service.CreateConnection(@"C:\temp\testCouchDb", "MvvXPluginTest", null))
                //{
                //    // On rajoute des documents :
                //    var document = service.Database.CreateDocument();
                //    if (document != null)
                //    {
                //        document.Update((IUnsavedRevision newRevision) =>
                //        {
                //            var properties = newRevision.Properties;

                //            //properties["title"] = title;
                //            //properties["notes"] = notes;

                //            return true;
                //        });

                //        var maintenanceProcedure = new Data.MaintenanceProcedure();
                //        Mvx.Trace("New document created : " + document.Id);
                //    }
                //    else
                //        Mvx.Trace("Error, document is null.");
                //}
                Mvx.Trace("End.");
            };
        }
    }
}

