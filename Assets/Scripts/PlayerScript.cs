using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController CC;
    public Camera Cam;
    public float speed = 1;
    float turnx = 0.0f;
    float turny = 0.0f;

    public bool moveRestricted = false;
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
                //right click
            }
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
}

