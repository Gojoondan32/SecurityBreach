using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    private Transform homeBase;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        homeBase = GameObject.FindGameObjectWithTag("HomeBase").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
