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
        
        _controller.SimpleMove(new Vector3(0, gravity, 0));

      
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        
        _controller.Move(move * (Time.deltaTime * moveSpeed * statsManager.speedMultiplier));
        _controller.transform.Rotate(Vector3.up * (mouseX));
       
        Quaternion rotation = _controller.transform.rotation.normalized;
        camera.transform.rotation = (Quaternion.Euler(_xRotation, rotation.eulerAngles.y, rotation.eulerAngles.z));

       

    }

    bool _buttonClicked = false;
    void FixedUpdate()
    {
        if(Input.GetMouseButton(1))
        {
         if(!_buttonClicked)
            if (Physics.Raycast(camera.transform.position,  camera.transform.forward, out RaycastHit hit, 20f))
            {
                Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.yellow, 50f);
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Entity e = hit.collider.GetComponent<Entity>();
                if(e)
                {
                    e.damage(10);
                }
            }
            else
            {
                Debug.DrawRay(camera.transform.position, camera.transform.forward * 1000, Color.white, 50f);
            }
            _buttonClicked = true;
        } else
        {
            _buttonClicked = false;
        }
    }
}
