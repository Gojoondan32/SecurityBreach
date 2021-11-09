using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoMachine : MonoBehaviour
{
    //This script will be attached to the nanomachines to move them
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }
}
