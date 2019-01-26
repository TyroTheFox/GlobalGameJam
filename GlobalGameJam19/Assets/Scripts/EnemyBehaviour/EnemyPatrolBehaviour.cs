using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : MonoBehaviour {

    public Rigidbody2D rigidBody;
    public BoxCollider2D boxColl;

    public float tollerance = 5.0f;
    public Transform[] patrolNodes;
    public Transform currentNode;
    public int i = 0;
    
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    
    private bool grounded = false;

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
        if (Mathf.Abs(Vector2.Distance(currentNode.position, transform.position)) <= tollerance)
        {
            if(i >= patrolNodes.Length)
            {
                i = 0;
            }
            else { i++; }
            currentNode = patrolNodes[i];
        }
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector2 targetVelocity = (currentNode.transform.position - transform.position).normalized;
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;
            
            // Apply a force that attempts to reach our target velocity
            Vector2 velocity = rigidBody.velocity;
            Vector2 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidBody.AddRelativeForce(velocityChange, ForceMode2D.Force);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                rigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed());
            }
        }

        // We apply gravity manually for more tuning control
        rigidBody.AddForce(new Vector3(0, -gravity * rigidBody.mass, 0));

        grounded = false;
    }

    void OnCollisionStay2D()
    {
        grounded = true;
        
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}
