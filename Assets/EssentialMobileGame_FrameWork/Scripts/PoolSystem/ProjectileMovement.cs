using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
#region Variables & Properties

private Rigidbody rb;
[SerializeField] private float speed;

#endregion

#region MonoBehaviour

private void Awake()
{
    rb = GetComponent<Rigidbody>();
}


    private void OnEnable()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

#region Methods



#endregion

}
