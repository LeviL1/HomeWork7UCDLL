using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float _moveSpeed;
    public float _walkSpeed = 5f;
    public float _runSpeed = 10f;
    public AudioSource jumpSound;
    private float Gravity = -9.81f; //acceleration of gravity
    [SerializeField] public float _jumpHeight = 1.0f;
    [SerializeField] public float _mouseSensitivity = 1f;
    float originalHeight;
    private float _dashDistance = 10;
    float coolDownTimer;
    [SerializeField] float coolDown = 3;

    private bool _groundedPlayer;

    private Vector3 _velocity;

    int currentJump = 0;
    [SerializeField] int maxJumps = 2;

    private CharacterController _controller; //references Character Controller component



    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        originalHeight = _controller.height;

        coolDownTimer = coolDown;
    }

    private void Update()
    {
        Movement(); //see function on line 45

        VerticalMovement();

        _velocity.y += Gravity * Time.deltaTime; //setting velocity in the y direction to the acceleration of gravity in relation to our fps (Time.deltaTime)
        _controller.Move(_velocity * Time.deltaTime); //movement based on velocity

        Crouch();
    }

    private void Movement()
    {
        _groundedPlayer = _controller.isGrounded; //was the character touching the ground during the last frame? Accessing character controller's isGrounded property
        if (_groundedPlayer && _velocity.y < 0)
        {
            _velocity.y = 0f; //if the character was grounded in the last frame and is now moving in a negative velocity (falling down), set the velocity (speed and direction) to zero
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //predefined axes in Unity linked to WASD controlls
        move = transform.TransformDirection(move); //changes direction 

        _controller.Move(move * Time.deltaTime * _moveSpeed);

        _velocity.y += Gravity * Time.deltaTime; //setting velocity in the y direction to the acceleration of gravity in relation to our fps (Time.deltaTime)
        _controller.Move(_velocity * Time.deltaTime); //movement based on velocity


        //movement options
        if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) //if the character is moving AND the left shift key is not pressed, use the walking speed
        {
            Walk(); //defined on line 76
        }
        else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) //if the character is moving and the left shift key IS pressed, use the running speed
        {
            Run(); //defined on line 82
        }
        else if (move == Vector3.zero) //if the character is not moving, stand in idle
        {
            Idle(); //defined on line 88
        }


        //Dash Functionality
        if (Input.GetKey(KeyCode.Q) && coolDownTimer <= 0f)
        {
            _controller.Move((transform.forward * _dashDistance));
            coolDownTimer = 0;
            coolDownTimer += coolDown;
        }

        //Dash cooldown
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

    }

    private void Walk()
    {
        _moveSpeed = _walkSpeed; //set my movement to walking speed
    }

    private void Run()
    {
        _moveSpeed = _runSpeed; //set my movement to running speed
    }

    private void Idle()
    {
        _moveSpeed = 0;
    }

    private void Jump()
    {
        _velocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * Gravity); //change velocity to reflect a jumping behavior
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _controller.height = originalHeight / 2;
        }
        else
        {
            _controller.height = originalHeight;
        }
    }

    private void VerticalMovement()
    {
        if (Input.GetButtonDown("Jump") && currentJump < maxJumps) //predefined jump in Unity-- paired with space bar
        {
            _velocity.y *= 0;
            Jump(); //defined on line 94
            
            currentJump++;
        }
        else if (_groundedPlayer)
        {
            currentJump = 0;
        }
    if (Input.GetButtonDown("Jump") && currentJump <= maxJumps)
    {
      jumpSound.Play();
    }
    }
}
