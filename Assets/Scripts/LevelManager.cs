using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Levels levels;
    private List<Room> roomList = new List<Room>(); //
    private int currentRoomIndex = 0; //
    [HideInInspector] public Room currentRoom;
    private List<GameObject> roomsInScene = new List<GameObject>();

    [SerializeField] private Vector3 firstRoomPosition;
    [SerializeField] private Vector3 nextRoomPosition;
    [SerializeField] private GameObject roomsParent;
    private Vector3 targetPosition;
    private bool roomsAreMoving = false;
    [SerializeField] private float speed;
    [SerializeField] private float roomWidth = 106.0f;

    [SerializeField] private GameObject town;
    private Vector3 townOriginalPosition;


    public void GenerateRun()
    {
        foreach (Level level in levels.list)
        {
            List<Room> levelRooms = level.GenerateRooms();
            roomList.AddRange(levelRooms);
        }

        roomList.Last().isLastLevel = true;

        townOriginalPosition = town.transform.position;
        roomsParent.transform.position = Vector3.zero;
        InstantiateFirstRooms();
        currentRoom = roomList[currentRoomIndex];
    }

    void InstantiateFirstRooms()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject prefab = roomList[i].background;
            Vector3 position = firstRoomPosition + new Vector3(roomWidth * i, 0, 0);
            GameObject newRoom = Instantiate(prefab, position, Quaternion.identity);
            newRoom.transform.SetParent(roomsParent.transform);
            roomsInScene.Add(newRoom);
        }
    }

    public void MoveTown() // <-- run manager BeginRun()
    {
        town.transform.SetParent(roomsParent.transform);
        StartMovingRooms(2);
    }

    public void GoToNextRoom()
    {
        currentRoom = roomList[++currentRoomIndex];
        InstantiateNextRoom();
        if (currentRoomIndex == 2)
        {
            town.transform.SetParent(null);
            town.SetActive(false);
        }
        StartMovingRooms();
    }

    void InstantiateNextRoom()
    {
        if (roomsInScene.Count == 5)
        {
            Destroy(roomsInScene[0]);
            roomsInScene.RemoveAt(0);
        }
        GameObject prefab = roomList[currentRoomIndex+2].background;
        GameObject newRoom = Instantiate(prefab, nextRoomPosition, Quaternion.identity);
        newRoom.transform.SetParent(roomsParent.transform);
        roomsInScene.Add(newRoom);
    }

    void StartMovingRooms(int amount = 1)
    {
        targetPosition = roomsParent.transform.position + Vector3.left * roomWidth * amount;
        roomsAreMoving = true;
    }

    void Update()
    {
        if (roomsAreMoving)
        {
            MoveRooms();
        }
    }

    void MoveRooms()
    {
        float step =  speed * Time.deltaTime;
        roomsParent.transform.position = Vector3.MoveTowards(roomsParent.transform.position, targetPosition, step);
        if (roomsParent.transform.position == targetPosition)
        {
            roomsAreMoving = false;
        }
    }

    public void DestroyRooms() // for the END of the RUN
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