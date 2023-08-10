using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour // script to apply damage to target this projectile collided with
{
    public float damage; // how much damage to apply
    public float lifespan = 1.5f; // lifespan, just in case the projectile misses so it destroys itself

    private bool hasDealtDamage = false; // Flag to track whether damage has been dealt
    // REMOVE THE ABOVE IF I AM MAKING AN AOE ATTACK, this is for SINGLE TARGET ONLY

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan); // once created, destroy self in lifespan time
    }

    private void OnTriggerEnter2D(Collider2D other) // when colliding with another 2d object, get that enemyhealth component and remove health = to damage, then destroy self immediately
    {
        if (hasDealtDamage) return;

        EnemyHealth otherHealth = other.gameObject.GetComponent<EnemyHealth>();

        if (otherHealth != null) 
        {
            otherHealth.DealDamage(damage);
            hasDealtDamage = true; // Set the flag to true after dealing damage
        }

        Destroy(gameObject);
    }
}