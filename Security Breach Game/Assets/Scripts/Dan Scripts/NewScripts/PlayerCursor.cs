using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float radius = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            transform.position = hit.point;
        }
        
    }

    private void GetBots()
    {
        LayerMask botMask = LayerMask.GetMask("Bots");
        Collider[] botsHit = Physics.OverlapSphere(gameObject.transform.position, radius, botMask);

        foreach (Collider bots in botsHit)
        {
            Debug.Log("Bots are in range");
        }
    }
}
