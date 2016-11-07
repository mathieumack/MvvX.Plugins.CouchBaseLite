using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Managers
{
    /// <summary>
    /// An interface describing a class that can serialize .NET objects 
    /// to and from their JSON representation
    /// </summary>
    public interface IJsonSerializer : IDisposable
    {
        /// <summary>
        /// Gets or sets the settings to apply to the serializer
        /// </summary>
        //IJsonSerializationSettings Settings { get; set; }

        /// <summary>
        /// Gets the current token when parsing in streaming mode
        /// </summary>
        JsonToken CurrentToken { get; }

        /// <summary>
        /// Convert an object to a JSON string
        /// </summary>
        /// <returns>The JSON string</returns>
        /// <param name="obj">The object to convert</param>
        /// <param name="pretty">Whether or not to use pretty printing</param>
        string SerializeObject(object obj, bool pretty);

        /// <summary>
        /// Converts a JSON string to a typed object
        /// </summary>
        /// <returns>The object</returns>
        /// <param name="json">The json string to parse</param>
        /// <typeparam name="T">The type of object to return</typeparam>
        T DeserializeObject<T>(string json);

        /// <summary>
        /// Reads a stream and converts the contained data to a typed object
        /// </summary>
        /// <param name="json">The stream to read from</param>
        /// <returns>The parsed object</returns>
        /// <typeparam name="T">The type of object to return</typeparam>
        T Deserialize<T>(Stream json);

        /// <summary>
        /// Starts parsing a stream of JSON incrementally, rather than serializing
        /// the entire object into memory
        /// </summary>
        /// <param name="json">The stream containing JSON data</param>
        void StartIncrementalParse(Stream json);

        /// <summary>
        /// Reads the next token from a JSON stream.  Note that an incremental parse
        /// must be started first.
        /// </summary>
        /// <returns>True if another token was read, false if an incremental parse is not started
        /// or no more tokens are left</returns>
        bool Read();

        /// <summary>
        /// A convenience function for deserializing the next object in a stream into
        /// a .NET object
        /// </summary>
        /// <returns>The deserialized object, or null if unable to deserialize</returns>
        T DeserializeNextObject<T>();

        /// <summary>
        /// Converts the object from its intermediary JSON dictionary class to a .NET dictionary,
        /// if applicable.
        /// </summary>
        /// <returns>The .NET dictionary, or null if the object cannot be converted</returns>
        /// <param name="obj">The object to try to convert</param>
        /// <typeparam name="K">The key type of the dictionary</typeparam>
        /// <typeparam name="V">The value type of the dictionary</typeparam>
        IDictionary<K, V> ConvertToDictionary<K, V>(object obj);

        /// <summary>
        /// Converts the object from its intermediary JSON array class to a .NET list,
        /// if applicable.
        /// </summary>
        /// <returns>The .NET list, or null if the object cannot be converted</returns>
        /// <param name="obj">The object to try to convert</param>
        /// <typeparam name="T">The type of object in the list</typeparam>
        IList<T> ConvertToList<T>(object obj);

        /// <summary>
        /// Makes a deep copy of the serializer in order to start an incremental parse
        /// that is disposable.
        /// </summary>
        /// <returns>The cloned object</returns>
        IJsonSerializer DeepClone();

    }
}
