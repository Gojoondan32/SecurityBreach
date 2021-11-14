using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [System.Serializable]
    public class OreStats
    {
        public string name;
        public int totalOre;
        
    }

    [SerializeField] private float radius = 3f;
    private LayerMask botMask;

    public OreStats oreStats;

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

            if (workerScript == null)
                continue;

            workerScript.collectingOre = true;

            if (!workerScript.fullOnOre)
            {
                oreStats.totalOre -= workerScript.maxOreAllowed;
                workerScript.currentOre = workerScript.maxOreAllowed;
                workerScript.fullOnOre = true;
            }

            workerScript.StartCoroutine(workerScript.MiningOre(transform.position));

            if(oreStats.totalOre <= 0)
            {
                oreStats.totalOre = 0;
                Destroy(gameObject, 2f);
            }

            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
