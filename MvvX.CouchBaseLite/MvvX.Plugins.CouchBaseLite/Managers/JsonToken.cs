namespace MvvX.Plugins.CouchBaseLite.Managers
{
    /// <summary>
    /// An enum representing the current Token being parsed
    /// in a JSON stream
    /// </summary>
    public enum JsonToken
    {
        /// <summary>
        /// No token
        /// </summary>
        None,
        /// <summary>
        /// Start of an object ("{")
        /// </summary>
        StartObject,
        /// <summary>
        /// Start of an array ("[")
        /// </summary>
        StartArray,
        /// <summary>
        /// Start of a JSON constructor
        /// </summary>
        StartConstructor,
        /// <summary>
        /// An object property name
        /// </summary>
        PropertyName,
        /// <summary>
        /// A comment
        /// </summary>
        Comment,
        /// <summary>
        /// Raw JSON
        /// </summary>
        Raw,
        /// <summary>
        /// An integer
        /// </summary>
        Integer,
        /// <summary>
        /// A float
        /// </summary>
        Float,
        /// <summary>
        /// A string
        /// </summary>
        String,
        /// <summary>
        /// A boolean
        /// </summary>
        Boolean,
        /// <summary>
        /// A null token
        /// </summary>
        Null,
        /// <summary>
        /// An undefined token
        /// </summary>
        Undefined,
        /// <summary>
        /// End of an object ("}")
        /// </summary>
        EndObject,
        /// <summary>
        /// End of an array ("]")
        /// </summary>
        EndArray,
        /// <summary>
        /// A constructor end token.
        /// </summary>
        EndConstructor,
        /// <summary>
        /// A date
        /// </summary>
        Date,
        /// <summary>
        /// Byte data
        /// </summary>
        Bytes
    }
}
