using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataClassParent
{
    
}

public interface SaveClassParent<T> where T: DataClassParent

{
    
#region Variables & Properties


    public T data;
    [SerializeField] private string fileName;
    [SerializeField] private string directory;

#endregion

#region Methods

private virtual void Awake()
{
    
}

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
