using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerResources : MonoBehaviour
{
    public int overallGold;
    public float goldConversionRate;

    public LevelResources levelResources;

    public int overallExperience;
    public float experienceConversionRate;

    // convert experience into talent points
    

    public void EndOfLevelGoldConvert(int gold) // work in progress/not implemented yet
    {
        int tempGold;

        tempGold = (int)(gold * goldConversionRate);

        overallGold += tempGold;
    }

    // Start is called before the first frame update
    void Start()
    {
        // find LevelManager on map
        // get reference to levelManager
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
