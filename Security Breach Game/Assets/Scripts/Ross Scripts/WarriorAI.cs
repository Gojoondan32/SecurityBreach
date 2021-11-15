using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : MonoBehaviour
{

    private Transform homeBase;

    private Animator anim;

    private NavMeshAgent agent;

    public GameObject sword;

    private bool isAttacking = false;
    private float swingCooldown = 0.0f;

    private GameObject target;

    private LayerMask botMask;

    private BotMovement botMove;

    Vector3 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        botMove = GetComponent<BotMovement>();
        homeBase = GameObject.FindGameObjectWithTag("HomeBase").transform;

        botMask = LayerMask.GetMask("EnemyBots");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position == previousPos)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }

        previousPos = transform.position;

        
        if (!botMove.botFollowingPlayer)
        {
            target = getTarget();
            if (target != null)
            {

                Vector3 targetPos = target.transform.position;

                agent.SetDestination(targetPos);

                if ((targetPos - gameObject.transform.position).magnitude <= 2.0f)
                {
                    //Close enough to start attacking!
                    if (swingCooldown <= 0.0f)
                    {
                        isAttacking = true;
                        anim.SetBool("isAttacking", true);
                        swingCooldown = 1.0f;
                        //Begin swing animation.
                        sword.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                        Invoke("StopSwing", 0.5f);
                        

                        //Do damage to the target!
                        BotHealth healthScript = target.GetComponent<BotHealth>();
                        if(healthScript != null)
                        {
                            healthScript.Health--;
                        }

                    }
                    else if (!isAttacking)
                    {
                        swingCooldown -= Time.deltaTime;
                        
                    }

                }

            }

        }

    }

    private void StopSwing()
    {
        sword.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    private GameObject getTarget()
    {

        GameObject victim = null;
        float closest = 8.0f;

        Collider[] botsInRadius = Physics.OverlapSphere(transform.position, closest, botMask);
        foreach (Collider bots in botsInRadius)
        {

            float distance = (bots.gameObject.transform.position - gameObject.transform.position).magnitude;
            if(distance < closest)
            {
                closest = distance;
                victim = bots.gameObject;
            }

        }

        return victim;

    }

}
