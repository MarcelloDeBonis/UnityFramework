using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerData: DataClassParent
{
    public int score;
    public List<int> inventory = new List<int>();
}

public class Player: SaveClassParent<PlayerData>
{

    [SerializeField] private Text textReference;

    private void Awake()
    {
        data = new PlayerData();
    }

    public void GainScore()
    {
        data.score++;
        textReference.text = data.score.ToString();
    }

    public override void SaveClass()
    {
        base.SaveClass();
    }
}
