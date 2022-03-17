using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public PlayerData Data;
    public PlayerData nullData = new PlayerData(1, 1, 1, 0);
    public GameObject Unit;
    public float mass;
    public float randomRegion;
    private void Start()
    {
        if(mass == 0 || tag == "Untagged")
        {
            Data = nullData;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
    public void SendUnits(GameObject EndPoint)
    {
        if (EndPoint != gameObject)
        {
            GameObject newUnit;
            for (int i = 0; i < (int)mass / 2; i++)
            {
                newUnit = Instantiate(Unit, transform.position + new Vector3(Random.Range(-randomRegion, randomRegion),
                    Random.Range(-randomRegion, randomRegion)), Quaternion.identity);
                var unitScript = newUnit.GetComponent<Unit>();
                unitScript.SetTarget(EndPoint);
                unitScript.Data = Data;
                unitScript.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            }
            mass -= ((int)mass) / 2;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Unit")
        {
            var unitScript = col.gameObject.GetComponent<Unit>();
            if(unitScript.EndPoint == gameObject && unitScript.Data == Data)
            {
                mass++;
                Destroy(col.gameObject);
            }
            else if(unitScript.Data != Data)
            {
                float attack = unitScript.Data.attack / Data.defense;
                mass -= attack;
                if(Mathf.RoundToInt(mass)==0)
                {
                    Data = nullData;
                    GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else if(Mathf.RoundToInt(mass)<0)
                {
                    Data = unitScript.Data;
                    GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color;
                    mass = -mass;
                }
                Destroy(col.gameObject);
            }
        }
    }
}
