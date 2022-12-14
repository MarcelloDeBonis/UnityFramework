using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //The Instance of class T available from everywhere
    public static T Instance { get; private set; } = null;

    //Get and set are the equivalent of these methods.
    //public T GetInstance() { return Instance; }
    //private void SetInstance(T instance) { Instance = instance; }

    [SerializeField] protected bool IsPersistent;

    protected virtual void Awake()
    {
        SetSingleInstance();
    }

    protected virtual void OnEnable()
    {
        SetSingleInstance();
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }

    private void SetSingleInstance()
    {
        //There is already an instance
        if (Instance == this)
            return;
        else if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //Find all the instances of type T in the scenes and save them into an array
        T[] instances = FindObjectsOfType<T>();

        if(instances.Length > 1)//More than one instance is found in the scene
        {
            Debug.LogError("Multiple instances of " + typeof(T).Name + " found");
            //Set this as instance. The others will be destroyed in their Awake
            Instance = this as T;
        }
        else if(instances.Length == 1)//Only one instance of type T is found. Set the instance.
        {
            Instance = instances[0];
        }
        if (IsPersistent)
            DontDestroyOnLoad(gameObject);
    }

}