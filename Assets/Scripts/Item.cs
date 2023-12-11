using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject ItemManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("DropPoint"))
        {
            ItemManager.GetComponent<ItemManager>().collectItem(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("DropPoint"))
        {
            ItemManager.GetComponent<ItemManager>().removeItem(gameObject);
        }
    }
}
