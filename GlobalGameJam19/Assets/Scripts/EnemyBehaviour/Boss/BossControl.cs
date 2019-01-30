using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{

    private bool isLookingRight;
    public Transform player;
    public bool lockAttacking;

    public ParticleSystem party;
    public BoxCollider2D fireBox;

    public float meleeRange;

    public float attackTimer;
    public float attackDelay;
    public Animator animator;

    public AudioClip attackSound;
    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        isLookingRight = false;
        lockAttacking = false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockAttacking)
        {
            source.PlayOneShot(attackSound);
            if(animator != null)
                animator.SetTrigger("SlamPunch");
            if (player.position.x - GetComponent<Transform>().position.x > 0.2 && isLookingRight == false)
            {
                isLookingRight = true;
                fireBox.gameObject.GetComponent<AttackBox>().isLookingRight = true;
                rotateModel();
            }
            else if (player.position.x - GetComponent<Transform>().position.x < -0.2 && isLookingRight == true)
            {
                isLookingRight = false;
                fireBox.gameObject.GetComponent<AttackBox>().isLookingRight = false;
                rotateModel();
            }
        }

        if (attackTimer > 0)
        {

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                fireBox.enabled = false;
                lockAttacking = false;
            }
        }
        else
        {
            if (attackDelay > 0)
            {
                attackDelay -= Time.deltaTime;
            }
            else
            {
                Debug.Log(Mathf.Abs(player.position.x - GetComponent<Transform>().position.x));
                Debug.Log(meleeRange);
                if (Mathf.Abs(player.position.x - GetComponent<Transform>().position.x) > meleeRange)
                {
                    party.Play();
                    fireBox.enabled = true;
                    attackTimer = 2;
                    attackDelay = 2;
                    lockAttacking = true;
                }
                else
                {
                    //punch
                }


            }

        }
    }


    private void rotateModel()
    {
        GetComponent<Transform>().localRotation *= Quaternion.Euler(0, 180, 0);
    }
}
