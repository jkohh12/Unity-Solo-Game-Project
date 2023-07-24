using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //--Component References

    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D coll;
    private SpriteRenderer sprite;

    private bool isFacingRight = true;

    public float directionX;

    private Vector2 newVelocity;
    private Vector2 newForce;




    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed;

    Vector2 vecGravity;

    [Header("Friction")]
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;



    [Header("Slope Check Variables")]
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float slopeCheckDistance;
    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;
    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private float slopeSideAngle;
    private bool isOnSlope;
    private bool canWalkOnSlope;


    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;


    [Header("Jump Syatem")]
    bool canJump;
    public bool isGrounded;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpStartTime;
    [SerializeField] private float fallStartTime;
    private float jumpTime;
    private float fallTime;
    private float jumpCounter;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;


    [Header("Knockback System")]
    [SerializeField] private float KBForce; //how powerful the knockback is
    [SerializeField] private float KBCounter; //counts down how much time left on effect
    [SerializeField] private float KBTotalTime; //how long knock back effect will last alltogether
    private bool KnockFromRight;



    //can implement max slope angle later if needed

    private bool isJumping;

    private enum MovementState { idle, running, falling, jumping, jumpingAction};


    // Start is called before the first frame update
    private void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();

        colliderSize = coll.size;
    }

    // Update is called once per frame
    private void Update()
    {

        directionX = Input.GetAxisRaw("Horizontal");

        //   rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);



        if (Input.GetButtonDown("Jump") && canJump && isGrounded)
        {

            canJump = false;
            isJumping = true;
            jumpTime = jumpStartTime;
            fallTime = fallStartTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //need to rework jumping somehow (collding with walls causes character jump super high)
            /*            canJump = false;
                        isJumping = true;
                        newVelocity.Set(0.0f, 0.0f);
                        rb.velocity = newVelocity;
                        newForce.Set(0.0f, jumpForce);
                        rb.AddForce(newForce, ForceMode2D.Impulse);*/

        }

        //block of code for "mario" jump
        if (Input.GetButton("Jump") && isJumping && !isOnSlope && !isGrounded)//check for holding space key
        {
            jumpCounter += Time.deltaTime;
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;
            if (jumpTime > 0)
            {
                if (t < 0.5f) //if half the jump time is up, the character moves upward slower.
                {
                    currentJumpM = jumpMultiplier * (1 - t);
                }
                //rb.velocity = new Vector2(rb.velocity.x, jumpMulitplier);
                rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
                jumpTime -= Time.deltaTime;
            }
            else
            {

                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")) //if the user isn't holding space, then isJumping is false, no more increase in jumpforce
        {

            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }


        if (rb.velocity.y < 0) // faster falling
        {

            //isFalling = true;
            if (fallTime > 0)
            {
                rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
                fallTime -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        ApplyMovement();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {

        MovementState state;
        animator.SetBool("InterruptAnimation", false);

        if (directionX > 0f)
        {
            
            if(!isFacingRight)
            {
                Flip();
                isFacingRight = true;
                animator.SetBool("InterruptAnimation", true);
            }
            state = MovementState.running;
   
        }
        else if (directionX < 0f)
        {
      
            if (isFacingRight)
            {
                isFacingRight = false;
                Flip();
                animator.SetBool("InterruptAnimation", true);
            }
   
            state = MovementState.running;
      
           
        }
        else
        {
            state = MovementState.idle;
            animator.SetBool("InterruptAnimation", true);
     
        }

        if (rb.velocity.y > 0.1f && !isOnSlope)
        {
            state = MovementState.jumping;
            animator.SetBool("InterruptAnimation", true);
        }

        else if (rb.velocity.y < -1.5f && !isOnSlope)
        {
            state = MovementState.falling;
            animator.SetBool("InterruptAnimation", true);
        }
   
        animator.SetInteger("state", (int)state);
    }

    private void Flip()
    {
        
        if(!isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
 
            
        }
    }
    private void SlopeCheck()
    {
        //determine position at bottom of capsule collider
        Vector2 checkPos = coll.bounds.center - (Vector3)(new Vector2(0.0f, colliderSize.y / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);

    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront /*if the raycast is colliding with terrain tag? */)
        {
            /*            float slopeFAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
                        if (slopeFAngle <= 90f)
                        {*/
           // Debug.Log(slopeSideAngle);
                isOnSlope = true;
                slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

            //}

        }

        else if (slopeHitBack)
        {
/*            float slopeBackAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            if (slopeBackAngle <= 90f)
            {*/
                isOnSlope = true;
                slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            //}

        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        //store what the vertical raycast hits
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle )
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }
        
        if (isOnSlope && directionX == 0.0f && canWalkOnSlope)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    private void CheckGround()
    {


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }


    }

    private void ApplyMovement()
    {
        if(KBCounter <= 0)
        {

        }
        if (isGrounded && !isOnSlope && !isJumping)
        {
            newVelocity.Set(moveSpeed * directionX, 0.0f);
            rb.velocity = newVelocity;
        }
        else if (isGrounded && isOnSlope && !isJumping && canWalkOnSlope)
        {
            newVelocity.Set(moveSpeed * slopeNormalPerp.x * -directionX, moveSpeed * slopeNormalPerp.y * -directionX);
            rb.velocity = newVelocity;
        }
        else if (!isGrounded)
        {
            newVelocity.Set(moveSpeed * directionX, rb.velocity.y);
            rb.velocity = newVelocity;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
