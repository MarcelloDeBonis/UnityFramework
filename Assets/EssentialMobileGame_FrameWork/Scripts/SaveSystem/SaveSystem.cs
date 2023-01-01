using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class SaveSystem : Singleton<SaveSystem>
{
    
    #region Methods

    public void Save(DataClassParent obj, string fileName, string directory)
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

    public T Load<T>(string fileName, string directory) where T: DataClassParent
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(GetFullPath(fileName, directory), FileMode.Open);
        T obj = (T)formatter.Deserialize(file);
        file.Close();
        return obj;
    }

    private bool DirectoryExists(string directory)
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }
    
    
    private string GetFullPath(string fileName, string directory)
    {
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }
    
    #endregion
}