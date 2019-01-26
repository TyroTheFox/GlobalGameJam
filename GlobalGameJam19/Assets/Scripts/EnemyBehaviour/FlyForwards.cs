using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyForwards : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public BoxCollider2D boxColl;

    public bool moveRight = true;

    public float speed = 10.0f;
    public float maxVelocityChange = 10.0f;

    void Awake()
    {
        rigidBody.freezeRotation = true;
        rigidBody.gravityScale = 0;
    }

    void FixedUpdate()
    {
        // Calculate how fast we should be moving
        Vector2 targetVelocity;
        if (moveRight)
        {
            targetVelocity = transform.right;
        }
        else
        {
            targetVelocity = -transform.right;
        }
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;
        Debug.Log(targetVelocity);
        // Apply a force that attempts to reach our target velocity
        Vector2 velocity = rigidBody.velocity;
        Vector2 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rigidBody.AddForce(velocityChange, ForceMode2D.Force);
    }
}
