using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] levels = new Level[3];

    // Start is called before the first frame update
    void Start()
    {
        GenerateRun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRun()
    {
        foreach (Level level in levels)
        {
            level.FillRooms();

            // FOR TESTING
            int num = System.Array.IndexOf(levels, level) + 1;
            Debug.Log("Level " + num + ": " + level.name);
            level.PrintInfo();
        }
    }
}
