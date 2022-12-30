using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData: DataClassParent
{
    public int score=0;
    public List<int> inventory = new List<int>();
}

public class Player: SaveClassParent<DataClassConcrete>
{

    [SerializeField] private Text textReference;

    private void Awake()
    {
        
    }

    public void GainScore()
    {
        data.score++;
        textReference.text = data.score.ToString();
    }

    public override void SaveClass()
    {
        data.textReference = textReference;
        base.SaveClass();
    }
}
