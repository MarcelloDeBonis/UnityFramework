using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SaveClassParent : MonoBehaviour

{
#region Variables & Properties

[System.Serializable]
public class DataClassParent
{
    private int points;
    private int bestscore;
    private Mesh myMesh;
}

private DataClassParent data;
[SerializeField] private Text text;


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
