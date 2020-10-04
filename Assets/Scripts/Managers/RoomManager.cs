using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RoomManager : SerializedMonoBehaviour
{
    public List<Room> Rooms = new List<Room>();
    private int index = 0;

    private void Start()
    {
        Manager.Instance.CurrentRoom = Rooms[index];
    }

    public void Next()
    {
        index++;

        if (index < Rooms.Count) Manager.Instance.CurrentRoom = Rooms[index];
    }

    [Button] public void Skip()
    {
        Manager.Instance.CurrentRoom.door.Open();
        Next();
    }
}
