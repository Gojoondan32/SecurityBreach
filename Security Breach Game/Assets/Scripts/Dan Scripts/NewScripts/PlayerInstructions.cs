using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstructions : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    private LayerMask botMask;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        botMask = LayerMask.GetMask("Bots");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CallBots();
        }
    }

    private void CallBots()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if(Physics.Raycast(ray, out hit, botMask))
        {
            
            //hit.point = Physics.OverlapSphere(hit.point, 5);
            Collider[] botsHit = Physics.OverlapSphere(hit.point, range, botMask);

            foreach (Collider bots in botsHit)
            {
                Debug.Log("Bots have been hit" + bots.name.ToString());
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(hit.point != null)
        {
            Gizmos.DrawWireSphere(hit.point, range);
        }
    }
}
