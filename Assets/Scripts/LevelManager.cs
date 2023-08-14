using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Levels levels;
    [SerializeField] private List<Room> roomList;
    private int currentRoomIndex = 0;
    public Room currentRoom;


    public void GenerateRun()
    {
        foreach (Level level in levels.list)
        {
            List<Room> levelRooms = level.GenerateRooms();
            roomList.AddRange(levelRooms);

            /* // FOR TESTING
            Debug.Log(level.name);
            foreach (Room room in levelRooms)
            {
                room.PrintInfo();
            } */
        }

        currentRoom = roomList[currentRoomIndex];
    }

    public void GetNextRoom()
    {
        currentRoom = roomList[currentRoomIndex++];
    }

}
