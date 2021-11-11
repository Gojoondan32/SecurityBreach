using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 5f;

    public Camera viewCamera;

    private Vector3 groundNormal = new Vector3(0.0f, 0.0f, 1.0f);

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
        Vector3 direction = gameObject.transform.forward * z + gameObject.transform.right * x;

        Vector3 move = direction * speed;

        controller.Move(move * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Climbable"))
        {
            groundNormal = collision.contacts[collision.contactCount - 1].normal;

            Quaternion rotate = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            //Gonna do alot of comparisons...
            if (groundNormal.x != 0.0f)
            {
                if (groundNormal.x < 0.0f)
                    rotate = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                else
                    rotate = Quaternion.Euler(180.0f, 180.0f, 90.0f);
            }
            else if (groundNormal.z != 0.0f)
            {
                if (groundNormal.z < 0.0f)
                    rotate = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
                else
                    rotate = Quaternion.Euler(90.0f, 180.0f, 180.0f);
            }

            gameObject.transform.rotation = rotate;
        }
    }

}