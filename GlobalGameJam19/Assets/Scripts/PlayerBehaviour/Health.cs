using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public bool dead = false;
    public int MaxHP = 3;
    public int HP;

	// Use this for initialization
	void Start () {
        HP = MaxHP;
    }

    void Update()
    {
        if(HP <= 0)
        {
            dead = true;
        }

        if (dead)
        {
            Debug.Log("DEAD");
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
}
