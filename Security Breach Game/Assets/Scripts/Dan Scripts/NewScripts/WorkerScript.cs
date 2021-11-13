using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    private Transform homeBase;

    public bool collectingOre = false;

    public bool movingToOre = false;

    private NavMeshAgent agent;

    Vector3 localOre;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        homeBase = GameObject.FindGameObjectWithTag("HomeBase").transform;
        collectingOre = false;
        movingToOre = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    

    public IEnumerator MiningOre(Vector3 ore)
    {
        localOre = ore;
        //Debug.Log("Waiting");
        yield return new WaitForSeconds(2f);

        if (collectingOre)
        {
            agent.SetDestination(homeBase.position);
            collectingOre = false;
        }
        

        
    }

    public IEnumerator MovingToOre()
    {
        yield return new WaitForSeconds(2f);
        if (movingToOre)
        {
            agent.SetDestination(localOre);
            movingToOre = false;
        }
    }
    
}
