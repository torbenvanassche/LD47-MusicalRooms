using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomManager : SerializedMonoBehaviour
{
    private List<Room> Rooms = new List<Room>();
    private int currentRoom;

    private void Start()
    {
        Rooms = FindObjectsOfType<Room>().OrderBy(room => room.name).ToList();

        Manager.Instance.currentRoom = Rooms[currentRoom];
    }

    [Button]
    private void RegenerateAll()
    {
        Rooms = FindObjectsOfType<Room>().OrderBy(room => room.name).ToList();
        foreach (var room in Rooms)
        {
            room.Regenerate();
        }
    }

    public void Next()
    {
        //close entrance to previous room
        var prevIndex = currentRoom - 1;
        if (prevIndex < 0) prevIndex = Rooms.Count - 1;
        Rooms[prevIndex].door.Close();
        Rooms[prevIndex].Regenerate();
        
        currentRoom++;
        if (currentRoom >= Rooms.Count) currentRoom = 0;
        Manager.Instance.currentRoom = Rooms[currentRoom];
    }
}
