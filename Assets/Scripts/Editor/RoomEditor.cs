using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomEditor : OdinEditor
{
    private Room r = null;
    private int count = 0;
    
    private void OnEnable()
    {
        r = target as Room;
        count = r.size.x * r.size.y;
        
        while (count > r.tiles.Count)
        {
            r.tiles.Add(null);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var width = Screen.width - 50;
        
        for (var i = 0; i < count; i++)
        {
            if (i % 5 == 0) GUILayout.BeginHorizontal();

            r.tiles[i] = EditorGUILayout.ObjectField(r.tiles[i], typeof(RoomTile), false,
                GUILayout.Height(width / 5), GUILayout.Width(width / 5)) as RoomTile;
            
            if (i % 5 == 4) GUILayout.EndHorizontal();
        }
    }
}
