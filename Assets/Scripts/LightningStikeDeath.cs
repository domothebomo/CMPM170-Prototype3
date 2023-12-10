using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightningStikeDeath : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverScreen;
    public GameObject gameOverText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameOverScreen.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            player.GetComponent<PlayerScript>().moveRestricted = true;
            gameOverText.GetComponent<TMP_Text>().text = "You were hit by lightning. You died.";
        }
    }
}
