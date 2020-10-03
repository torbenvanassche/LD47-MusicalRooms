using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Sprites", fileName = "RoomTileContainer")]
public class RoomTileContainer : SerializedScriptableObject
{
    public Dictionary<string, RoomTile> data = new Dictionary<string, RoomTile>();

    public RoomTile Get(string key)
    {
        return data.TryGetValue(key, out var clip) ? clip : null;
    }
}
