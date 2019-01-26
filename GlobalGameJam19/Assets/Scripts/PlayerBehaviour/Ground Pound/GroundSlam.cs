using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    BoxCollider2D cd;
    //public CamControl cam;

    private float poundDelay;
    public float poundDelayTime;
    public Health thealth;
    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<BoxCollider2D>();
        cd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (poundDelay > 0)
        {
            poundDelay -= Time.deltaTime;

            if (poundDelay <= 0)
            {
                poundDelay = 0;
                cd.enabled = false;
                thealth.deactivateInvul();
                Debug.Log("dcfxvtgyhui");
                //CamControl
            }

        }
    }

    public void Slamming()
    {
        cd.enabled = true;
        poundDelay = poundDelayTime;
        //cam.ShakeCam();
    }
}
