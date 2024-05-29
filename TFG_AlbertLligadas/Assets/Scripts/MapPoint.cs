using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;

    public string levelToLoad, levelToCheck, levelName;

    public int amatistaCollected, totalAmatista;
    public float bestTime, targetTime;

    public GameObject amatistaBadge, timeBadge;

    // Start is called before the first frame update
    void Start()
    {
        if (isLevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "_amatistas"))
            {
                amatistaCollected = PlayerPrefs.GetInt(levelToLoad + "_amatistas");
            }
            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }
            if (amatistaCollected >= totalAmatista && totalAmatista != 0)
            {
                amatistaBadge.SetActive(true);
            }
            if (bestTime <= targetTime && bestTime != 0)
            {
                timeBadge.SetActive(true);
            }
            isLocked = true;
            
            if (levelToCheck != null)
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }

            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }
}
