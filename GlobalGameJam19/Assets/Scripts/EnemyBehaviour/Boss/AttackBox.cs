using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{ 

    public ParticleSystem party;
    public BossControl boss;

    public float knockBack;

    public bool isLookingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            Health health = col.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
            Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
            int dir;
            if (isLookingRight)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            rb.AddForce(new Vector2(dir * knockBack, 0));
        }
    }
}
