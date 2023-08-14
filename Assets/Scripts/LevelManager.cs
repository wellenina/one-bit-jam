using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Levels levels;
    public List<Room> roomList;
    public int currentRoomIndex = 0;
    private MainUIManager UImanager;


    void Start()
    {
        UImanager = GetComponent<MainUIManager>();
        GenerateRun();
        UImanager.SetNewRoom(roomList[currentRoomIndex]);
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

    public void EnterNextRoom()
    {
        currentRoomIndex++;
        Room room = roomList[currentRoomIndex];

        Debug.Log("ENTERING ROOM NUMBER " + currentRoomIndex);

        // UI:
        UImanager.SetNewRoom(room);

        // in scena:
        // background = room.background;

    }

}
