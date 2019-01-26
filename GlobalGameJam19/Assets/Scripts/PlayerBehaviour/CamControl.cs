using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    Transform tf;
    string state;
    private float shakeTimer;
    public float shakeDuration;
    public float shakePower;

    float dir;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        state = "normal";
        dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        if (state == "Shaking")
        {
            
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0)
                {
                    shakeTimer = 0;
                    state = "Normalising";
                }
            }
            else if (tf.position.x > 1 || tf.position.x < -1)
            {
                //dir = 
                tf.Translate(new Vector3(shakePower * dir * Time.deltaTime, 0, 0));
            }
            else
            {
                tf.Translate(new Vector3(shakePower * Time.deltaTime, 0, 0));
            }
        }
        else if (state == "Normalising")
        {
            if(tf.position.x < 0.2 && tf.position.x > -0.2)
            {
                tf.position.Set(0, tf.position.y, tf.position.z);
                state = "normal";
            }
            else
            {
                tf.Translate(new Vector3(-tf.position.x * Time.deltaTime, 0, 0));
            }
        }
    }


    public void ShakeCam()
    {
        state = "Shaking";
        shakeTimer = shakeDuration;
    }
}
