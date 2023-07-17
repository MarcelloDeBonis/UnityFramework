using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEditor;
using UnityEngine;

public class ProjectilePooler : ObjectPooler<ProjectilePooler>
{
#region Variables & Properties


#endregion

#region MonoBehaviour

#endregion

#region Methods

public void Fire()
{
    SpawnObjectPoolable<Projectile>();

}

#endregion

}
