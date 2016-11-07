using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Managers
{
    /// <summary>
    /// Specifies methods for deserializing date times that are written as strings
    /// </summary>
    public enum DateTimeHandling
    {
        /// <summary>
        /// Deserialize to System.DateTime (local time zone)
        /// </summary>
        UseDateTime,

        /// <summary>
        /// Deserialize to System.DateTimeOffset (embedded time zone)
        /// </summary>
        UseDateTimeOffset,

        /// <summary>
        /// Don't deserialize (keep as a date-time string).
        /// </summary>
        /// <remarks>
        /// WARNING: This will cause any DateTime or DateTimeOffset values to be
        /// returned as strings.  Make sure you know what you are doing
        /// </remarks>
        Ignore
    }

    /// <summary>
    /// A struct containing options for JSON serialization and deserialization
    /// </summary>
    public struct JsonSerializationSettings
    {

        /// <summary>
        /// Gets or sets how the serializer will handle deserializing date times.
        /// </summary>
        public DateTimeHandling DateTimeHandling { get; set; }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(JsonSerializationSettings x, JsonSerializationSettings y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(JsonSerializationSettings x, JsonSerializationSettings y)
        {
            return !x.Equals(y);
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            if (!(obj is JsonSerializationSettings))
            {
                return false;
            }

            return DateTimeHandling == ((JsonSerializationSettings)obj).DateTimeHandling;
        }

        public override int GetHashCode()
        {
            return DateTimeHandling.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("JsonSerializationSettings[DateTimeHandling={0}]", DateTimeHandling);
        }

        #endregion
    }
}
