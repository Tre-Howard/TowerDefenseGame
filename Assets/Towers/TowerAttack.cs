using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public enum TowerState // preview for when building (so it does not shoot), active for when building is placed and paid for
{
    Preview,
    Active
}

public class TowerAttack : MonoBehaviour // script for attacking, targeting, and applying damage
{
    public Transform projectileTransform; // variable for where to spawn projectile at on the tower

    public TowerStats towerStats; // Reference to the TowerStats script on the same GameObject
    public List<EnemyAI> enemiesInRange = new List<EnemyAI>(); // list of targets that walk in range of this tower

    public EnemyAI target; // current target from list of targets

    public GameObject projectilePrefab; // which projectile to attack with

    public bool canShoot; // used for cooldown, can or cannot shoot

    public TowerState towerState = TowerState.Preview; // starts tower in preview, is put to active in #TowerPlacementButton


    private void Start() 
    {
        towerStats = GetComponent<TowerStats>();

        // Set the CircleCollider2D radius to match the range variable in TowerStats, this is for targeting and shooting within range
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.radius = towerStats.range;
            //Debug.Log("Range of this tower is: " + circleCollider.radius);
        }
        else
        {
            Debug.LogWarning("CircleCollider2D component not found on Tower!");
        }
    }

    // Update is called once per frame
    private void Update() // as long as it is active, remove time from cooldown until ready to attack, get enemy target, rotate towards, pew pew, attack cooldown
    {
        if (towerState == TowerState.Active)
        {
            towerStats.timeUntilNextEvent -= Time.deltaTime;

            if (towerStats.timeUntilNextEvent <= 0)
            {
                canShoot = true;
            }

            GetCurrentEnemyTarget();

            if (target != null)
            {
                // Update the target position here
                Vector3 targetPos = target.transform.position;

                RotateTowardsTarget();

                if (canShoot)
                {
                    MainAttack();
                    canShoot = false; // Only attack once per event
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            AddEnemyToRange(enemy);
            Debug.Log("OnTriggerEnterAdd");
        }
    } // upon entering towers range, add enemies to target list

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit");
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            RemoveEnemyFromRange(enemy);
            Debug.Log("OnTriggerExitRemove");            
        }

        if (target == enemy)
        {
            target = null;
        }
    } // upon leaving towers range, remove enemies from target list

    private void AddEnemyToRange(EnemyAI enemy)
    {
        if (!enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    } // add enemies to target list from Enter2D function

    private void RemoveEnemyFromRange(EnemyAI enemy)
    {
        enemiesInRange.Remove(enemy);
    } // remove enemies to target list from Exit2D function

    private void GetCurrentEnemyTarget()
    {
        if (enemiesInRange.Count == 0)
        {
            Debug.Log("List is empty");
        }
        else
        {
            target = enemiesInRange[0];
        }
    } // get target to attack (currently only option is target first)

    private void RotateTowardsTarget()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    } // rotate this objects transform towards current selected target

    private void MainAttack() // main attack for tower
    {
        ShootCooldown(); // checks to see if it can fire or if its on cooldown
        if (canShoot) // if it can attack, attack
        {
            canShoot = false; // put attack on cooldown
            Debug.Log("Firing!");

            // create projectile, add damage collider script, update damage from towerStats
            GameObject _projectile = Instantiate(projectilePrefab, projectileTransform.position, Quaternion.identity) as GameObject;
            DamageOnHit doh = _projectile.GetComponent<DamageOnHit>();
            doh.damage = towerStats.damage;

            Rigidbody2D rb = _projectile.GetComponent<Rigidbody2D>();

            // get which way to shoot the projectile, apply force and move it forward, apply the Projectile layer so it goes through colliders EXCEPT for enemies, continous collision
            if (rb != null) 
            {
                // Get the direction vector from the projectile to the target
                Vector2 direction = (target.transform.position - projectileTransform.position).normalized;

                // Apply force in the direction of the target (only once)
                rb.AddForce(direction * towerStats.fireForce, ForceMode2D.Impulse);

                // Set the layer of the projectile to the "Projectiles"
                rb.gameObject.layer = LayerMask.NameToLayer("Projectiles");

                // Set projectile's collision detection to Continuous
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        }
    }


    public void ShootCooldown() // checks cooldown for attacks
    {

        if (canShoot == true && towerStats.timeUntilNextEvent <= 0)
        {
            towerStats.timeUntilNextEvent = towerStats.timerDelay; // = fireRate

        }
        else
        {
            canShoot = false;
            return;
        }
    }
}
