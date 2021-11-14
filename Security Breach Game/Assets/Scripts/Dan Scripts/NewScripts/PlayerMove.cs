using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 5f;

    Vector3 velocity;
    [SerializeField] private float gravity = 2f;

    private float smoothTime = 0.1f;
    private float turnSmoothVelocity;

    Vector3 moveDir;

    Vector3 lastPos;
    
    private Animator anim;

    private void Awake()
    {
        //Get animator component
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        MovePlayer(x, z);
    }

    private void MovePlayer(float x, float z)
    {
        
        
        Vector3 direction = new Vector3(x, 0, z);
        Vector3 move = direction * speed;

        move = transform.TransformDirection(move);

        

        controller.Move(move * Time.deltaTime);

        if (transform.position != lastPos)
        {
            anim.SetBool("Walk_Anim", true);
        }
        else
        {
            anim.SetBool("Walk_Anim", false);
        }
        lastPos = transform.position;

        RotatePlayer(move);

        AddGravity();
        

    }
    private void AddGravity()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotatePlayer(Vector3 move)
    {
        if(move.magnitude > 0)
        {
            //If the player is moving turn the player to face that new direction
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1.3f * Time.deltaTime);
        }
        

        
    }
}
