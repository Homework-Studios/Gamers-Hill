using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Stats")] public float damage;
    public float range = 100f;
    public float fireRate = 0.5f;
    public float reloadTime = 2f;
    public float maxBounces;
    public float maxAmmo = 6;

    [Header("Render")] public GameObject shootPoint;

    public Entity owner;

    public Quaternion forward;


    private float _ammo;

    private void Start()
    {
        owner = Camera.allCameras[0].GetComponentInParent<Camera>().GetComponentInParent<Entity>();
        owner.weapon = this;
        forward = owner.weapon.transform.rotation;
    } // ReSharper disable Unity.PerformanceAnalysis
    public void Shoot(Transform transform)
    {
        //TODO: screenshake

        var bounces = maxBounces;
        if (Physics.Raycast(transform.position, transform.forward, out var hit, range))
        {
            var hitCollider = hit.collider;
            if (hitCollider != null)
            {
                var entity = hitCollider.GetComponent<Entity>();
                if (entity != null)
                {
                    owner.damageEntity(entity, damage, hit.point);
                    bounces = 0;
                }
            }

            Debug.DrawLine(shootPoint.transform.position, hit.point, Color.yellow, 10f);

            var direction = transform.forward;
            for (var i = 0; i < bounces; i++)
                if (hit.collider != null)
                {
                    var hit2 = hit;
                    direction = Vector3.Reflect(direction, hit2.normal);
                    if (Physics.Raycast(hit.point, direction, out hit,
                            range))
                    {
                        Debug.DrawLine(hit2.point, hit.point, Color.red, 10f);
                        if (hit.collider != null)
                        {
                            var entity = hit.collider.GetComponent<Entity>();
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

    private bool _pickedUp;

    public void TogglePickup()
    {
        if (_pickedUp)
            PickUp();
        else
            Drop();
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


    private void Update()
    {
        var camera = Camera.allCameras[0];
        // Raycast and rotate towards the hit point //TODO: convert to be usable with enemies
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hit, range))
        {
            // Rotate towards the hit point but take into account that the weapon has default rotation
            // and use a lerped rotation to make it smooth
            var weaponPointer = transform.GetChild(0);
            transform.rotation = Quaternion.Lerp(weaponPointer.transform.rotation,
                Quaternion.LookRotation(hit.point - weaponPointer.transform.position) * forward, 0.01f);
        }
    }
}