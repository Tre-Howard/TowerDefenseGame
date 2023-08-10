using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour // script to hold all information for enemies, other scripts reference this
{
    public int experienceWorth;
    public int goldWorth;

    // send experience and gold to level resources, levelresources will send (and convert) resources to the overall game system/gameManager


    public float movementSpeed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
