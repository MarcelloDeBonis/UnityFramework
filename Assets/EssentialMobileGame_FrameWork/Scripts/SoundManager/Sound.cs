using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

// Defines variables and properties
#region Variables & Properties

[SerializeField] public AudioClip clip;

#endregion

// Defines MonoBehaviour lifecycle events
#region MonoBehaviour


#region Activation/Deactivation

// Called when the object is enabled
void OnEnable()
{

}

// Called when the object is disabled
void OnDisable()
{

}

#endregion

// Called when the new script is loaded into the Unity editor
void Awake()
{

}

// Called before the first frame of the game
void Start()
{

}

//Called during Unity executions
#region Updates Events

// Called once per frame
void Update()
{

}

// Called once per frame, but at fixed intervals
void FixedUpdate()
{

}

// Called once per frame, after all other updates have been executed
void LateUpdate()
{

}


#endregion


#endregion

// Defines methods for the new script
#region Methods

public void PlaySound()
{
    SoundManager.Instance.PlaySound(clip);
}

#endregion

}
