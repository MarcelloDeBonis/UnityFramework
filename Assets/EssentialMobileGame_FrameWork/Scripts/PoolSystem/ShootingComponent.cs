using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    
#region Variables & Properties

[SerializeField] private ProjectilePooler projectilePooler;
[SerializeField] private KeyCode key;

#endregion

#region MonoBehaviour

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("Fired!");
            projectilePooler.Fire();
        }
    }

#endregion

#region Methods



#endregion

}
