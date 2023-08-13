using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scriptables/Level")]
public class Level : ScriptableObject
{
    public new string name;
    public float bonusMultiplier = 1.0f;

    public Room room1;
    public List<Room> slot1rooms = new List<Room>();

    public Room room2;
    public List<Room> slot2rooms = new List<Room>();

    public Room room3;
    public List<Room> slot3rooms = new List<Room>();

    public void FillRooms()
    {
        room1 = GetRandomRoom(slot1rooms);

        room2 = GetRandomRoom(slot2rooms);
        while (room1 == room2)
        {
            room2 = GetRandomRoom(slot2rooms);
        }

        room3 = GetRandomRoom(slot3rooms);
        while (room3 == room1 || room3 == room2)
        {
            room3 = GetRandomRoom(slot3rooms);
        }
    }
    public Room GetRandomRoom(List<Room> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }

    // FOR TESTING
    public void PrintInfo()
    {
        Debug.Log("Room 1: ");
        room1.PrintInfo();

        Debug.Log("Room 2: ");
        room2.PrintInfo();

        Debug.Log("Room 3: ");
        room3.PrintInfo();
    }

}
