using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPoint : MonoBehaviour
{
    public AudioSource activationObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            activationObject.enabled = true;
        }
    }
}
