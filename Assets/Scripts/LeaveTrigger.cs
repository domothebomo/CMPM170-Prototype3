using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    public GameObject camleave;
    public ItemManager itemManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = this.gameObject.transform;
            itemManager.endGame();
        }
    }
    public void PlayAnim(string animName)
    {
        camleave.SetActive(true);
        this.gameObject.GetComponentInParent<Animation>().Play(animName);
    }
}
