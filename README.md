## MvvX.CouchBaseLite

Using the CouchBaseLite-Plugin for MvvmCross is quite simple. The plugin injects the ICouchBaseLite interface into the IoC container.

### API

The API of ICouchBaseLite is very easy to understand and to use.

```c#
public interface ICouchBaseLite : IDisposable
{
	IDatabase Database { get; }
	bool CreateConnection(string workingFolderPath, string databaseName);
}
```
#### CouchBaseLite Connection

Using the name of the database and the folder on the client device where to store database files:
```c#

            ICouchBaseLite service = Mvx.Resolve<ICouchBaseLite>();

            var databaseName = "MvvXPluginTest";
            var databaseFolderPath = Path.Combine(Path.GetTempPath(), "testCouchDb");

            // Connect to the database
            var successConnection = service.CreateConnection(databaseFolderPath, databaseName);
```

To be complete...