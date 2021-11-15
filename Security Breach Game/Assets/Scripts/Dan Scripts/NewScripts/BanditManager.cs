using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BanditManager : MonoBehaviour
{
    private float startingTime = 5f;
    private float currentTime = 0f;
    private float waveStartTime = 60f;
    public Text ObjectiveText;

    public GameObject waveManager;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        waveManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        waveStartTime -= Time.deltaTime;
        if(waveStartTime <= 0)
        {
            waveManager.SetActive(true);
            
        }

        currentTime += Time.deltaTime;
        if(currentTime >= startingTime)
        {
            BanditIsAlive();
            currentTime = 0f;
            
            
        }
        

        if (!BanditIsAlive())
        {
            Debug.Log("You Win");
        }

        BanditCount();
    }

    private bool BanditIsAlive()
    {
        if(GameObject.FindGameObjectWithTag("Bandit") == null)
        {
            return false;
        }
        return true;
    }

    public void BanditCount()
    {
        int BanditNumbers = GameObject.FindGameObjectsWithTag("Bandit").Length;
        ObjectiveText.text = ("Current Objective: " + (8 - BanditNumbers) + "/" + 8).ToString();
    }
}
