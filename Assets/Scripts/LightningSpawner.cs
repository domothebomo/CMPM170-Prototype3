using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightningSpawner : MonoBehaviour
{
    public GameObject LightningStrikes;
    public float timeBeforeStart = 45;
    public float timeBetweenSpawning = 10;
    public GameObject SpawnAreas;
    private Transform[] spawns;
    private Transform lastSpawnArea;
    private void Start()
    {
        StartCoroutine(SpawnLightning());
        spawns = SpawnAreas.GetComponentsInChildren<Transform>();
        spawns[0] = spawns[1];
    }
    private IEnumerator SpawnLightning()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        while (true)
        {
            Transform newSpawnArea = lastSpawnArea;
            while (newSpawnArea == lastSpawnArea)
            {
                newSpawnArea = spawns[Random.Range(0, spawns.Length)];
            }
            lastSpawnArea = newSpawnArea;
            Instantiate(LightningStrikes, newSpawnArea);
            yield return new WaitForSeconds(timeBetweenSpawning);
        }

    }
}
