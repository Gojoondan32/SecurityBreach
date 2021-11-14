using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float radius = 3f;



    [SerializeField] private List<GameObject> botList = new List<GameObject>();

    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        //Create a raycast direction at the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        //Keep the raycast only on the specific layer
        if(Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
        {
            //Move all the bots currently following the player
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach (GameObject bots in botList)
                {
                    BotMovement newBot = bots.gameObject.GetComponent<BotMovement>();
                    newBot.PlayerMovePoint(hit.point);
                }
                
            }
            //Set the game object cursor to equal where the raycast hit
            transform.position = hit.point;
            GetBots();
        }
        
    }

    private void GetBots()
    {
        LayerMask botMask = LayerMask.GetMask("Bots");
        //Get all bots that are in an overlap sphere and add them to the array
        Collider[] botsHit = Physics.OverlapSphere(gameObject.transform.position, radius, botMask);

        //Access each of the bots in the array individually
        foreach (Collider bots in botsHit)
        {
            //Get each bots botMovement script so they can follow the player
            BotMovement botMovement = bots.gameObject.GetComponent<BotMovement>();

            botList.Add(bots.gameObject);

            botMovement.CheckRadius();
        }
    }
}
