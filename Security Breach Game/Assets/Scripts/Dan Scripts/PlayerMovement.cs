using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float gravity = 1f;

    [SerializeField] private float wallCheckDistance = 1f;

    [SerializeField] private bool isOnGround;

    [SerializeField] private bool isOnWall;

    private Vector3 velocity;

    private Vector3 move;

    [SerializeField] private bool onFirstWall;
    [SerializeField] private bool onSecondWall;
    [SerializeField] private bool onThirdWall;
    [SerializeField] private bool onFourthWall;


    private float smoothTime = 0.1f;
    private float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        //Get the character controller
        controller = GetComponent<CharacterController>();

        isOnGround = true;
        isOnWall = false;

        onFirstWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the player input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Call the move player function calculate the direction need to move the player
        MovePlayer(x, z);

        //Apply gravity
        AddGravity();

    }

    private void MovePlayer(float x, float z)
    {
        Vector3 groundDirection = new Vector3(x, 0, z);
        Vector3 firstWallDirection = new Vector3(x, z, 0);
        Vector3 secondWallDirection = new Vector3(0, z, -x);
        Vector3 thirdWallDirection = new Vector3(0, z, x);
        Vector3 fourthWallDirection = new Vector3(-x, z, 0);

        move = groundDirection * speed;

        /*
        //Rotating the player character
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //------------------
        */

        //ChangeWall(move, wallDirection);
        
        if (isOnGround)
        {
            controller.Move(move * Time.deltaTime);
        }
        else if (isOnWall)
        {
            //controller.Move(secondWallDirection * speed * Time.deltaTime);
        }

        else if (onFirstWall)
        {
            controller.Move(firstWallDirection * speed * Time.deltaTime);
        }
        else if (onSecondWall)
        {
            controller.Move(secondWallDirection * speed * Time.deltaTime);
        }
        else if (onThirdWall)
        {
            controller.Move(thirdWallDirection * speed * Time.deltaTime);
        }
        else if (onFourthWall)
        {
            controller.Move(fourthWallDirection * speed * Time.deltaTime); 
        }
    }


    public void AddGravity()
    {
        //Change the velocity.y to change the focus of the gravity
        if (isOnGround)
        {
            velocity.y -= gravity * Time.deltaTime;
            velocity.z = 0;
            velocity.x = 0;
        }
        else if (isOnWall)
        {
            
            //Used for first wall
            //velocity.z += gravity * Time.deltaTime;

            //Used for second wall
            //velocity.x += gravity * Time.deltaTime;

            //Used for third wall
            //velocity.x -= gravity * Time.deltaTime;

            velocity.y = 0;
        }
        else if (onFirstWall)
        {
            //Used for first wall
            velocity.z += gravity * Time.deltaTime;
            velocity.y = 0;
            velocity.x = 0;
        }
        else if (onSecondWall)
        {
            //Used for second wall
            velocity.x += gravity * Time.deltaTime;
            velocity.y = 0;
            velocity.z = 0;
        }
        else if (onThirdWall)
        {
            //Used for third wall
            velocity.x -= gravity * Time.deltaTime;
            velocity.y = 0;
            velocity.z = 0;
        }
        else if (onFourthWall)
        {
            velocity.z -= gravity;
            velocity.x = 0;
            velocity.y = 0;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    
    public void ChangeWall(Vector3 move, Vector3 wallDirection)
    {
        if (isOnGround)
        {
            LayerMask wallMask = LayerMask.GetMask("Wall");
            if(Physics.Raycast(transform.position, move.normalized, wallCheckDistance, wallMask))
            {
                isOnGround = false;
                isOnWall = true;
                //Rotate the player 
                transform.rotation = Quaternion.Euler(270f, 0f, 0f);
                Debug.Log("Player has hit a wall");
            }
        }
        else if (isOnWall)
        {
            LayerMask groundMask = LayerMask.GetMask("Ground");
            
            if (Physics.Raycast(transform.position, wallDirection.normalized, wallCheckDistance, groundMask))
            {
                isOnGround = true;
                isOnWall = false;
                //Rotate the player 
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                Debug.Log("Player has hit the floor");
                Debug.DrawLine(transform.position, move, Color.red);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Used to run on walls 
        if(hit.normal.y < 0.1f)
        {
            if (hit.gameObject.tag == "Wall1")
            {
                onFirstWall = true;
                isOnGround = false;
                isOnWall = false;
                onThirdWall = false;
                onFourthWall = false;

                Debug.Log("On first wall");
            }
            else if (hit.gameObject.tag == "Wall2")
            {
                onFirstWall = false;
                isOnGround = false;
                onSecondWall = true;
                onThirdWall = false;
                onFourthWall = false;
            }
            else if (hit.gameObject.tag == "Wall3")
            {
                onFirstWall = false;
                onSecondWall = false;
                isOnGround = false;
                onThirdWall = true;
                onFourthWall = false;
            }
            else if (hit.gameObject.tag == "Wall4")
            {
                onFirstWall = false;
                onSecondWall = false;
                onThirdWall = false;
                isOnGround = false;
                onFourthWall = true;
            }
            
            isOnGround = false;
            //Debug.Log("Normal created from the wall");
            //Debug.DrawRay(hit.point, hit.normal, Color.green, 1.25f);
        }
        //Used to return back to the ground
        else if (hit.normal.y > 0.1f)
        {
            isOnGround = true;
            
            onFirstWall = false;
            onSecondWall = false;
            onThirdWall = false;
            onFourthWall = false;
        }
    }
}
