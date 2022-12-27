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
    SaveSystem.Save("ciao","saluti", instance);
}

public void loadinstance()
{
    instance = SaveSystem.Load("ciao", "saluti");
    instance.SetText();
}

#endregion

}
