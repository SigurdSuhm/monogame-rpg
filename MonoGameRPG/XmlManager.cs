#region Using Statements

using System;
using System.IO;
using System.Xml.Serialization;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Manager class for saving and loading game data through Xml serialization.
    /// </summary>
    public class XmlManager<T>
    {
        // Type of the object to be serialized
        public Type Type;

        /// <summary>
        /// Loads data from an Xml file and returns an object with the data.
        /// </summary>
        /// <param name="path">Path to the Xml file.</param>
        /// <returns>Object of the type T to be loaded.</returns>
        public T Load(string path)
        {
            // Instance to return
            T instance;

            // Create stream reader object
            using (TextReader reader = new StreamReader(path))
            {
                // Read data from the Xml file
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }

            return instance;
        }

        /// <summary>
        /// Saves data from an object to an Xml file.
        /// </summary>
        /// <param name="path">Path to the Xml file.</param>
        /// <param name="obj">Object of the type T to be saved.</param>
        public void Save(string path, object obj)
        {
            // Create stream write object
            using (TextWriter writer = new StreamWriter(path))
            {
                // Save data to Xml
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
