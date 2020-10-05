using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomManager : SerializedMonoBehaviour
{
    public List<Room> Rooms = new List<Room>();
    public int currentRoom;

    private void Start()
    {
        Manager.Instance.currentRoom = Rooms[currentRoom];
    }

    [Button]
    private void RegenerateAll()
    {
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
