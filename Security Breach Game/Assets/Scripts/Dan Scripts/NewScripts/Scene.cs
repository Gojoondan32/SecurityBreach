using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    string scene;
    public void ChangeScene(string scene)
    {
        switch (scene)
        {
            case "Play":
                SceneManager.LoadScene("MainScene");
                break;
        }
    }
}
