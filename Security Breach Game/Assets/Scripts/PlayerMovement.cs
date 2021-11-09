using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float gravity = 1f;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        //Get the character controller
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the player input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Call the move player function calculate the direction need to move the player
        MovePlayer(x, z);

        //Add gravity
        AddGravity();
    }

    private void MovePlayer(float x, float z)
    {
        Vector3 direction = new Vector3(x, 0, z);
        Vector3 move = direction * speed;

        controller.Move(move * Time.deltaTime);
    }

    private void AddGravity()
    {
        //Change the velocity.y value when climbing walls, for example (velocity.x)
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
