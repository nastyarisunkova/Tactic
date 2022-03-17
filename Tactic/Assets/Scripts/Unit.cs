using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public PlayerData Data;
    public GameObject EndPoint;
    private Vector2 endPosition;
    public void SetTarget(GameObject EndPoint)
    {
        this.EndPoint = EndPoint;
        endPosition = EndPoint.transform.position - transform.position;
        endPosition = endPosition.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(endPosition * Data.speed / 40);
    }
}
