using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Levels levels;
    // levels.list
    public List<Room> roomList;

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
        foreach (Level level in levels.list)
        {
            List<Room> levelRooms = level.GenerateRooms();
            roomList.AddRange(levelRooms);

            // FOR TESTING
            Debug.Log(level.name);
            foreach (Room room in levelRooms)
            {
                room.PrintInfo();
            }
        }
    }
}
