using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Serialization;


[System.Serializable]
public class ObjectToPool
{
    public GameObject objectPoolable;
    public GameObject parentWhenDeactivated;
    public GameObject parentWhenActivated;
    public int quantity;
    public bool expandable;
    public Transform transform;
}

public class ObjectPooler<T> : MonoBehaviour where T : ObjectPooler<T>
{
    #region Variables & Properties

   [SerializeField] private List<ObjectToPool> listObjectPoolables;
   private Dictionary<ObjectToPool, List<GameObject>> listsObjectPoolables = new Dictionary<ObjectToPool, List<GameObject>>();

   #endregion

    #region MonoBehaviour

    // Awake is called when the script instance is being loaded
    protected void Awake()
    {
        InitPoolLists();
    }

    #endregion

    #region Methods

    protected void InitPoolLists()
    {
        for (int i = 0; i < listObjectPoolables.Count; i++)
        {
            List<GameObject> objectPoolableList = new List<GameObject>();
            for (int j = 0; j < listObjectPoolables[i].quantity; j++)
            {
                GameObject obj= Instantiate(listObjectPoolables[i].objectPoolable, listObjectPoolables[i].transform.position, listObjectPoolables[i].transform.rotation);
                obj.GetComponent<ObjectPoolable>().SetParents(listObjectPoolables[i].parentWhenDeactivated, listObjectPoolables[i].parentWhenActivated);
                if (obj == null)
                {
                    Debug.Log("Null");
                }
                obj.gameObject.SetActive(false);
                objectPoolableList.Add(obj);
                obj.gameObject.GetComponent<ObjectPoolable>().AttachToDeactivatedParent();
                
            }
            listsObjectPoolables.Add(listObjectPoolables[i], objectPoolableList);
        }
    }

    protected GameObject GetPooledObject<T>(T itemType)
    {
        var type = itemType.GetType();

        int i = 0;
        foreach (var key in listsObjectPoolables.Keys)
        {
            if(listsObjectPoolables[key][i].GetComponent<ObjectPoolable>().GetType()==type)
            {
                foreach (var item in listsObjectPoolables[key])
                {
                    if (!item.GetComponent<ObjectPoolable>().IsActive() && item.GetComponent<ObjectPoolable>().GetType()==type)
                    {
                        return item;
                    }

                }

                foreach (var item in listsObjectPoolables[key])
                {
                    if (item.GetComponent<ObjectPoolable>().IsActive() && item.GetType()==type)
                    {
                        if (key.expandable)
                        {
                            Debug.Log(key.ToString() + " pool list in the object " + this.name + " expanded");
                            for (int j = 0; j < listObjectPoolables.Count; j++)
                            {
                                if (listObjectPoolables[j] == key)
                                {
                                    GameObject obj = Instantiate(listObjectPoolables[j].objectPoolable);
                                    obj.GetComponent<ObjectPoolable>().SetParents(
                                        listObjectPoolables[j].parentWhenDeactivated,
                                        listObjectPoolables[j].parentWhenActivated);
                                    obj.gameObject.SetActive(false);
                                    listsObjectPoolables[key].Add(obj);
                                    return obj;
                                }
                            }
                        }
                        else
                        {
                            Debug.Log(listsObjectPoolables[key][i].GetType().Name + " can't be added into " +
                                      listsObjectPoolables[key].GetType().Name);
                        }
                    }
                }

                break;
            }

                i++;
        }
        
        return null;
    }

    protected void SpawnObjectPoolable<T>()
    {

        var type = typeof(T);

        int i = 0;
        foreach (var key in listsObjectPoolables.Keys)
        {

            if (listsObjectPoolables[key][i].GetComponent<ObjectPoolable>().GetType() == type)
            {
                GameObject objectPoolable = GetPooledObject(listsObjectPoolables[key][i].GetComponent<ObjectPoolable>());
                if (objectPoolable != null)
                {
                    objectPoolable.transform.position = key.transform.position;
                    objectPoolable.transform.rotation = key.transform.rotation;
                    objectPoolable.gameObject.GetComponent<ObjectPoolable>().AttachToActivatedParent();
                    Debug.Log("Object " + type.ToString() + " activated!");
                    objectPoolable.gameObject.SetActive(true);
                    objectPoolable.gameObject.GetComponent<ObjectPoolable>().OnSpawn();
                    
                    return;
                }
                else
                {
                    Debug.Log("Not founded");
                    return;
                }
            }

            i++;
        }
    }
    
    
    #endregion
}
