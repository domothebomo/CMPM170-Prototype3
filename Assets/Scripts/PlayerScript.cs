using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController CC;
    public Camera Cam;
    public GameObject holdPos;
    public float speed = 1;
    float turnx = 0.0f;
    float turny = 0.0f;

    public bool moveRestricted = false;

    public GameObject manuscript;
    
    bool input;
    RaycastHit hit;

    GameObject heldItem = null;
    bool onDeck = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        CC = GetComponent<CharacterController>();
    }
    private void OnDisable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveRestricted)
        {
            turnx += Input.GetAxis("Mouse X") * 5;
            turny += Input.GetAxis("Mouse Y") * 5;
            turny = Mathf.Clamp(turny, -90, 90);
            if (turnx >= 360)
                turnx -= 360;
            else if (turnx <= -360)
                turnx += 360;
            transform.localRotation = Quaternion.Euler(0, turnx, 0);
            Cam.transform.localRotation = Quaternion.Euler(-turny, 0, 0);
            if (Input.GetMouseButtonDown(0))
            {
                //left click
            }
        }

        checkInput();
        if (heldItem != null)
        {
            //heldItem.transform.localPosition = Vector3.zero;
            //heldItem.transform.rotation = holdPos.transform.rotation;
            heldItem.GetComponent<Rigidbody>().AddForce(holdPos.transform.position - heldItem.transform.position, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        if (!moveRestricted)
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                direction += (speed * transform.forward) / 10;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += -(speed * transform.forward) / 10;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += -(speed * transform.right) / 10;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += (speed * transform.right) / 10;
            }
            if (!CC.isGrounded)
            {
                direction.y = -1;
            }
            CC.Move(direction);
        }
    }

    void checkInput()
    {
        input = Input.GetButtonDown("Fire1");
        if (input)
        {
            Debug.Log("interact");
            if (heldItem != null)
            {
                Debug.Log("drop item");
                dropItem();
            }
            else if (Physics.Raycast(Cam.gameObject.transform.position, Cam.gameObject.transform.forward, out hit, 2.0f))
            {
                if (hit.collider.gameObject.name.Contains("Item"))
                {
                    Debug.Log("pickup item");
                    pickUpItem(hit.collider.gameObject);
                }
            }
        }

        input = Input.GetButtonDown("Fire2");
        if (input)
        {
            if (manuscript.GetComponent<Animator>().GetBool("Open"))
            {
                manuscript.GetComponent<Animator>().SetBool("Open", false);
            }
            else
            {
                manuscript.GetComponent<Animator>().SetBool("Open", true);
            }
        }
    }

    void pickUpItem(GameObject item)
    {
        heldItem = item;
        heldItem.transform.position = holdPos.transform.position;
        heldItem.transform.rotation = holdPos.transform.rotation;
        heldItem.transform.parent = holdPos.transform;
        //heldItem.GetComponent<Rigidbody>().useGravity = false;
        //heldItem.GetComponent<BoxCollider>().enabled = false;
    }

    void dropItem()
    {
        heldItem.transform.parent = null;
        //heldItem.GetComponent<Rigidbody>().useGravity = true;
        //heldItem.GetComponent<BoxCollider>().enabled = true;
        heldItem = null;
    }

    public bool isOnDeck()
    {
        return onDeck;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("DropPoint"))
        {
            Debug.Log("enter");
            onDeck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("DropPoint"))
        {
            Debug.Log("exit");
            onDeck = false;
        }
    }
}

