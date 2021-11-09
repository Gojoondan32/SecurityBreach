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
    // Start is called before the first frame update
    void Start()
    {
        //Get the character controller
        controller = GetComponent<CharacterController>();

        isOnGround = true;
        isOnWall = false;
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
        Vector3 direction = new Vector3(x, 0, z);
        Vector3 wallDirection = new Vector3(x, z, 0);

        move = direction * speed;

        ChangeWall(move);
        if (isOnGround)
        {
            controller.Move(move * Time.deltaTime);
        }
        else if (isOnWall)
        {
            controller.Move(wallDirection * speed * Time.deltaTime);
        }
    }


    public void AddGravity()
    {
        //Change the velocity.y to change the focus of the gravity
        if (isOnGround)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else if (isOnWall)
        {
            velocity.z += gravity * Time.deltaTime;
            velocity.y = 0;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    
    public void ChangeWall(Vector3 move)
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
    }
}
