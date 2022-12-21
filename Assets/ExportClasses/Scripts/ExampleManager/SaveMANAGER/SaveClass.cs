using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable] public class SaveClass: MonoBehaviour
{
#region Variables & Properties

[SerializeField] private Text text;
private int points;

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        points = 0;
    }

    #endregion

#region Methods

public void AddPoints()
{
    points++;
    SetText();
}

public void SetText()
{
    text.text = points.ToString();
}

#endregion

}
