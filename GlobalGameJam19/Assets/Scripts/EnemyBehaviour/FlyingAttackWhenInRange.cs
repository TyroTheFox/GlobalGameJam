using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackWhenInRange : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public BoxCollider2D boxColl;

    public Transform goToPoint;

    public float speed = 10.0f;
    public float maxVelocityChange = 10.0f;

    void Awake()
    {
        rigidBody.freezeRotation = true;
        rigidBody.gravityScale = 0;


    }

    void FixedUpdate()
    {
        if(goToPoint == null) { return; }
        // Calculate how fast we should be moving
        Vector2 targetVelocity = (goToPoint.position - transform.position).normalized;
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector2 velocity = rigidBody.velocity;
        Vector2 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        rigidBody.AddRelativeForce(velocityChange, ForceMode2D.Force);
    }
}
