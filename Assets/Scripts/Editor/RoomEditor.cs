using System;
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
            Debug.Log(v.x);
            if (i % v.x == 0) GUILayout.BeginHorizontal();

            r.tiles[i] = EditorGUILayout.ObjectField(r.tiles[i], typeof(RoomTile), false,
                GUILayout.Height(Mathf.Floor(width / (float)v.x)), GUILayout.Width(Mathf.Floor(width / (float)v.x))) as RoomTile;
            
            if (i % v.x == v.x - 1) GUILayout.EndHorizontal();
        }
    }
}
