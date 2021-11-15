using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHealth : MonoBehaviour
{

    public int Health = 3;
    public int priorityLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Health <= 0)
        {
            gameObject.SetActive(false); //It is now dead.
        }

    }
}
