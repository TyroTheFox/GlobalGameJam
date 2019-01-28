using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public bool dead = false;
    public bool invulnerable = false;
    private float invalDelay;
    public int MaxHP = 3;
    public int HP;
    public Animator animator;

	// Use this for initialization
	void Start () {
        HP = MaxHP;
        invalDelay = 0;
        animator = GetComponentInChildren<Animator>();
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
            if(animator != null)
            animator.SetBool("Dead", dead);
        }
        else
        {
            dead = false;
            if(animator != null)
                animator.SetBool("Dead", dead);
        }

        if (dead)
        {
            //Debug.Log("DEAD");
            gameObject.SetActive(false);
            if (tag == "Player")
            {
                Application.LoadLevel(Application.loadedLevel); //might not use
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            HP -= damage;
            invalDelay = 0.5f;
            invulnerable = true;
            if(animator != null)
            animator.SetTrigger("BigHit");
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

    public void pickupShield()
    {
        HP += 2;
    }
}
