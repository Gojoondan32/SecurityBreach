using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstructions : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    private LayerMask groundMask;
    RaycastHit hit;

    public BotMovement botMovement;
    // Start is called before the first frame update
    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MoveBots();
        }
        */
    }

    private void MoveBots()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if(Physics.Raycast(ray, out hit, groundMask))
        {
            botMovement.PlayerMovePoint(hit.point);


            /*
            Collider[] botsHit = Physics.OverlapSphere(hit.point, range, groundMask);

            foreach (Collider bots in botsHit)
            {
                Debug.Log("Bots have been hit" + bots.name.ToString());
            }
            */
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
