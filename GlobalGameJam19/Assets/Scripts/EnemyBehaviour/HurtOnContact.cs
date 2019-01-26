using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnContact : MonoBehaviour {

    public int damage = 1;
    public float pushForce = 20.0f;
    public Health my_health;

    void OnCollisionEnter2D(Collision2D col)
    {
        Health health = col.transform.GetComponent<Health>();
        if(health != null)
        {
            health.TakeDamage(damage);
            // Apply a force that attempts to reach our target velocity
            Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
            Vector2 targetVelocity = -col.contacts[0].normal;
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity.x *= pushForce * 0.5f;
            targetVelocity.y *= pushForce * rb.mass;

            if (rb != null)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddRelativeForce(targetVelocity, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.transform.GetComponent<Health>();
        if (health != null)
        {
            if (my_health.invulnerable)
            {
                my_health.deactivateInvul(1f);
            }
            health.TakeDamage(damage);
        }
    }

}
