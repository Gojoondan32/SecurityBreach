using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalOre : MonoBehaviour
{
    private int metalCount;
    public int metalTaken;

    private void Start()
    {
        if (gameObject.name == "MetalOre")
        {
            metalCount = 20;
        }
        else if(gameObject.name == "Large Metal Ore")
        {
            metalCount = 40;
        }
    }

    private void FixedUpdate()
    {
        if (metalTaken >= metalCount)
        {
            Destroy(gameObject);
        }
    }
}
