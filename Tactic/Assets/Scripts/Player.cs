using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData Data;

    public List<GameObject> ChangedBases;
    public GameObject EndPoint;

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
#if UNITY_ANDROID
        if (Input.touchCount>0)
        {
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Collider2D col = Physics2D.Raycast(touchPosition, transform.position).collider;
            AddBese(col);
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.Raycast(touchPosition, transform.position).collider;
            AddBese(col);
        }
#endif
        else if(ChangedBases.Count!=0)
        {
            foreach(GameObject myBase in ChangedBases)
            {
                Base bs = myBase.GetComponent<Base>();
                if(bs.Data == Data)
                {
                    bs.SendUnits(EndPoint);
                }
            }

            ClearList();
        }
    }

    void ClearList()
    {
        foreach(GameObject myBase in ChangedBases)
        {
            LineRenderer lr = myBase.GetComponent<LineRenderer>();
            lr.SetPosition(1, myBase.transform.position);
        }
        ChangedBases.Clear();
        EndPoint = null;
    }

    void AddBese(Collider2D col)
    {
        if(col!=null)
        {
            var obj = col.gameObject;
            var bs = obj.GetComponent<Base>();
            if(bs !=null)
            {
                if((bs.Data == Data)&&(!ChangedBases.Contains(obj)))
                {
                    ChangedBases.Add(obj);
                }
                EndPoint = obj;
                
                foreach(GameObject myBase in ChangedBases)
                {
                    LineRenderer lr = myBase.GetComponent<LineRenderer>();
                    if(bs.Data == Data)
                    {
                        lr.SetPosition(0, myBase.transform.position);
                        lr.SetPosition(1, EndPoint.transform.position);
                    }
                    else
                    {
                        lr.SetPosition(1, myBase.transform.position);
                    }
                }
            }
        }
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
