using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Entity : MonoBehaviour
{
    [Header("Object References")]
    public Rigidbody rb;
    public StatsManager statsManager;
    public Slider healthBar;
    
    [SerializeField] public bool isPlayer;
    public Weapon weapon;

   
    void Start()
    {
        
    }

    private void Update()
    {Camera camera = Camera.allCameras[0];
        if(healthBar != null) healthBar.transform.LookAt(healthBar.transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }

    Vector3 critPoint;

    // public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
// cooles c# feature
    public bool damage(float damage)
    {
        bool dead = statsManager.damage(damage);
        if (dead)
        {
            var r = new Random();
            rb.AddForce(new Vector3(r.Next(-10,10),r.Next(-10,10),r.Next(-10,10)), ForceMode.Impulse);
            //TODO: Add particle effect
            if (isPlayer)
            {
                //TODO: Respawn player or so ( show press ALT + F + 4 to respawn)
            }
        }
      UpdateHealthBar();
        return dead;
    }
    
    public bool damageEntity(Entity entity, float damage, Vector3 hitInfoPoint)
    {
        damage *= statsManager.damageMultiplier;
        if (isCrit(hitInfoPoint)) damage *= statsManager.criticalDamageMultiplier;
        return entity.damage(damage);
    }
    
    public void heal(float heal)
    {
        statsManager.heal(heal);
        UpdateHealthBar();
    }
    
    public bool isCrit(Vector3 point)
    {
        if (.2 > Vector3.Distance(critPoint, getLocal(point)))
        {
            return true;
        }
        return false;
    }
    
    public Vector3 getLocal(Vector3 point)
    {
        return transform.position - point;
    }
    
    void UpdateHealthBar()
    {
        healthBar.value = statsManager.currentHealth / statsManager.maxHealth;
        
    }
    
}