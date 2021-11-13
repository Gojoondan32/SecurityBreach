using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    private LayerMask botMask;
    // Start is called before the first frame update
    void Start()
    {
        botMask = LayerMask.GetMask("Bots");
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] botsInRadius = Physics.OverlapSphere(transform.position, radius, botMask);

        foreach (Collider bots in botsInRadius)
        {
            //Debug.Log("Bots in ore radius");
            WorkerScript workerScript = bots.gameObject.GetComponent<WorkerScript>();


            workerScript.collectingOre = true;
            //workerScript.MiningOre(transform.position);
            workerScript.StartCoroutine(workerScript.MiningOre(transform.position));
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
