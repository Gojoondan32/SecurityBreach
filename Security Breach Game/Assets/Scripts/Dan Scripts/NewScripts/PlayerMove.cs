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

        //RotatePlayer(move);

        AddGravity();
        

    }
    private void AddGravity()
    {
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 RotatePlayer(Vector3 move)
    {
        float targetAngle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTime);

        transform.rotation = Quaternion.Euler(0, angle, 0);

        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        Debug.Log("Rotating the player");

        return moveDir;
    }
}
