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
}
