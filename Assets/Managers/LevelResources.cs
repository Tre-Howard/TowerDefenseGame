using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelResources : MonoBehaviour
{
    public GameObject resourceBar;
    public TextMeshProUGUI goldDisplay;

    public int gold;
    public int mana;

    // Start is called before the first frame update
    void Start()
    {
        // Find the ResourceBar GameObject by its name
        resourceBar = GameObject.Find("ResourceBar");

        if (resourceBar != null)
        {
            // Find the child TextMeshProUGUI component named "GoldNumber"
            Transform goldNumberTransform = resourceBar.transform.Find("Panel/GoldNumber");

            if (goldNumberTransform != null)
            {
                // Get the TextMeshProUGUI component from the found transform
                goldDisplay = goldNumberTransform.GetComponent<TextMeshProUGUI>();

                // Call a method to update the displayed gold value
                UpdateGoldDisplay();
            }
            else
            {
                Debug.LogError("GoldNumber TextMeshProUGUI component not found!");
            }
        }
        else
        {
            Debug.LogError("ResourceBar GameObject not found or goldDisplay not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGoldDisplay()
    {
        goldDisplay.text = gold.ToString();
    }

    public void RemoveGold(int amount)
    {
        gold -= amount;
        UpdateGoldDisplay();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldDisplay();
    }

    public void RemoveMana(int amount)
    {
        mana -= amount;
    }

    public void AddMana(int amount)
    {
        mana += amount;
    }
}
