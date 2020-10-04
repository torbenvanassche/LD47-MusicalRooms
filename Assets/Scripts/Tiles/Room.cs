using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Room : SerializedMonoBehaviour
{
    public Vector2Int size = new Vector2Int(5, 5);
    [HideInInspector] public List<Texture2D> tiles = new List<Texture2D>();
    public GameObject prefab = null;

    private bool isCompleted = false;
    [HideInInspector] public int noteAmount = 1;

    public Door door = null;

    [HideInInspector] public List<GameObject> tileEntities = new List<GameObject>();
    
    public List<Texture2D> tileClickAnimation = new List<Texture2D>();
    public float highlightDuration = 1;

    public void IsCompleted()
    {
        isCompleted = tileEntities
            .Where(x => x.TryGetComponent<TileEvent>(out var component) && !component.completed)
            .ToList().Count == 0;

        if (isCompleted)
        {
            door.Open();
        }
    }
}
