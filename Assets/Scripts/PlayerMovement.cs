using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovement : MonoBehaviour
{
    [Header("Object References")]
    public new Camera camera;
    public StatsManager statsManager;
    public Entity entity;
    [Space(10)]
    [Header("Movement")]
    public float dpi = 100; 
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
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
        
        _controller.Move(new Vector3(0, gravity, 0));

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        
        _controller.Move(move * (Time.deltaTime * moveSpeed * statsManager.speedMultiplier));
        _controller.transform.Rotate(Vector3.up * (mouseX));
       
        Quaternion rotation = _controller.transform.rotation.normalized;
        camera.transform.rotation = (Quaternion.Euler(_xRotation, rotation.eulerAngles.y, rotation.eulerAngles.z));

    }

   
}
