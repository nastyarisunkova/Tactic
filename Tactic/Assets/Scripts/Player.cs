using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData Data;
    void Start()
    {
        Data = new PlayerData(1, 1, 1, 1);
        Base[] bases = FindObjectsOfType<Base>();
        foreach(Base myBase in bases)
        {
            myBase.Data = Data; 
        }
    }

    void Update()
    {
        
    }
}
[System.Serializable]
public class PlayerData
{
    public float attack;
    public float defense;
    public float speed;
    public float reproductionTime;

    public PlayerData(float attack, float defense, float speed, float reproductionTime)
    {
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.reproductionTime = reproductionTime;

    }
}
