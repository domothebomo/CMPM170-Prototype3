using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeSinceStart = 0;
    private void Update()
    {
        timeSinceStart += Time.deltaTime;
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("time", timeSinceStart);
        Debug.Log(PlayerPrefs.GetFloat("time", 0));
    }
}
