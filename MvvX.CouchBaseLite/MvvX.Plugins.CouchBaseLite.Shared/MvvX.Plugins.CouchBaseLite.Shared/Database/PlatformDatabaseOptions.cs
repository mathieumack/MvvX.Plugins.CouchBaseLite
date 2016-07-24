using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Store;
using MvvX.Plugins.CouchBaseLite.Storages;

namespace MvvX.Plugins.CouchBaseLite.Shared.Database
{
    public class PlatformDatabaseOptions : IDatabaseOptions
    {
        public bool Create { get; set; }

        public byte[] KeyData { get; internal set; }

        public string Password { get; internal set; }

        public bool ReadOnly { get; set; }

        public int Rounds { get; internal set; }

        public byte[] Salt { get; internal set; }

        public StorageTypes StorageType { get; set; }

        public SymmetricKeyType SymmetricKeyType { get; internal set; }

        public void SetSymmetricKey(byte[] keyData)
        {
            KeyData = keyData;
            SymmetricKeyType = SymmetricKeyType.KeyData;
        }

        public void SetSymmetricKey(string password)
        {
            Password = password;
            SymmetricKeyType = SymmetricKeyType.Password;
        }

        public void SetSymmetricKey(string password, byte[] salt, int rounds)
        {
            Password = password;
            Salt = salt;
            Rounds = rounds;
            SymmetricKeyType = SymmetricKeyType.PasswordWithSalt;
        }

        public PlatformDatabaseOptions()
        {
            this.StorageType = StorageTypes.Sqlite;
        }
    }
}
