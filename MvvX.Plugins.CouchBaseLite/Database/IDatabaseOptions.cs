using MvvX.Plugins.CouchBaseLite.Storages;
using MvvX.Plugins.CouchBaseLite.Store;

namespace MvvX.Plugins.CouchBaseLite.Database
{
    public interface IDatabaseOptions
    {
        bool Create { get; set; }
        
        bool ReadOnly { get; set; }

        /// <summary>
        /// Storage type (SQlite by default)
        /// </summary>
        StorageTypes StorageType { get; set; }

        SymmetricKeyType SymmetricKeyType { get; }

        string Password { get; }

        void SetSymmetricKey(string password);
        
        byte[] KeyData { get; }

        void SetSymmetricKey(byte[] keyData);

        byte[] Salt { get; }

        int Rounds { get; }

        void SetSymmetricKey(string password, byte[] salt, int rounds);
    }
}
