using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DefaultCompany.Utils {
    /// <summary>
    /// Save/Load services for binary files.
    /// </summary>
    public static class DataService {

        /// <summary>
        /// Saves/Overwrites data on the requested file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        public static void Save(string fileName, object data)
        {
            //"/save.dat"
            string destination = Path.Combine(Application.persistentDataPath, fileName);
            FileStream file;

            if (File.Exists(destination)) {
                file = File.OpenWrite(destination);
            } else {
                file = File.Create(destination);
            }

            BinaryFormatter bf = new();
            bf.Serialize(file, data);
            file.Close();
        }

        /// <summary>
        /// Loads An object by loading from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static T Load<T>(string fileName)
        {
            string destination = Path.Combine(Application.persistentDataPath, fileName);
            FileStream file;

            if (File.Exists(destination)) {
                file = File.OpenRead(destination);
            } else {
                // Create a new data, i.e initialize a new game.
                return default(T);
            }

            BinaryFormatter bf = new();
            T data = (T)bf.Deserialize(file);
            file.Close();

            return data;
        }
    } 
}