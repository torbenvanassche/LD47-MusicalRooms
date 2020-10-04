﻿using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Room))]
public class RoomEditor : OdinEditor
{
    private Room r;

    private Vector2Int v = Vector2Int.zero;

    protected override void OnEnable()
    {
        r = target as Room;
        v = r.size;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        r.noteAmount = EditorGUILayout.IntSlider(r.noteAmount, 0, v.x * v.y);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset")) r.DestroyRoom();
        if (GUILayout.Button("Populate")) r.Populate();
        GUILayout.EndHorizontal();


        var width = Screen.width - 50;
        
        for (var i = 0; i < v.x * v.y; i++)
        {
            if (i % v.x == 0) GUILayout.BeginHorizontal();

            r.tiles[i] = EditorGUILayout.ObjectField(r.tiles[i], typeof(Sprite), false,
                GUILayout.Height(Mathf.Floor(width / (float)v.x)), GUILayout.Width(Mathf.Floor(width / (float)v.x))) as Texture2D;
            
            if (i % v.x == v.x - 1) GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Regenerate"))
        {
            r.Regenerate();
            
            if(!Application.isPlaying) EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
