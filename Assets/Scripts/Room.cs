using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room : SerializedMonoBehaviour
{
    public Vector2Int size = new Vector2Int(5, 5);
    [HideInInspector] public List<Sprite> tiles = new List<Sprite>();

    public List<GameObject> tileEntities = new List<GameObject>();
}
