using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour // script to control health on enemies (later will add shield/armor/etc on top of health)
{
    [SerializeField] private EnemyStats enemyStats; // grabs enemy stats on object
    [SerializeField] private Image healthBar; // reference to which health bar to update

    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();

        maxHealth = maxHealth + enemyStats.bonusHealth; // on spawn, update max health and current health based on baseStats and enemyStats
        currentHealth = currentHealth + enemyStats.bonusHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // used for testing
        {
            DealDamage(5);
            Debug.Log("P pressed");
        }
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, Time.deltaTime * 10f); // when damaged, update healthbar image over time
    }

    public void DealDamage(float damageReceived) // takes damage based on #DamageOnHit script from projectiles
    {
        currentHealth -= damageReceived;
        if (currentHealth <= 0) // if <= 0, destroy object
        {
            currentHealth = 0;
            Die();
        }
        //else
        //OnEnemyHit?.Invoke(_enemy);
    }

    private void Die() // destroy object, will add FX/sound later
    {
        Debug.Log("This monster died");
        Destroy(this.gameObject);
    }

}
