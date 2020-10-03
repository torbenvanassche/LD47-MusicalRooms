using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room : SerializedMonoBehaviour
{
    public Vector2Int size = new Vector2Int(5, 5);
    public List<RoomTile> tiles = new List<RoomTile>(); 
}
