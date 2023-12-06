using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject player;
    RaycastHit hit;

    public GameObject gameOverScreen;
    public GameObject gameOverText;

    public GameObject itemCountText;

    // Track number of items collected
    int itemsCollected = 0;
    //Dictionary<string, int> itemsCollected = new Dictionary<string, int>();
    Dictionary<string, int> shoppingList = new Dictionary<string, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        shoppingList.Add("crate", 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void collectItem(GameObject item)
    {
        itemsCollected++;
        itemCountText.GetComponent<TMP_Text>().text = "Items: " + itemsCollected.ToString() + "/6";
    }

    public void endGame()
    {
        gameOverScreen.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        player.GetComponent<PlayerScript>().moveRestricted = true;

        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 2.0f))
        {
            if (hit.collider.transform.parent == null || !hit.collider.transform.parent.gameObject.name.Contains("pier"))
            {
                gameOverText.GetComponent<TMP_Text>().text = "With the storm closing in, your crew left you behind...";
                return;
            }
        }

        foreach (KeyValuePair<string, int> kvp in shoppingList)
        {
            if (itemsCollected < kvp.Value)
            {
                gameOverText.GetComponent<TMP_Text>().text = "Without enough supplies, you were lost at sea...";
                return;
            }

        }

        gameOverText.GetComponent<TMP_Text>().text = "The voyage was a success!";
    }
}
