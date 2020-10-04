using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : SerializedMonoBehaviour
{
    public Vector2Int size = new Vector2Int(5, 5);
    public GameObject prefab = null;
    [SerializeField] private Material interactableMaterial = null;
    public Door door = null;

    private bool isCompleted = false;
    
    [HideInInspector] public List<Texture2D> tiles = new List<Texture2D>();
    [HideInInspector] public int noteAmount = 1;

    public void IsCompleted()
    {
        isCompleted = transform.GetComponentsInChildren<TileEvent>()
            .Where(x => !x.completed)
            .ToList().Count == 0;

        if (isCompleted)
        {
            door.Open();
            Manager.Instance.rooms.Next();
        }
    }
    
    //Generate room
    public void DestroyRoom()
    {
        foreach (Transform tileEntity in transform) DestroyImmediate(tileEntity);
        tiles.Clear();
        
        while (size.x * size.y > tiles.Count)
        {
            tiles.Add(null);
        }
    }

    public void Populate()
    {
        if (prefab)
        {
            for (var index = 0; index < tiles.Count; index++)
            {
                if (!tiles[index])
                {
                    tiles[index] = prefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture as Texture2D;
                }
            }
        }
    }
    
    private void Randomize()
    {
        while (true)
        {
            var randomIndex = Random.Range(0, transform.childCount);
            if (!transform.GetChild(randomIndex) ||  transform.GetChild(randomIndex).GetComponent<TileEvent>()) continue;
            
            transform.GetChild(randomIndex).gameObject.AddComponent<TileEvent>();
            transform.GetChild(randomIndex).gameObject.name = "EventTile";
            transform.GetChild(randomIndex).gameObject.GetComponent<MeshRenderer>().sharedMaterial = interactableMaterial;
            break;
        }
    }
    
    public void Regenerate()
    {
        if (tiles.All(x => x == null)) return;
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        var oldPos = gameObject.transform.position;
        gameObject.transform.position = Vector3.zero;
        for (var index = 0; index < tiles.Count; index++)
        {
            var rTile = tiles[index];

            if (!rTile) continue;
            
            var gO = Instantiate(prefab, gameObject.transform);
            var ren = gO.GetComponent<MeshRenderer>();

            gO.transform.position = new Vector3(ren.bounds.size.x * (index % size.x), 0, -ren.bounds.size.z * (index / size.x));
            gO.transform.LookAt(gO.transform.position + Vector3.down);
            gO.name = "Ground";

            var boxCollider = gO.GetComponent<BoxCollider>();
            boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, 0.05f); 
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0.1f);

            gO.transform.Translate(-size.x / 2, size.y / 2, 0);
        }
        
        gameObject.transform.position = oldPos;
        
        for (var i = 0; i < noteAmount; i++)
        {
            Randomize();
        }
    }
}
