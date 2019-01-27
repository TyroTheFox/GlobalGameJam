using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    BoxCollider2D cd;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<BoxCollider2D>();
        cd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GroundPoundActivated()
    {
        cd.enabled = true;
    }

    public void GroundSlam()
    {
        cd.enabled = false;
    }

    public bool isEnabled()
    {
        return cd.enabled;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Health health = col.transform.GetComponent<Health>();
        if (health != null)
        {
           

             Debug.Log("You made them oof");
             player.GetComponent<Health>().deactivateInvul(1f);
                
             health.TakeDamage(1);
            
        }

        player.GetComponent<PlayerController>().stopSlamming();


    }
}
