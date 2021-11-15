using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    private Transform homeBase;

    public bool collectingOre = false;

    public bool movingToOre = false;

    public bool fullOnOre = false;

    private NavMeshAgent agent;

    Vector3 localOre;

    Vector3 previousPos;

    public int currentOre = 0;
    public int maxOreAllowed = 5;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        homeBase = GameObject.FindGameObjectWithTag("HomeBase").transform;
        collectingOre = false;
        movingToOre = false;
        fullOnOre = false;

        
    }



    public IEnumerator MiningOre(Vector3 ore)
    {
        localOre = ore;
        //Debug.Log("Waiting");
        yield return new WaitForSeconds(2f);

        if (collectingOre)
        {
            //Move to the home base
            agent.SetDestination(homeBase.position);
            collectingOre = false;
        }

    }

    public IEnumerator MovingToOre()
    {
        yield return new WaitForSeconds(2f);
        if (movingToOre)
        {
            //Move to the previous ore location
            agent.SetDestination(localOre);
            movingToOre = false;
        }

    }
    
}
