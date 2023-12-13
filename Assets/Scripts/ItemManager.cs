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

    public GameObject crateText;
    public GameObject boardText;
    public GameObject weaponText;

    // Track number of items collected
    //int itemsCollected = 0;
    //Dictionary<string, int> itemsCollected = new Dictionary<string, int>();
    Dictionary<string, int> shoppingList = new Dictionary<string, int>();
    Dictionary<string, int> itemsCollected = new Dictionary<string, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        shoppingList.Add("Food Crates", Random.Range(0,3));
        itemsCollected.Add("Food Crates", 0);
        shoppingList.Add("Boards", Random.Range(0, 3));
        itemsCollected.Add("Boards", 0);
        shoppingList.Add("Weapons", Random.Range(0, 3));
        itemsCollected.Add("Weapons", 0);

        crateText.GetComponent<TMP_Text>().text = "Food Crates - " + itemsCollected["Food Crates"] + "/" + shoppingList["Food Crates"];
        boardText.GetComponent<TMP_Text>().text = "Boards - " + itemsCollected["Boards"] + "/" + shoppingList["Boards"];
        weaponText.GetComponent<TMP_Text>().text = "Weapons - " + itemsCollected["Weapons"] + "/" + shoppingList["Weapons"];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void collectItem(GameObject item)
    {
        string name = item.GetComponent<Item>().itemName;
        itemsCollected[name] += 1;
        Debug.Log(name + ": " + itemsCollected[name].ToString());

        switch (name)
        {
            case "Food Crates":
                crateText.GetComponent<TMP_Text>().text = "Food Crates - " + itemsCollected["Food Crates"] + "/" + shoppingList["Food Crates"];
                break;
            case "Boards":
                boardText.GetComponent<TMP_Text>().text = "Boards - " + itemsCollected["Boards"] + "/" + shoppingList["Boards"];
                break;
            case "Weapons":
                weaponText.GetComponent<TMP_Text>().text = "Weapons - " + itemsCollected["Weapons"] + "/" + shoppingList["Weapons"];
                break;
        }
    }

    public void removeItem(GameObject item)
    {
        string name = item.GetComponent<Item>().itemName;
        itemsCollected[name] -= 1;
        Debug.Log(name + ": " + itemsCollected[name].ToString());

        switch (name)
        {
            case "Food Crates":
                crateText.GetComponent<TMP_Text>().text = "Food Crates - " + itemsCollected["Food Crates"] + "/" + shoppingList["Food Crates"];
                break;
            case "Boards":
                boardText.GetComponent<TMP_Text>().text = "Boards - " + itemsCollected["Boards"] + "/" + shoppingList["Boards"];
                break;
            case "Weapons":
                weaponText.GetComponent<TMP_Text>().text = "Weapons - " + itemsCollected["Weapons"] + "/" + shoppingList["Weapons"];
                break;
        }
    }

    public Dictionary<string, int> getShoppingList()
    {
        return shoppingList;
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
            if (itemsCollected[kvp.Key] < kvp.Value)
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
