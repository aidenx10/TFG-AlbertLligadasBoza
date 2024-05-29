using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoScene : MonoBehaviour
{
    public float waitTime = 60f;
    public string sceneName = "Level1-1";

    private float timePassed = 0f;

    private void Start()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= waitTime)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
