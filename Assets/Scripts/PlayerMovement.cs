using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
     Cursor.lockState = CursorLockMode.Locked;
     Cursor.visible = false;
    }
    float xRotation = 0f;
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        float mouseX = Input.GetAxis("Mouse X") * 5f;
        float mouseY = Input.GetAxis("Mouse Y") * 5f;

      
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Vector3 move = transform.right * moveX + transform.forward * moveY;

        controller.Move(move * (Time.deltaTime * 5f));
        controller.transform.Rotate(Vector3.up * (mouseX));
       
        Quaternion rotation = controller.transform.rotation;
        camera.transform.rotation = (Quaternion.Euler(xRotation, rotation.y * 100, rotation.z * 100));

        
    }
}
