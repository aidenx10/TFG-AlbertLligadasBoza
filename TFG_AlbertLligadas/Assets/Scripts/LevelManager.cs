using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;

    public int amatistaCollected;
    public float timeInLevel;

    public string levelToLoad;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn - (1f/UIController.instance.fadeSpeed));

        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .2f);
        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        UIController.instance.UpdateHealthDisplay();
    }

    public void EndLevel(string levelLoading)
    {
        StartCoroutine(EndLevelCo(levelLoading));
    }

    public IEnumerator EndLevelCo(string levelLoading)
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;

        CameraController.instance.stopFollow = true;

        UIController.instance.levelCompletedText.SetActive(true);

        yield return new WaitForSeconds(2f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((6f / UIController.instance.fadeSpeed) + .25f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_amatistas"))
        {
            if (amatistaCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_amatistas", amatistaCollected))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_amatistas", amatistaCollected);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_amatistas", amatistaCollected);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
        
        SceneManager.LoadScene(levelLoading);
    }
}
