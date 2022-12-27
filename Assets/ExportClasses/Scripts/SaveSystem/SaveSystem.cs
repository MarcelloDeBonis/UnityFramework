using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class SaveSystem : MonoBehaviour
{
    
    #region Methods

    public static void Save(string fileName, string directory, SaveClass obj)
    {
        if (!DirectoryExists(directory))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(GetFullPath(fileName, directory));
        formatter.Serialize(file, obj);
        file.Close();
    }

    public static SaveClass Load(string fileName, string directory)
    {

        if (SaveExists(fileName, directory))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(GetFullPath(fileName, directory), FileMode.Open);
                SaveClass obj = (SaveClass)formatter.Deserialize(file);
                file.Close();
                return obj;
            }
            catch (SerializationException)
            {
                Debug.Log("Failed to load file!");
            }
        }

        return null;
    }

    private static bool SaveExists(string fileName, string directory)
    {
        return File.Exists(GetFullPath(fileName, directory));
    }

    private static bool DirectoryExists(string directory)
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }
    
    
    public static string GetFullPath(string fileName, string directory)
    {
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }
    
    #endregion
}