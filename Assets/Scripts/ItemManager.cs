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
    public LeaveTrigger shipLeave;
    public Timer timer;

    // Track number of items collected
    int itemsCollected = 0;
    //Dictionary<string, int> itemsCollected = new Dictionary<string, int>();
    Dictionary<string, int> shoppingList = new Dictionary<string, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        shoppingList.Add("crate", 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void collectItem(GameObject item)
    {
        itemsCollected++;
        itemCountText.GetComponent<TMP_Text>().text = "Items: " + itemsCollected.ToString() + "/" + shoppingList["crate"];
    }

    public void removeItem(GameObject item)
    {
        itemsCollected--;
        itemCountText.GetComponent<TMP_Text>().text = "Items: " + itemsCollected.ToString() + "/" + shoppingList["crate"];
    }

    public void endGame()
    {
        timer.ticking = false;
        gameOverScreen.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        player.GetComponent<PlayerScript>().moveRestricted = true;

        if (!player.GetComponent<PlayerScript>().isOnDeck()) 
        {
            // Ship leaving you anim
            gameOverText.GetComponent<TMP_Text>().text = "With the storm closing in, your crew left you behind...";
            shipLeave.PlayAnim("ShipSinking");
            return;
        }

        foreach (KeyValuePair<string, int> kvp in shoppingList)
        {
            if (itemsCollected < kvp.Value)
            {
                // Ship sinks anim
                gameOverText.GetComponent<TMP_Text>().text = "Without enough supplies, you were lost at sea...";
                shipLeave.PlayAnim("ShipSinking");
                return;
            }

        }

        // Ship sails off anim
        gameOverText.GetComponent<TMP_Text>().text = "The voyage was a success!";
        shipLeave.PlayAnim("ShipLeave");
    }
}
