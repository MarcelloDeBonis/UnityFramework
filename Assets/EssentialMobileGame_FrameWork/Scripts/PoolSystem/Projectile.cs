using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ObjectPoolable
{
#region Variables & Properties

[SerializeField] private float lifeTime;

#endregion

#region MonoBehaviour

private void Awake()
{
    
}


#endregion

#region Methods

public override void OnSpawn()
{ 
  
  StartCoroutine(DeactiveAfterTime(lifeTime));
  
}

#endregion

}
