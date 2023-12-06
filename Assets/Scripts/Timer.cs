using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject TimerText;
    public float TimeLimit = 60.0f;

    private float timeSinceStart = 0;
    private void Update()
    {
        timeSinceStart += Time.deltaTime;
        TimerText.GetComponent<TMP_Text>().text = "Time to Departure: " + Mathf.Ceil(TimeLimit - timeSinceStart).ToString();
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("time", timeSinceStart);
        Debug.Log(PlayerPrefs.GetFloat("time", 0));
    }
}
