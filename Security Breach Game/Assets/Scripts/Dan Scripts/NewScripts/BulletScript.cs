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
            Vector3 direction = (target.position - transform.position).normalized;

            rb.velocity = direction * force;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the bullets hits an object on the enemy layer
        if(collision.gameObject.layer == 13)
        {
            BotHealth enemyHealth = collision.gameObject.GetComponent<BotHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.Health--;
            }
        }


        Debug.Log("Enemy has been hit... self destructing!");
        Destroy(gameObject);
    }
}
