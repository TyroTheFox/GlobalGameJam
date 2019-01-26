using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    BoxCollider2D cd;


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

}
