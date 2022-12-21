using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem : Singleton<SaveSystem>
{
    #region Variables & Properties

    #endregion

    #region MonoBehaviour


    #endregion

    #region Methods

    public void Save(string saveName, object objectToSave)
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path =Path.Combine(Application.persistentDataPath, saveName + ".json");
        FileStream stream = new FileStream(path, FileMode.Create);

       object data = objectToSave;

        formatter.Serialize(stream, objectToSave);
        stream.Close();
    }

    public T Load<T>(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            object data = formatter.Deserialize(stream) as object;
            stream.Close();

            return (T)data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return default(T);
        }

        

    }
    
    #endregion
}