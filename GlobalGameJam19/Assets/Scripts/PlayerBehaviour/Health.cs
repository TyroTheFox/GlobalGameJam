using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public bool dead = false;
    public bool invulnerable = false;
    private float invalDelay;
    public int MaxHP = 3;
    public int HP;

	// Use this for initialization
	void Start () {
        HP = MaxHP;
        invalDelay = 0;
    }

    void Update()
    {
        if (invalDelay > 0)
        {
            invalDelay -= Time.deltaTime;
            if (invalDelay<=0)
            {
                invulnerable = false;
            }
        }



        if(HP <= 0)
        {
            dead = true;
        }

        if (dead)
        {
            //Debug.Log("DEAD");
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            HP -= damage;
        }
    }

    public void activateInvul()
    {
        invulnerable = true;
    }

    public void deactivateInvul()
    {
        invulnerable = false;
    }

    public void deactivateInvul(float delay)
    {
        invalDelay = delay;
    }
}
