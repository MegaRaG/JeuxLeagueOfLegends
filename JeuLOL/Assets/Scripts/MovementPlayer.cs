using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speed;
    public float climb;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded = true;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;

    public Rigidbody2D rigidBody;

    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    private bool endedJumpEarly = true;
    private float currentHorizontalSpeed;
    private float currentVerticalSpeed;
    private float _fallSpeed;
    private float lastJumpPressed;
    private bool colDown;

    [SerializeField] private float jumpEndEarlyGravityModifier = 3;
    [SerializeField] private float jumpBuffer = 0.1f;
    [Header("GRAVITY")] [SerializeField] private float fallClamp = -40f;






    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climb * Time.fixedDeltaTime;
        

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        //if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
           // isGrounded = false;
        }
      
        

        Jump();
        JumpBuff();

    }

    void FixedUpdate()
    {

        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rigidBody.velocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, .05f);
        }

    }

    private void Jump()
    {
        var fallSpeed = endedJumpEarly && currentVerticalSpeed > 0 ? _fallSpeed * jumpEndEarlyGravityModifier : _fallSpeed;

        currentVerticalSpeed -= fallSpeed * Time.deltaTime;

        if (currentVerticalSpeed < fallClamp) currentVerticalSpeed = fallClamp;
    }

    private void JumpBuff()
    {
        if (colDown && lastJumpPressed + jumpBuffer > Time.time)
        {

        }
    }








}