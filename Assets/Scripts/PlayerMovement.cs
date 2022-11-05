using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object References")]
    public CharacterController controller;
    public new Camera camera;
    public StatsManager statsManager;
    public Entity entity;
    [Space(10)]
    [Header("Movement")]
    public float gravity = -9.81f;
    public float dpi = 5; 
    public float moveSpeed = 5f;
   
    // Start is called before the first frame update
    void Start()
    {
     Cursor.lockState = CursorLockMode.Locked;
     Cursor.visible = false;
    }
    

    float _xRotation;
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        float mouseX = Input.GetAxis("Mouse X") * dpi * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * dpi * Time.deltaTime;

      
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        
        entity.rb.AddForce(0,gravity,0);
        
        controller.Move(move * (Time.deltaTime * moveSpeed * statsManager.speedMultiplier));
        controller.transform.Rotate(Vector3.up * (mouseX));
       
        Quaternion rotation = controller.transform.rotation.normalized;
        camera.transform.rotation = (Quaternion.Euler(_xRotation, rotation.eulerAngles.y, rotation.eulerAngles.z));

        
    }
}
