using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public static Transform target;
    [SerializeField] private float force;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 direction = target.position - transform.position;

            rb.velocity = direction * force;
            Debug.Log("Enemy located");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy has been hit... self destructing!");
        Destroy(gameObject);
    }
}
