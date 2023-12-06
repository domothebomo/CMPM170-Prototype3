using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ItemSpawned;
    
    public GameObject SpawnAreas;

    public int NumItemsSpawned;

    List<GameObject> SpawnAreasList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in SpawnAreas.transform)
        {
            SpawnAreasList.Add(child.gameObject);
        }

        for (int i = 0; i < NumItemsSpawned; i++)
        {
            GameObject spawn = SpawnAreasList[Random.Range(0, SpawnAreasList.Count)];
            Instantiate(ItemSpawned, spawn.transform.position, Quaternion.identity);
            SpawnAreasList.Remove(spawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
