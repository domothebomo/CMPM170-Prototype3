using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> ItemTable;
    
    public GameObject SpawnAreas;

    public int NumItemsSpawned;

    public GameObject itemManager;

    List<GameObject> SpawnAreasList = new List<GameObject>();
    Dictionary<string, int> itemsSpawned = new Dictionary<string, int>();
    Dictionary<string, GameObject> itemObjects = new Dictionary<string, GameObject>();
    bool extraSpawns = false;
    
    // Start is called before the first frame update
    void Start()
    {
        itemObjects.Add("Food Crates", ItemTable[0]);
        itemObjects.Add("Boards", ItemTable[1]);
        itemObjects.Add("Weapons", ItemTable[2]);

        foreach (Transform child in SpawnAreas.transform)
        {
            SpawnAreasList.Add(child.gameObject);
        }

        itemsSpawned.Add("Food Crates", 0);
        itemsSpawned.Add("Boards", 0);
        itemsSpawned.Add("Weapons", 0);

        for (int i = 0; i < NumItemsSpawned; i++)
        {
            spawnItem(ItemTable[Random.Range(0, ItemTable.Count)]);
        }

    }

    void spawnItem(GameObject item)
    {
        GameObject spawn = SpawnAreasList[Random.Range(0, SpawnAreasList.Count)];
        Instantiate(item, spawn.transform.position, Quaternion.identity);
        SpawnAreasList.Remove(spawn);
        itemsSpawned[item.GetComponent<Item>().itemName] += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!extraSpawns)
        {
            foreach (KeyValuePair<string, int> kvp in itemManager.GetComponent<ItemManager>().getShoppingList())
            {
                while (kvp.Value > itemsSpawned[kvp.Key])
                {

                    spawnItem(itemObjects[kvp.Key]);
                }
            }
            extraSpawns = true;
        }
    }
}
