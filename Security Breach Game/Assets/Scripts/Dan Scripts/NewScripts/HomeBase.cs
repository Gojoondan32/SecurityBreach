using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeBase : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    private LayerMask botMask;

    public static int totalOre = 0;

    public GameObject loose;

    // Start is called before the first frame update
    void Start()
    {
        botMask = LayerMask.GetMask("Bots");
        totalOre = 0;

        loose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        Collider[] botsInRadius = Physics.OverlapSphere(transform.position, radius, botMask);
        
        foreach (Collider otherBots in botsInRadius)
        {
            
            WorkerScript workerScript = otherBots.gameObject.GetComponent<WorkerScript>();

            if(workerScript != null)
            {
                totalOre += workerScript.currentOre;
                workerScript.currentOre = 0;
                workerScript.fullOnOre = false;

                workerScript.movingToOre = true;
                workerScript.StartCoroutine(workerScript.MovingToOre());
            }
            
        }

        HealthBar health = gameObject.GetComponent<HealthBar>();
        if(health.slider.value == 0)
        {
            loose.SetActive(true);
            StartCoroutine(Menu());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private IEnumerator Menu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
}
