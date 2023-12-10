using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    public GameObject camleave;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = this.gameObject.transform;
            other.gameObject.GetComponent<PlayerScript>().enabled = false;
            camleave.SetActive(true);
            this.gameObject.GetComponentInParent<Animation>().Play("ShipLeave");
        }
    }
}
