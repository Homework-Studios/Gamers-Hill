using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
        
    [Header("Movement Stats")]
    public float speedMultiplier = 1f;
    [Header("DamageStats")]
    public float damageMultiplier = 1f;
    public float criticalDamageMultiplier = 2f;
    [Header("Defense Stats")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    
    
    void Start()
    {
           
    }
    
    public bool damage(float damage)
    {
        if(currentHealth - damage <= 0)
        {
            currentHealth = 0;
            return true;
        }
        currentHealth -= damage;
        return false;
    }
    
    public void heal(float heal)
    {
        if(currentHealth + heal >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }

  
}