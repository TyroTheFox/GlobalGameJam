using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rd;
    public GroundPound tgroundPound;
    public GroundSlam tgroundSlam;
    public Health thealth;
    public float speed = 5.0f;
    public float jumpForce = 400f;
    public float groundPoundForce = -2000f;

    //Items to check the player has the ability to jump
    public Transform groundCheck;
    public float radiusToGround;
    public LayerMask groundItems;
    private bool isGrounded;

    private float jumpDelay;
    public float jumpDelaytime;

    private float poundDelay;
    public float poundDelayTime;

    private bool isLookingRight;

    //Variable to hold double jump
    private bool canDoubleJump;
    private bool canGroundPound;

    //Vector to hold player position
    private Vector2 playerVelocity;



    // Start is called before the first frame update
    void Start()
    {
        jumpDelay = jumpDelaytime;
        rd = GetComponent<Rigidbody2D>();
        isLookingRight = true;
    }

    void FixedUpdate()
    {
        /*Check whether the player is grounded by getting the position of a empty game object that 
		 * is applied to the player. You then can define the size of the radius that us used from that position
		 * to the other objects. The objects are contained in a layer. This layer can contain a number of objects. We
		 * can use a layer of ground objects.
		 */
        if (jumpDelay > 0)
        {
            jumpDelay -= Time.deltaTime;
            if (jumpDelay < 0)
            {
                jumpDelay = 0;
            }
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusToGround, groundItems);
        }
    }

    // Update is called once per frame
    void Update()
    {


        //Get the position of the player at the first step
        playerVelocity = GetComponent<Rigidbody2D>().velocity;

        //Check if grounded so double jump is reset
        if (isGrounded)
        {
            canDoubleJump = false;
            canGroundPound = false;
            if (tgroundPound.isEnabled())
            {
                tgroundPound.GroundSlam();
                tgroundSlam.Slamming();
            }

        }

        if (poundDelay > 0)
        {
            poundDelay -= Time.deltaTime;

            if (poundDelay < 0)
            {
                poundDelay = 0;
                CharacterGroundPound();
                thealth.activateInvul();
            }
            else
            {
                rd.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * speed * Time.deltaTime;
            if (Input.GetAxis("Horizontal") > 0 && isLookingRight == false)
            {
                isLookingRight = true;
                rotateModel();
            }
            else if (Input.GetAxis("Horizontal") < 0 && isLookingRight == true)
            {
                isLookingRight = false;
                rotateModel();
            }


            if (Input.GetButtonDown("Jump") && !isGrounded && !canDoubleJump && canGroundPound)
            {
                poundDelay = poundDelayTime;
                canGroundPound = false;

            }

            //Get the jump axis and have the character jump
            if (Input.GetButtonDown("Jump") && !isGrounded && canDoubleJump)
            {
                CharacterJump();
                //Debug.Log("jump2");
                canDoubleJump = false;
                canGroundPound = true;
            }

            //Get the jump axis and have the character jump - using get button instead of axis as we need specific key down actions
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jumpDelay = jumpDelaytime;
                CharacterJump();
                isGrounded = false;
                canDoubleJump = true;
                canGroundPound = false;

            }
        }


    }

    private void CharacterJump()
    {
        rd.AddForce(new Vector2(playerVelocity.x, jumpForce));
        //Debug.Log("You Jumped");
    }

    private void CharacterGroundPound()
    {
        rd.AddForce(new Vector2(0, groundPoundForce));
        tgroundPound.GroundPoundActivated();
        //Debug.Log("You Ground Pounded");
    }

    private void rotateModel()
    {
        GetComponent<Transform>().localRotation *= Quaternion.Euler(0, 180, 0);
    }
}

