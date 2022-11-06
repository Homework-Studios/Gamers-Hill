using System;
using UnityEngine;

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
            rb.AddForce(0,20,0, ForceMode.Impulse);
            //TODO: Add particle effect
            if (isPlayer)
            {
                //TODO: Respawn player or so ( show press ALT + F + 4 to respawn)
            }
            else
            {
                Destroy(transform.parent.gameObject);
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