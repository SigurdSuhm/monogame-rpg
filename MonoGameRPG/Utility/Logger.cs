#region Using Statements

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace MonoGameRPG.Utility
{
    /// <summary>
    /// Logs events which can then be written to a text file.
    /// </summary>
    public class Logger
    {
        #region Fields

        // List of log entries
        private List<LogEntry> logEntries;
        // Name of the file to write to
        private string fileName;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Logger(string fileName = "log.txt")
        {
            this.fileName = fileName;
            logEntries = new List<LogEntry>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Posts a log entry to the logger.
        /// </summary>
        /// <param name="entry">Entry to be logged.</param>
        public void PostEntry(LogEntry entry)
        {
            logEntries.Add(entry);
        }

        /// <summary>
        /// Creates a new log entry and posts it to the logger.
        /// </summary>
        /// <param name="type">Type of the entry.</param>
        /// <param name="message">Message of the entry.</param>
        public void PostEntry(LogEntryType type, string message)
        {
            logEntries.Add(new LogEntry(type, message));
        }

        /// <summary>
        /// Gets all entries of a certain type.
        /// </summary>
        /// <param name="type">Type of log entry to get.</param>
        /// <returns>Array of log entries of the given type.</returns>
        public LogEntry[] GetEntriesByType(LogEntryType type)
        {
            return logEntries.Where(e => e.Type == type).ToArray();
        }

        /// <summary>
        /// Writes all log entries to a text file.
        /// </summary>
        public void FlushToFile()
        {
            // Sort list by time stamp
            logEntries.Sort((e1, e2) => e2.TimeStamp.CompareTo(e1.TimeStamp));

            using (StreamWriter writer = new StreamWriter(File.Create(fileName)))
            {
                // Log file header
                writer.WriteLine(String.Format("-- Log file created on {0:HH:mm:ss} --", DateTime.Now));

                for (int i = 0; i < logEntries.Count; i++)
                {
                    LogEntry currentEntry = logEntries[i];
                    // Create formatted entry string as "[HH:mm:ss] Type: Message"
                    string outputString = String.Format("[{0:HH:mm:ss}] {1}: {2}", currentEntry.TimeStamp, 
                        currentEntry.Type.ToString(), currentEntry.Message);

                    writer.WriteLine(outputString);
                }
            }
        }

        #endregion
    }
}