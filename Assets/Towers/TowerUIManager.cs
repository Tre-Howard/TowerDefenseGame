using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUIManager : MonoBehaviour
{
    public GameObject towerStatsWindow;

    public TextMeshProUGUI buildingName;
    //public TextMeshProUGUI levelText;
    public Image buildingImage; // might have to add to TowerStats for the image, then send to here, or on the click Script for my tower make it look at its own sprite renderer

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackSpeedText;

    // Add other UI elements as needed

    private TowerStats currentTower;


    void Start()
    {
        towerStatsWindow.SetActive(false);
    }

    private void Update()
    {
        // Check for clicks outside the tower and close the window if needed
        if (Input.GetMouseButtonDown(0) && towerStatsWindow.activeSelf)
        {
            // Check if the mouse is over the UI
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Check if the mouse is over the selected towerPrefab
                if (currentTower != null)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    BoxCollider2D towerCollider = currentTower.GetComponent<BoxCollider2D>();

                    if (!towerCollider.OverlapPoint(mousePosition))
                    {
                        Debug.Log("Mouse clicked outside of selected tower. Closing window.");
                        towerStatsWindow.SetActive(false);
                    }
                }
            }
        }
    }

    public void CloseTowerUI()
    {
        towerStatsWindow.SetActive(false);
    }

    public void OnTowerClicked(TowerStats tower)
    {
        Debug.Log("OnTowerClicked = true!");
        currentTower = tower;
        UpdateTowerStatsUI();
        towerStatsWindow.SetActive(true);
    }

    public void OnSellButtonClicked()
    {
        // Implement sell functionality here
        // For example, you can destroy the tower GameObject
        Destroy(currentTower.gameObject);
        towerStatsWindow.SetActive(false);
    }

    public void OnUpgradeButtonClicked()
    {
        // Implement upgrade functionality here
        // You can modify the tower's stats or appearance based on the upgrade
        currentTower.damage += 10;
        UpdateTowerStatsUI();
    }

    private void UpdateTowerStatsUI()
    {
        damageText.text = currentTower.damage.ToString();
        rangeText.text = currentTower.range.ToString();
        attackSpeedText.text = currentTower.timerDelay.ToString();
        // Update other UI elements based on the tower's stats
    }
}