using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour // stats for player's towers, other scripts reference this
{
    // default
    //public string name; 
    public bool onClick;

    // cost
    public int goldCost;
    public int manaCost;

    // combat stats
    public float damage; // normal damage, applies to all shields before health
    public float armorDamage; // armor damage, applies to armor and health ONLY, ignores shield
    public float shieldDamage;  //shield damage, applies to shield and health ONLY, ignores armor
    public float criticalChance; // chance to make a critical strike (bonus damage)
    public float criticalDamage; // default 2x normal damage, this increases it higher
    public float multiHit; // chance to attack again on the same hit, may take out

    public float range; // how far it can shoot

    public float timeUntilNextEvent; // countdown timer to attack again, aprt of attackSpeed
    public float timerDelay; // attackSpeed

    // stats for projectiles and attack cooldown
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


    void OnMouseDown()
    {
        TowerUIManager uiManager = FindObjectOfType<TowerUIManager>();
        Debug.Log(onClick);

        if (onClick && uiManager != null)
        {
            uiManager.OnTowerClicked(this);
        }
        else
        {
            onClick = true;
        }
    }
}
