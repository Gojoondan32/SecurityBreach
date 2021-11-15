using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditManager : MonoBehaviour
{
    private float startingTime = 5f;
    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime <= startingTime)
        {
            BanditIsAlive();
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if (!BanditIsAlive())
        {
            Debug.Log("You Win");
        }
    }

    private bool BanditIsAlive()
    {
        if(GameObject.FindGameObjectWithTag("Bandit") == null)
        {
            return false;
        }
        return true;
    }
}
