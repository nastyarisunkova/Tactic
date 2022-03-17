using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AI : MonoBehaviour
{
    public PlayerData Data;
    public float analyzingTime;
    public string StartTag;

    void Start()
    {
        Base[] bases = FindObjectsOfType<Base>();
        foreach (Base myBase in bases)
        {
            if (myBase.gameObject.tag == StartTag)
                myBase.Data = Data;
        }
        StartCoroutine(Analyze());
    }

    IEnumerator Analyze()
    {
        while (true)
        {
            if (analyzingTime > 0)
                yield return new WaitForSeconds(analyzingTime);
            else
                yield return new WaitForSeconds(4);

            Base[] Bases = FindObjectsOfType<Base>();
            var myBases = Bases.Where(myBase => myBase.Data == Data);
            Bases = Bases.Where(enemyBase => enemyBase.Data != Data).ToArray();
            var total = TotalWeight(Bases);
            var target = Bases.Where(enemyBase => enemyBase.mass < total / 3).Min();
            if (target != null)
            {
                foreach (Base myBase in myBases)
                {
                    myBase.SendUnits(target.gameObject);
                }
            }
        }
    }

    float TotalWeight(Base[] bases)
    {
        float total = 0;
        foreach (Base myBase in bases)
        {
            total += myBase.mass;
        }
        return total;
    }
}
