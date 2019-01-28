using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrol : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public BoxCollider2D boxColl;

    public float tollerance = 5.0f;
    public Transform[] patrolNodes;
    public Transform currentNode;
    public int i = 0;

    public float speed = 10.0f;
    public float maxVelocityChange = 10.0f;

    void Awake()
    {
        rigidBody.freezeRotation = true;
        rigidBody.gravityScale = 0;
        if (patrolNodes.Length > 0)
        {
            currentNode = patrolNodes[i];
        }
    }

    void FixedUpdate()
    {
        if(patrolNodes.Length <= 0){return;}
        if (Mathf.Abs(Vector2.Distance(currentNode.position, transform.position)) <= tollerance)
        {
            
            if (i >= patrolNodes.Length)
            {
                i = 0;
            }
            else
            {
                currentNode = patrolNodes[i];
                i++;
            }
        }
            // Calculate how fast we should be moving
            Vector2 targetVelocity = (currentNode.transform.position - transform.position).normalized;
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
