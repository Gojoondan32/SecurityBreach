using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private GameObject homeBase;

    private Animator anim;

    private NavMeshAgent agent;

    public GameObject sword;

    private bool isAttacking = false;
    private float swingCooldown = 0.0f;

    private GameObject target;

    private LayerMask botMask;

    private BotMovement botMove;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        botMove = GetComponent<BotMovement>();
        homeBase = GameObject.FindGameObjectWithTag("HomeBase");

        botMask = LayerMask.GetMask("Bots");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPos = homeBase.transform.position;

        target = getTarget();
        if (target != null)
        {

            targetPos = target.transform.position;

        }
        else
        {
            
            target = homeBase;
        }

        if ((targetPos - gameObject.transform.position).magnitude <= (target.CompareTag("Drone") ? 6.0f : 3.0f))
        {
            //Close enough to start attacking!
            if (swingCooldown <= 0.0f)
            {
                isAttacking = true;
                anim.SetBool("isAttacking", true);
                swingCooldown = 1.0f;
                //Begin swing animation.
                sword.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                Invoke("StopSwing", 0.25f);

                HealthBar healthScript = target.GetComponent<HealthBar>();
                if (healthScript != null)
                {
                    healthScript.SetHealth(1);
                }

            }
            else if (!isAttacking)
            {
                swingCooldown -= Time.deltaTime;
            }

        }

        agent.SetDestination(targetPos);

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
        float priorityLevel = 0;

        Collider[] botsInRadius = Physics.OverlapSphere(transform.position, closest, botMask);
        foreach (Collider bots in botsInRadius)
        {

            HealthBar healthScript = bots.gameObject.GetComponent<HealthBar>();
            if (healthScript == null)
                continue;
            
            float distance = (bots.gameObject.transform.position - gameObject.transform.position).magnitude;
            if (distance < closest || healthScript.priorityLevel > priorityLevel)
            {
                closest = distance;
                priorityLevel = healthScript.priorityLevel;
                victim = bots.gameObject;
            }

        }

        return victim;

    }
}
