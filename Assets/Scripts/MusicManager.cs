using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource Ba;
    public AudioSource Bum;
    public Timer time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(0.1f);
        while (time.ticking)
        {
            float timeBetween = (time.TimeLimit - time.timeSinceStart) / 10;
            if (timeBetween < 0.1f) { timeBetween = 0.1f; }
            Ba.Play();
            yield return new WaitForSeconds(timeBetween);
            timeBetween = (time.TimeLimit - time.timeSinceStart) / 10;
            if (timeBetween < 0.1f) { timeBetween = 0.1f; }
            Bum.Play();
            yield return new WaitForSeconds(timeBetween);
        }
    }
}
