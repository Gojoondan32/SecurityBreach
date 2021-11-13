﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float radius = 3f;

    public BotMovement botMovement1;
    

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                botMovement1.PlayerMovePoint(hit.point);
            }
            
            transform.position = hit.point;
            GetBots();
        }
        
    }

    private void GetBots()
    {
        LayerMask botMask = LayerMask.GetMask("Bots");
        Collider[] botsHit = Physics.OverlapSphere(gameObject.transform.position, radius, botMask);

        foreach (Collider bots in botsHit)
        {
            //Debug.Log("Bots are in range");
            BotMovement botMovement = bots.gameObject.GetComponent<BotMovement>();

            botMovement.CheckRadius();
        }
    }
}
