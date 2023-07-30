using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour // stats for player's towers, other scripts reference this
{
    // combat stats
    public float range;
    public float damage;

    // stats for projectiles and attack cooldown
    public float timerDelay;
    public float timeUntilNextEvent;
    public float fireForce;
    public float shellLifespan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
