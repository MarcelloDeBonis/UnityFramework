using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DataClassParent
{
    
}

public class SaveClassParent<T>: MonoBehaviour where T: DataClassParent
{
    
#region Variables & Properties

    public T data;
    [SerializeField] private string fileName;
    [SerializeField] private string directory;

#endregion

#region Methods

public virtual void SaveClass()
{
    SaveSystem.Instance.Save(data, fileName, directory);
}

public virtual void LoadData()
{
    data = SaveSystem.Instance.Load<T>(fileName, directory);
}


#endregion

}
