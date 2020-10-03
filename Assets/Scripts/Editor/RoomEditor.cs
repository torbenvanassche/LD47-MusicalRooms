using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomEditor : OdinEditor
{
    private Room r = null;
    private int count = 0;
    
    private Vector2Int v = Vector2Int.zero;

    protected override void OnEnable()
    {
        r = target as Room;
        v = r.size;
        count = v.x * v.y;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset"))
        {
            r.tiles.Clear();
            
            v = r.size;
            count = v.x * v.y;
            while (count > r.tiles.Count)
            {
                r.tiles.Add(null);
            }
        }

        var width = Screen.width - 50;
        
        for (var i = 0; i < count; i++)
        {
            if (i % v.x == 0) GUILayout.BeginHorizontal();

            r.tiles[i] = EditorGUILayout.ObjectField(r.tiles[i], typeof(Sprite), false,
                GUILayout.Height(Mathf.Floor(width / (float)v.x)), GUILayout.Width(Mathf.Floor(width / (float)v.x))) as Sprite;
            
            if (i % v.x == v.x - 1) GUILayout.EndHorizontal();
        }
        
        if (GUILayout.Button("Regenerate"))
        {
            Regenerate();
        }
    }

    private void Regenerate()
    {
        foreach (var tileEntity in r.tileEntities)
        {
            DestroyImmediate(tileEntity);
        }

        r.tileEntities.Clear();

        for (var index = 0; index < r.tiles.Count; index++)
        {
            var rTile = r.tiles[index];
            
            var gO = new GameObject();
            var sprite = gO.AddComponent<SpriteRenderer>().sprite;
            sprite = rTile;
            gO.transform.SetParent(r.transform);
            gO.transform.Translate(sprite.bounds.size.x * (index % v.x), -sprite.bounds.size.y * (index / v.x), 0);
            gO.AddComponent<TileEvent>();
            
            r.tileEntities.Add(gO);
        }
    }
}
