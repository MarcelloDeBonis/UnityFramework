using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    [SerializeField] private SaveClassConcrete myClass;

    public void AddScore()
    {
        myClass.GainScore();
    }

    public void LoadAllClass()
    {
        myClass.LoadData();
    }

    public void SaveAllClass()
    {
        myClass.SaveClass();
    }
}
