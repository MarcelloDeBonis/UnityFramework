using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolable : MonoBehaviour
{
    #region Variables & Properties
    
    private GameObject parentWhenDeactivated;
    private GameObject parentWhenActivated;
    
    #endregion

    #region MonoBehaviour

    protected virtual void OnEnable()
    {
        
    }

    public void AttachToActivatedParent()
    {
        gameObject.transform.parent = parentWhenActivated.transform;
    }

    public void AttachToDeactivatedParent()
    {
        gameObject.transform.parent = parentWhenDeactivated.transform;
    }
    
    protected virtual void OnDisable()
    {
        
    }

    #endregion

    #region Methods

    public void SetParents(GameObject parentDeactivation, GameObject parentActivation)
    {
        parentWhenDeactivated = parentDeactivation;
        parentWhenActivated = parentActivation;
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public IEnumerator DeactiveAfterTime(float dieTime)
    {
        float time = 0f;
        while (time < dieTime)
        {
            Debug.Log(time);
            time += Time.deltaTime;
            yield return null;
        }
        AttachToDeactivatedParent();
        gameObject.SetActive(false);
    }

    public virtual void OnSpawn()
    {
        
    }
    
    #endregion

}