using System;
using System.Collections.Generic;
using System.Windows;
using MvvX.Plugins.CouchBaseLite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Documents;
using MvvX.Plugins.CouchBaseLite.Platform;
using Newtonsoft.Json;

namespace Client.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICouchBaseLite couchBaseLite;

        private string username;

        private string password;

        private Uri url;

        private IDatabase database;

        public MainWindow()
        {
            InitializeComponent();

            InitializeCouchbase();
        }

        /// <summary>
        /// Initialize the couchbase database
        /// </summary>
        private void InitializeCouchbase()
        {
            couchBaseLite = new CouchBaseLite();
            
            couchBaseLite.Initialize("C:/temp/cbl");

            var databaseOptions = couchBaseLite.CreateDatabaseOptions();

            databaseOptions.Create = true;
            databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;

            database = couchBaseLite.CreateConnection("beer", databaseOptions);
        }

        /// <summary>
        /// Initialize connection to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectToCouchbaseSync(object sender, RoutedEventArgs e)
        {
            InitializeCouchbase();
            ConnectButton.IsEnabled = false;
            ContentTextBlock.Text = "Connected to local couchbase instance";
            DataTabItem.IsEnabled = true;
            ReloadDataTab();
        }

        private void PushReplication(object sender, RoutedEventArgs e)
        {
            url = new Uri(UrlTextBox.Text);
            username = LoginTextBox.Text;
            password = PasswordTextBox.Text;

            var docId = PushId.Text;
            var docName = PushNameValue.Text;

            var docToUpdate = database.GetDocument(docId);

            docToUpdate.Update((IUnsavedRevision newRevision) =>
            {
                var properties = newRevision.Properties;
                properties["name"] = docName;
                return true;
            });

            var push = database.CreatePushReplication(url);
            push.SetBasicAuthenticator(username, password);
            push.Continuous = true;
            push.Changed += Push_Changed;

            push.Start();
        }

        private void PullReplication(object sender, RoutedEventArgs e)
        {
            url = new Uri(UrlTextBox.Text);
            username = LoginTextBox.Text;
            password = PasswordTextBox.Text;

            var pull = database.CreatePullReplication(url);
            pull.SetBasicAuthenticator(username, password);
            pull.Continuous = true;
            pull.Changed += Pull_Changed;

            pull.Start();
        }

        /// <summary>
        /// Triggered when a push replication causes changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Push_Changed(object sender, MvvX.Plugins.CouchBaseLite.Sync.IReplicationChangeEventArgs e)
        {
            ContentTextBlock.Text = string.Concat("PUSH : ", JsonConvert.SerializeObject(e, Formatting.Indented), "\n");
            if (e.Status == ReplicationStatus.Idle)
                ReloadDataTab();
        }

        /// <summary>
        /// Triggered when a pull replication causes changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pull_Changed(object sender, MvvX.Plugins.CouchBaseLite.Sync.IReplicationChangeEventArgs e)
        {
            ContentTextBlock.Text = string.Concat("PULL : ", JsonConvert.SerializeObject(e, Formatting.Indented), "\n");
            if (e.Status == ReplicationStatus.Idle)
                ReloadDataTab();
        }

        private void ReloadDataTab()
        {
            var allDocQuery = database.CreateAllDocumentsQuery();
            var queryEnum = allDocQuery.Run();

            IList<Beer> beers = new List<Beer>();

            foreach(var queryRow in queryEnum)
            {
                var jsonDict = queryRow.AsJSONDictionary();
                var beer = new Beer();
                beer.address = queryRow.Document.CurrentRevision.GetProperty("address") as string[];
                beer.city = queryRow.Document.CurrentRevision.GetProperty("city") as string;
                beer.code = queryRow.Document.CurrentRevision.GetProperty("code") as string;
                beer.country = queryRow.Document.CurrentRevision.GetProperty("country") as string;
                beer.description = queryRow.Document.CurrentRevision.GetProperty("description") as string;
                beer.geo = queryRow.Document.CurrentRevision.GetProperty("geo") as Geolocalisation;
                beer.name = queryRow.Document.CurrentRevision.GetProperty("name") as string;
                beer.phone = queryRow.Document.CurrentRevision.GetProperty("phone") as string;
                beer.state = queryRow.Document.CurrentRevision.GetProperty("state") as string;
                beer.type = queryRow.Document.CurrentRevision.GetProperty("type") as string;
                DateTime.TryParse(queryRow.Document.CurrentRevision.GetProperty("updated") as string, out DateTime beerDate);
                beer.updated = beerDate;
                beer.website = queryRow.Document.CurrentRevision.GetProperty("website") as string;

                beers.Add(beer);
            }

            DataListBox.ItemsSource = beers;
        }
    }
}
