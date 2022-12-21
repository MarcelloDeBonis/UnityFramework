using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
#region Variables & Properties

[SerializeField] private SaveClass instance;

#endregion

#region MonoBehaviour

#endregion

#region Methods

public void saveinstace()
{
    SaveSystem.Instance.Save("ciao", instance);
}

public void loadinstance()
{
    instance = SaveSystem.Instance.Load<SaveClass>("ciao");
    instance.SetText();
}

#endregion

}
