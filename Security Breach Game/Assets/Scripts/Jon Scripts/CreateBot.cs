using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBot : MonoBehaviour
{

    public GameObject Worker;
    public GameObject Warrior;
    public GameObject Drone;

    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnWorker()
    {
        Instantiate(Worker, spawn);
    }
    public void spawnWarrior()
    {
        Instantiate(Warrior, spawn);
    }
    public void spawnDrone()
    {
        Instantiate(Drone, spawn);
        Debug.Log("Drone spawned");
    }

}
