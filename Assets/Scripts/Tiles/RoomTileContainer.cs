using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Sprites", fileName = "RoomTileContainer")]
public class RoomTileContainer : SerializedScriptableObject
{
    public Dictionary<string, Sprite> data = new Dictionary<string, Sprite>();

    public Sprite Get(string key)
    {
        return data.TryGetValue(key, out var clip) ? clip : null;
    }
}
