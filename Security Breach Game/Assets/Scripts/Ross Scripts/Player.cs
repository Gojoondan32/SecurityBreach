using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 5f;

    private int isClimbing = 0;
    public Camera viewCamera;

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

        MovePlayer(x, z);

        if (isClimbing > 0)
            controller.Move(new Vector3(0.0f, -9.8f, 0.0f) * Time.deltaTime);

        //Re-adjusting camera automatically.
        Vector3 cameraOffset = new Vector3(0.0f, 10.0f, 0.0f);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(gameObject.transform.position, cameraOffset.normalized, out hit, 10.0f))
        {
            cameraOffset = hit.point - gameObject.transform.position;
        }

        viewCamera.transform.position = gameObject.transform.position + cameraOffset;
    }

    private void MovePlayer(float x, float z)
    {
        Vector3 direction = new Vector3(x, 0, z);

        Debug.Log(isClimbing);
        if (isClimbing > 0)
            direction += new Vector3(0.0f, (x != 0.0f ? x : (z != 0.0f ? z : 0.0f)), 0.0f);

        Vector3 move = direction * speed;

        controller.Move(move * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Climbable"))
            isClimbing++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Climbable"))
            isClimbing--;
    }
}