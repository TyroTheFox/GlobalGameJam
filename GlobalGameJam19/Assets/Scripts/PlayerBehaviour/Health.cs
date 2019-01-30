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
    
    public SimpleHealthBar healthBar;

    public AudioSource source;
    public AudioClip deathNoise;
    public AudioClip hurtNoise;
	// Use this for initialization
	void Start () {
        HP = MaxHP;
        invalDelay = 0;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.UpdateBar(HP, MaxHP);
        }

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
            if (source != null)
            {
                source.PlayOneShot(deathNoise);
            } 
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
            if (tag == "Player" && !source.isPlaying)
            {
                Application.LoadLevel(Application.loadedLevel); //might not use
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            if (source != null)
            {
                source.PlayOneShot(hurtNoise);
            } 
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
