using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

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
    private bool isGroundPounding;

    public float dashPower;
    private float dashDelay;
    public float dashDelayTime;
    private bool isDashing;

    //Vector to hold player position
    private Vector2 playerVelocity;
    public Animator animator;

    private Player player;
    
    public AudioClip dash;
    public AudioClip startUp;
    public AudioClip groundPound;
    public AudioClip land;
    private bool playNoise = false;
    
    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        player = ReInput.players.GetSystemPlayer();
        jumpDelay = jumpDelaytime;
        rd = GetComponent<Rigidbody2D>();
        isLookingRight = true;
        isGroundPounding = false;
        isDashing = false;
        source.PlayOneShot(startUp);
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
        animator.SetBool("Grounded", isGrounded);
        if (isGrounded)
        {

            
            animator.SetBool("GroundPound", false);
            animator.SetBool("Jumping", false);
         }

        /*else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusToGround, groundItems);
        }*/
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Level") && playNoise)
        {
            if (isGroundPounding)
            {
                source.PlayOneShot(groundPound);
            }
            else
            {
                source.PlayOneShot(land);
            }

            playNoise = false;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Level"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Level"))
        {
            isGrounded = false;
            playNoise = true;
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
                isGroundPounding = false;
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
            if (!isGroundPounding)
            {
                var move = new Vector3(player.GetAxis("Horizontal"), 0, 0);
                Vector3 deltaSpeed = move * speed * Time.deltaTime;
                transform.position += deltaSpeed;
                animator.SetFloat("Move", Mathf.Abs(move.x));

                if (player.GetAxis("Horizontal") > 0 && isLookingRight == false)
                {
                    isLookingRight = true;
                    rotateModel();
                }
                else if (player.GetAxis("Horizontal") < 0 && isLookingRight == true)
                {
                    isLookingRight = false;
                    rotateModel();
                }
                
                if (dashDelay > 0)
                {
                    dashDelay -= Time.deltaTime;
                    if (isDashing && dashDelay <= dashDelayTime - 0.1)
                    {
                        rd.velocity = new Vector2(0, 0);
                        isDashing = false;
                    }
                }
                else if (player.GetButtonDown("Dash"))
                {
                    source.PlayOneShot(dash);
                    int dir;
                    if (isLookingRight) { dir = 1; }
                    else { dir = -1; }

                    rd.AddForce(new Vector2(dir * dashPower, 0));
                    dashDelay = dashDelayTime;
                    isDashing = true;
                    
                }
            }




            if (player.GetButtonDown("Jump") && !isGrounded && !canDoubleJump && canGroundPound)
            {
                animator.SetBool("GroundPound", true);
                isGroundPounding = true;
                poundDelay = poundDelayTime;
                canGroundPound = false;

            }

            //Get the jump axis and have the character jump
            if (player.GetButtonDown("Jump") && !isGrounded && canDoubleJump)
            {
                animator.SetTrigger("ExtraJump");
                CharacterJump();
                //Debug.Log("jump2");
                canDoubleJump = false;
                canGroundPound = true;
            }

            //Get the jump axis and have the character jump - using get button instead of axis as we need specific key down actions
            if (player.GetButtonDown("Jump") && isGrounded)
            {
                animator.SetBool("Jumping", true);
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

    public void stopSlamming()
    {
        isGroundPounding = false;
    }
}

