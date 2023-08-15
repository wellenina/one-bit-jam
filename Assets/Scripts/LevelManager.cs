using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Levels levels;
    private List<Room> roomList = new List<Room>();
    private int currentRoomIndex = 0;
    public Room currentRoom;

    [SerializeField] private List<GameObject> roomSlots = new List<GameObject>();
    private List<Vector3> roomSlotsPositions = new List<Vector3>();

    [SerializeField] private List<GameObject> roomsInScene = new List<GameObject>();


    void Start()
    {
        GetRoomSlotsPositions();
    }


    public void GenerateRun()
    {
        foreach (Level level in levels.list)
        {
            List<Room> levelRooms = level.GenerateRooms();
            roomList.AddRange(levelRooms);
        }

        roomList.Last().isLastLevel = true;

        InstantiateFirstRooms();
        currentRoom = roomList[currentRoomIndex];
    }

    public void GoToNextRoom()
    {
        currentRoom = roomList[currentRoomIndex++];
        InstantiateNextRoom();
        moveRooms();
    }


    void GetRoomSlotsPositions()
    {
        foreach (GameObject slot in roomSlots)
        {
            roomSlotsPositions.Add(slot.transform.position);
        }
    }

    void InstantiateFirstRooms()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newRoom = Instantiate(roomList[i].background, roomSlotsPositions[i+2], Quaternion.identity);
            roomsInScene.Add(newRoom);
        }
    }

    void InstantiateNextRoom()
    {
        Destroy(roomsInScene[0]);
        roomsInScene.RemoveAt(0);
        GameObject newRoom = Instantiate(roomList[currentRoomIndex+2].background, roomSlotsPositions[4], Quaternion.identity);
        roomsInScene.Add(newRoom);
    }

    void moveRooms()
    {
        for (int i = 0; i < roomsInScene.Count; i++)
        {
            roomsInScene[i].transform.position = roomSlotsPositions[i];
        }
    }

    public void DestroyRooms()
    {
        roomList.Clear();
        currentRoomIndex = 0;
        foreach (GameObject room in roomsInScene)
        {
            Destroy(room);
        }
        roomsInScene.Clear();
    }

}