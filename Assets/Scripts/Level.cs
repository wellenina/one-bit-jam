using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Level
{
    public string name;
    public float bonusMultiplier = 1.0f;

    public List<Room> slot1Rooms = new List<Room>();
    public List<Room> slot2Rooms = new List<Room>();
    public List<Room> slot3Rooms = new List<Room>();

    public List<Room> GenerateRooms()
    {
        List<Room> rooms = new List<Room>();

        Room room1 = GetRandomRoom(slot1Rooms);
        rooms.Add(room1);

        Room room2 = GetRandomRoom(slot2Rooms);
        while (room1 == room2)
        {
            room2 = GetRandomRoom(slot2Rooms);
        }
        rooms.Add(room2);

        Room room3 = GetRandomRoom(slot3Rooms);
        while (room3 == room1 || room3 == room2)
        {
            room3 = GetRandomRoom(slot3Rooms);
        }
        room3.isLastRoom = true;
        rooms.Add(room3);

        return rooms;
    }
    
    public Room GetRandomRoom(List<Room> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }

}
