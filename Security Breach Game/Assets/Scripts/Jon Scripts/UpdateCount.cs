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

    // Start is called before the first frame update
    void Start()
    {
        countWorker = GameObject.FindGameObjectsWithTag("Worker").Length;
    }

    // Update is called once per frame
    void Update()
    {
        workercount.text = ((int)countWorker).ToString();
    }
}
