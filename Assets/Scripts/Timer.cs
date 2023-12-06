using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject ItemManager;
    
    public GameObject TimerText;
    public float TimeLimit = 60.0f;
    public bool ticking = false;

    private float timeSinceStart = 0;

    private void Start()
    {
        TimerText.SetActive(false);
        startTimer();
    }

    private void Update()
    {
        if (ticking)
        {
            timeSinceStart += Time.deltaTime;
            TimerText.GetComponent<TMP_Text>().text = "Time to Departure: " + Mathf.Ceil(TimeLimit - timeSinceStart).ToString();
            if (timeSinceStart >= TimeLimit)
            {
                ItemManager.GetComponent<ItemManager>().endGame();
                ticking = false;
            }
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("time", timeSinceStart);
        Debug.Log(PlayerPrefs.GetFloat("time", 0));
    }

    public void startTimer()
    {
        ticking = true;
        TimerText.SetActive(true);
    }
}
