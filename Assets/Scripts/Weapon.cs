using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [Header("Stats")] public float damage;
    public float range = 100f;
    public float fireRate = 0.5f;
    public float reloadTime = 2f; 
    public float maxBounces = 0;
    public float maxAmmo = 6;
    [Header("Render")]
    public GameObject shootPoint;
    public Entity owner;
        
    
    float _ammo;

    private void Start()
    {
        owner = Camera.allCameras[0].GetComponentInParent<Camera>().GetComponentInParent<Entity>();
        owner.weapon = this;
    }

    public void Shoot(Camera camera)
    {
       //TODO: screenshake
        
        float bounces = maxBounces;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, range))
        {
            
            // Debug the raycast
            Debug.DrawRay(camera.transform.position, camera.transform.forward * hit.distance, Color.yellow);
            
            Collider collider = hit.collider;
            if (collider != null)
            {
                Entity entity = hit.collider.GetComponent<Entity>();
                if (entity != null)
                {
                    owner.damageEntity(entity, damage, hit.point);
                    bounces = 0;
                }
            }
            Debug.DrawLine(shootPoint.transform.position, hit.point, Color.yellow, 10f);
            
            Vector3 direction = camera.transform.forward;
                for (int i = 0; i < bounces; i++)
                {
                    if (hit.collider != null)
                    {
                        RaycastHit hit2 = hit;
                        direction = Vector3.Reflect(direction, hit2.normal);
                        if (Physics.Raycast(hit.point, direction, out hit,
                                range))
                        {
                            
                        
                        Debug.DrawLine(hit2.point, hit.point, Color.red, 10f);
                        if (hit.collider != null)
                        {

                            Entity entity = hit.collider.GetComponent<Entity>();
                            if (entity != null)
                            {
                                entity.damage(damage);
                                bounces = 0;
                            }
                        }
                        }
                    }
                }

        }
    }
    
    bool _pickedUp;

    public void TogglePickup()
    {
        if (_pickedUp)
        {
            PickUp();
        }
        else
        {
            Drop();
        }
    }
    
    public void PickUp()
    {
        _pickedUp = true;
        transform.parent = Camera.allCameras[0].transform;
        transform.localPosition = new Vector3(0.5f, -0.5f, 1f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void Drop()
    {
        _pickedUp = false;
        transform.parent = null;
    }
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot(Camera.allCameras[0]);
         
        }
    }


}