using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
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
        
        foreach (Collider otherBots in botsInRadius)
        {
            
            WorkerScript workerScript = otherBots.gameObject.GetComponent<WorkerScript>();

            workerScript.movingToOre = true;
            workerScript.StartCoroutine(workerScript.MovingToOre());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
