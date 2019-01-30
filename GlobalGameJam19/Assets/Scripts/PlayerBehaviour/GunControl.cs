using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GunControl : MonoBehaviour
{
    // Start is called before the first frame update
    public PolygonCollider2D poly;
    public float fireRate;
    private float polyDelay;
    private float fireDelay;
    public ParticleSystem party;

    private Player player;
    
    public AudioSource source;
    public AudioClip shootSound;

    void Start()
    {
        fireDelay = fireRate;
        poly.enabled = false;
        polyDelay = 0;
        player = ReInput.players.GetSystemPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
        else if (player.GetButton("Attack"))
        {
            poly.enabled = true;
            polyDelay = 0.15f;
            fireDelay = fireRate;
            party.Emit(100);
            Debug.Log("fire");
            source.PlayOneShot(shootSound);

        }
        if (polyDelay > 0)
        {
            polyDelay -= Time.deltaTime;
        }
        else
        {
            poly.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            Health health = col.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}
