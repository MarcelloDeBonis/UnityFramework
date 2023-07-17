using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class Player : SaveClassParent<Points>
{

// Defines variables and properties
#region Variables & Properties

[SerializeField] private TextMeshProUGUI text;

#endregion

// Defines MonoBehaviour lifecycle events
#region MonoBehaviour

//Called during Unity executions
#region Updates Events

private void Awake()
{
    data = new Points();
}

// Called once per frame
void Update()
{
    text.text = data.value.ToString();
}

#endregion


#endregion

// Defines methods for the new script
#region Methods

public void AddPoint()
{
    data.value++;
}

#endregion

}
