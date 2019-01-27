using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendArea : MonoBehaviour {

    public Transform enemyToWarn;
    private AttackWhenInRange attack;
    private FlyingAttackWhenInRange flyAttack;

    void Start()
    {
        if(enemyToWarn != null)
        {
            attack = enemyToWarn.GetComponent<AttackWhenInRange>();
            flyAttack = enemyToWarn.GetComponent<FlyingAttackWhenInRange>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player") { return; }
        if (attack != null)
            attack.goToPoint = col.transform;
        if (flyAttack != null)
            flyAttack.goToPoint = col.transform;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "Player") { return; }
        if (attack != null)
            attack.goToPoint = col.transform;
        if (flyAttack != null)
            flyAttack.goToPoint = col.transform;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Player") { return; }
        if (attack != null)
            attack.goToPoint = transform;
        if (flyAttack != null)
            flyAttack.goToPoint = transform;
    }

     Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
