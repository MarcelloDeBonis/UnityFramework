using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveClassParent: MonoBehaviour
{
    
#region Variables & Properties

[SerializeField] public string path;

#endregion

#region Methods

public virtual void SaveClass()
{
    SaveSystem.Instance.Save(this, path);
}

public virtual void LoadClass()
{
    SaveSystem.Instance.Load(this, path);
}

#endregion

}
