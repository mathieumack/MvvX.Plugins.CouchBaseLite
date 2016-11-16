[![Build status](https://ci.appveyor.com/api/projects/status/uys4bl6qcqauram7/branch/master?svg=true)](https://ci.appveyor.com/project/mathieumack/mvvx-plugins-couchbaselite/branch/master)

# MvvX.CouchBaseLite

Using the CouchBaseLite-Plugin for MvvmCross is quite simple. The plugin injects the ICouchBaseLite interface into the IoC container.

## Nuget Installation

Install [MvvX.CouchBaseLite](https://www.nuget.org/packages/MvvX.Plugins.CouchBaseLite/) from nuget.

**Important:** You will need to add the nuget package to **both** your *PCL project* and your *platform-dependent project*.

## Please Contribute!

This is an open source project that welcomes contributions/suggestions/bug reports from those who use it. 

If you have any ideas on how to improve the library, please [post an issue here on github](https://github.com/mathieumack/MvvX.Plugins.CouchBaseLite/issues). Please check out the [How to Contribute](https://github.com/mathieumack/MvvX.Plugins.CouchBaseLite/wiki/How-to-Contribute).

# Example time

## API

The API of ICouchBaseLite is very easy to understand and to use.

```c#
public interface ICouchBaseLite : IDisposable
{
	IDatabase Database { get; }
	bool CreateConnection(string workingFolderPath, string databaseName);
}
```
## CouchBaseLite Connection

Using the name of the database and the folder on the client device where to store database files:
```c#

            ICouchBaseLite service = Mvx.Resolve<ICouchBaseLite>();

            var databaseName = "MvvXPluginTest";
            var databaseFolderPath = Path.Combine(Path.GetTempPath(), "testCouchDb");

            // Connect to the database
            var successConnection = service.CreateConnection(databaseFolderPath, databaseName);
```


To be complete... 