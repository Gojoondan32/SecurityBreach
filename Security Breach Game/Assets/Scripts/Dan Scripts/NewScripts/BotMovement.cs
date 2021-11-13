using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    private Transform target;

    private Transform cursor;

    private bool canMoveToPlayer = false;

    private bool moveToPoint = false;

    public bool botFollowingPlayer = false;

    private NavMeshAgent agent;

    Vector3 pointDirection;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        canMoveToPlayer = false;
        moveToPoint = false;
        botFollowingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoveToPlayer)
        {
            agent.SetDestination(target.position);
        }
        else if (moveToPoint && botFollowingPlayer)
        {
            Debug.Log("Bot moving to point");
            agent.SetDestination(pointDirection);
            botFollowingPlayer = false;
        }

    }

    public void CheckRadius()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Number 1 works");
            canMoveToPlayer = true;
            moveToPoint = false;
            botFollowingPlayer = true;
        }
    }

    public void PlayerMovePoint(Vector3 point)
    {
        moveToPoint = true;
        canMoveToPlayer = false;

        //Set the vector point to equal where the raycast from the mouse position intersected with the ground
        pointDirection = point;

    }
}
