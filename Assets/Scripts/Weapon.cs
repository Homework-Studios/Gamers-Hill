using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Stats")] public float damage;
    public float range = 100f;
    public float fireRate = 0.5f;
    public float reloadTime = 2f;
    public float Maxbounces = 0;
    public float maxAmmo = 6;
    [Header("Render")] public GameObject shootPoint;

    float ammo;

    public void shoot(Camera camera)
    {
        float bounces = Maxbounces;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, range))
        {
            Collider collider = hit.collider;
            if (collider != null)
            {
                Entity entity = hit.collider.GetComponent<Entity>();
                if (entity != null)
                {
                    entity.damage(damage);
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

bool _buttonClicked;

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!_buttonClicked)
            {
                shoot(Camera.allCameras[0]);
                _buttonClicked = true;
            }
        }
        else
        {
            _buttonClicked = false;
        }
    }


}