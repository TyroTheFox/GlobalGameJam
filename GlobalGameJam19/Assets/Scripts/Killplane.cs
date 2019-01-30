using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killplane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Health health = other.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(health.MaxHP);
            }
        }
    }
}
