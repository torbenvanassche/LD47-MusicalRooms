using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room : SerializedMonoBehaviour
{
    public Vector2Int size = new Vector2Int(5, 5);
    [HideInInspector] public List<Sprite> tiles = new List<Sprite>();
    public Sprite defaultTile = null;

    public bool isCompleted = false;

    [HideInInspector] public List<GameObject> tileEntities = new List<GameObject>();

    public void IsCompleted()
    {
        isCompleted = tileEntities
            .Where(x => x.TryGetComponent<TileEvent>(out var component) && !component.completed)
            .ToList().Count == 0;
    }
}
