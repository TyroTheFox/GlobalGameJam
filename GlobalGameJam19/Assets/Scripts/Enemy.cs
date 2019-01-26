using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public LayerMask groundItems;
    public float enemySpeed = 1f;
    private RigidBody2D enemyBody;
    private Transform enemyTransform;
    private float enemyWidth;
    private Vector2 enemyVel;
    private bool isGrounded;
    private Vector2 distance = new Vector2(0.0f, -0.5f);

    void Start()
    {
        enemyTransform = this.transform;
        enemyBody = GetComponent<RigidBody2D>();

        enemyWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void FixedUpdate ()
    {
        Vector2 lineCastPosition = enemyTransform.position - enemyTransform.right * enemyWidth;

        isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + distance, groundItems);
        Debug.DrawLine(lineCastPosition, lineCastPosition + distance);

        if (!isGrounded)
        {
            Vector3 currRotation = enemyTransform.eulerAngles;
            currRotation.y += 100;
            enemyTransform.eulerAngles = currRotation;
        }
        enemyVel = enemyBody.velocity;
        enemyVel.x = -enemyTransform.right.x * enemySpeed;
        enemyBody.velocity = enemyVel;
    }
}
