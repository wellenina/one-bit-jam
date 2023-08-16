using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Levels levels;
    private int levelLenght = 3;
    private List<Room> roomList = new List<Room>();
    private int currentRoomIndex = 0;
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
        if (currentRoomIndex > 2)
        {
            Destroy(roomsInScene[0]);
            roomsInScene.RemoveAt(0);
        }
        int nextRoomIndex = currentRoomIndex + 2;
        if (nextRoomIndex < roomList.Count)
        {
            GameObject prefab = roomList[nextRoomIndex].background;
            GameObject newRoom = Instantiate(prefab, nextRoomPosition, Quaternion.identity);
            newRoom.transform.SetParent(roomsParent.transform);
            roomsInScene.Add(newRoom);
        }
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

    public bool isLastLevel()
    {
        return currentRoomIndex == roomList.Count - 1;
    }

    public bool isEndLevel()
    {
        return (currentRoomIndex + 1) % levelLenght == 0;
    }

    public void EndRunDestroyRooms()
    {
        roomList.Clear();
        currentRoomIndex = 0;
        foreach (GameObject room in roomsInScene)
        {
            Destroy(room);
        }
        roomsInScene.Clear();

        town.transform.SetParent(null);
        town.transform.position = townOriginalPosition;
        town.SetActive(true);
    }
}