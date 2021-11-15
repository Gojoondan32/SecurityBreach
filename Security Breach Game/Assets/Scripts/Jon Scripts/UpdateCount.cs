using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCount : MonoBehaviour
{
    public Text workercount;
    public Text warriorcount;
    public Text dronecount;
    public Text popcount;
    public Text orecount;

    private int countWorker;
    private int countWarrior;
    private int countDrone;
    private int countPop;

    // Start is called before the first frame update
    void Start()
    {
        countWorker = GameObject.FindGameObjectsWithTag("Worker").Length;
        countWarrior = GameObject.FindGameObjectsWithTag("Warrior").Length;
        countDrone = GameObject.FindGameObjectsWithTag("Drone").Length;
        countPop = (countWorker + countWarrior + countDrone + 1);
    }

    // Update is called once per frame
    void Update()
    {
        workercount.text = ((int)countWorker).ToString();
        warriorcount.text = ((int)countWarrior).ToString();
        dronecount.text = ((int)countDrone).ToString();
        popcount.text = ((int)countPop + " /50").ToString();
        orecount.text = HomeBase.totalOre.ToString();
    }
}
