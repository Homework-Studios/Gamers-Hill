using System;
using UnityEngine;
using Random = System.Random;

public class Entity : MonoBehaviour
{
    [Header("Object References")]
    public Rigidbody rb;
    public StatsManager statsManager;
    [SerializeField] public bool isPlayer;
    void Start()
    {
        
    }

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
            else
            {
               
            }
           
        }
      
        return dead;
    }
    
    public bool damageEntity(Entity entity,float damage)
    {
        return entity.damage(damage * statsManager.damageMultiplier);
    }
    
    public void heal(float heal)
    {
        statsManager.heal(heal);
    }
}