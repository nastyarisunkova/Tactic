using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public PlayerData Data;
    public GameObject Unit;
    public GameObject EndEndPoint;
    public float mass;
    public void SendUnits(GameObject EndPoint)
    {
        if (EndPoint != gameObject)
        {
            GameObject newUnit;
            for (int i = 0; i < (int)mass / 2; i++)
            {
                newUnit = Instantiate(Unit, transform.position + new Vector3(Random.Range(-0.25f, 0.25f),
                    Random.Range(-0.25f, 0.25f)), Quaternion.identity);
                var unitScript = newUnit.GetComponent<Unit>();
                unitScript.SetTarget(EndPoint);
                unitScript.Data = Data;
                unitScript.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            }
            mass -= ((int)mass) / 2;
        }
    }

    private void OnMouseDown()
    {
        SendUnits(EndEndPoint);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Unit")
        {
            var unitScript = col.gameObject.GetComponent<Unit>();
            if(unitScript.EndPoint == gameObject)
            {
                mass++;
                Destroy(col.gameObject);
            }
        }
    }
}
