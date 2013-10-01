#region Using Statements

using System;

#endregion

namespace MonoGameRPG.Utility
{
    /// <summary>
    /// Entries used in the logger class.
    /// </summary>
    public class LogEntry
    {
        #region Fields

        // Type of the log entry
        private LogEntryType type;
        // Message of the log entry
        private string message;
        // Time stamp for the log entry
        private DateTime timeStamp;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the log entry.
        /// </summary>
        public LogEntryType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the message of the log entry.
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Gets the time stamp for when the entry was created.
        /// </summary>
        public DateTime TimeStamp
        {
            get { return timeStamp; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="type">Type of the log entry.</param>
        /// <param name="message">Message of the log entry.</param>
        public LogEntry(LogEntryType type, string message)
        {
            this.type = type;
            this.message = message;

            timeStamp = DateTime.Now;
        }

        #endregion
    }
}