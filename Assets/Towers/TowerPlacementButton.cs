using UnityEngine.Tilemaps;
using UnityEngine;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class TowerPlacementButton : MonoBehaviour // when pressing a menu button during gameplay, makes a preview of the tower and allows player to place onto "Buildable" tiles
{
    public GameObject towerPrefab; // Reference to the Tower prefab you created

    public Color validColor = Color.green; // variable for color when in preview/build mode - will change to images of prefab instead of color later
    public Color invalidColor = Color.red; // variable for color when in preview/build mode - will change to images of prefab instead of color later

    private bool placingTower = false; // changes if your placing towers or not
    private Tilemap buildableTilemap; // tile variable for player to build on
    private Tilemap enemyPathTilemap; // tile variable for enemies to walk on / not walkable

    private TowerStats towerStats;
    private TowerAttack towerAttack; // Reference to the TowerAttack script on the preview tower
    private GameObject towerPreview; // prefab object when placing a tower before left clicking

    private LevelResources levelResources;

    private void Start()
    {
        buildableTilemap = GameObject.Find("Buildable").GetComponent<Tilemap>();
        enemyPathTilemap = GameObject.Find("EnemyPath").GetComponent<Tilemap>();
        levelResources = GameObject.Find("LevelManager").GetComponent<LevelResources>(); // need to change code below to check for gold
        towerStats = towerPrefab.GetComponent<TowerStats>();

        Debug.Log("Gold: " + levelResources.gold);
    }

    private void Update()
    {
        if (placingTower && Input.GetMouseButtonDown(0)) // checks if you are in building mode and if you press left click on mouse, places tower
        {
            PlaceTower();
        }

        // follow the mouse with the preview tower if we are placing the tower
        if (placingTower)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // gets position of players mouse
            Vector3Int gridPosition = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0); // gets closest grid to mouse position
            towerPreview.transform.position = gridPosition + new Vector3(0.5f, 0.5f, 0); // snaps preview tower to grid near mouse location
        }
    }

    private void PlaceTower() // creates the tower, destroys the preview tower, places one tower down only, sets to active
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0);

        if (buildableTilemap.HasTile(gridPosition))
        {            
            if (levelResources.gold >= towerStats.goldCost)
            {
                levelResources.gold -= towerStats.goldCost;
                levelResources.UpdateGoldDisplay();

                // Tower can be placed here since it's on the "Buildable" tilemap
                GameObject newTower = Instantiate(towerPrefab, gridPosition + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                TowerAttack towerAttackScript = newTower.GetComponent<TowerAttack>();
                TowerStats towerStatsScript = newTower.GetComponent<TowerStats>();

                // Set the state of the new tower to "Active" and destroy the towerPreview
                towerAttackScript.towerState = TowerState.Active;
                towerStatsScript.onClick = true;
                Destroy(towerPreview);

                // Nullify the references to the tower preview
                towerPreview = null;
                towerAttack = null;
                CancelTowerPlacement(); // this may just be double code for the same shit

                Debug.Log(levelResources.gold);
            }
            else
            {
                Debug.Log("Not enough gold!");
            }

        }
        else
        {
            Debug.Log("Invalid location to build!");
        }
    }


    private void OnMouseOver() // when mousing over locations, should change color to buildable or not - will change colors to images later
    {
        if (placingTower)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // gets position of players mouse
            Vector3Int gridPosition = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0); // gets closest grid to mouse position

            if (buildableTilemap.HasTile(gridPosition))
            {
                towerPreview.GetComponent<SpriteRenderer>().color = validColor;
            }
            else if (enemyPathTilemap.HasTile(gridPosition))
            {
                towerPreview.GetComponent<SpriteRenderer>().color = invalidColor;
            }
        }
    }

    public void OnTowerPlacementButtonClick() // function for the button in UI, on button click starts the preview of tower so you can pick where u want to place it
    {
        if (!placingTower)
        {

            placingTower = true; // start building
            towerPreview = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity); // towerPrefab is the preview
            towerAttack = towerPreview.GetComponent<TowerAttack>(); // Get the TowerAttack script from the preview tower
            towerAttack.towerState = TowerState.Preview; // Set the state of the preview tower to "Preview"
            towerPreview.GetComponent<SpriteRenderer>().color = validColor; // Set the initial color to green
            towerPreview.gameObject.SetActive(true);
        }
        else
        {
            CancelTowerPlacement();
        }
    }

    private void CancelTowerPlacement() // stop building, destroys preview prefab
    {
        placingTower = false;
        if (towerPreview != null)
        {
            Destroy(towerPreview);
            towerPreview = null;
            towerAttack = null;
        }
    }
}
