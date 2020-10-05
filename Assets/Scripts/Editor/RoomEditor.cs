using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Room))]
public class RoomEditor : OdinEditor
{
    private Room r;
    private Vector2Int v = Vector2Int.zero;
    private bool fold = false;
    protected override void OnEnable()
    {
        r = target as Room;
        v = r.size;
    }
    
    private void Populate()
    {
        if (r.prefab)
        {
            for (var index = 0; index < r.tiles.Count; index++)
            {
                if (!r.tiles[index])
                {
                    r.tiles[index] = r.prefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture as Texture2D;
                }
            }
        }
    }
    
    private void DestroyRoom()
    {
        r.Clear();
        r.tiles.Clear();
        
        while (r.size.x * r.size.y > r.tiles.Count)
        {
            r.tiles.Add(null);
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset")) DestroyRoom();
        if (GUILayout.Button("Populate")) Populate();
        GUILayout.EndHorizontal();

        fold = EditorGUILayout.Foldout(fold, "Show / Hide grid editor.");
        if (fold)
        {
            var width = Screen.width - 50;
            for (var i = 0; i < v.x * v.y; i++)
            {
                if (i % v.x == 0) GUILayout.BeginHorizontal();

                r.tiles[i] = EditorGUILayout.ObjectField(r.tiles[i], typeof(Sprite), false,
                    GUILayout.Height(Mathf.Floor(width / (float)v.x)), GUILayout.Width(Mathf.Floor(width / (float)v.x))) as Texture2D;
            
                if (i % v.x == v.x - 1) GUILayout.EndHorizontal();
            }   
        }

        if (GUILayout.Button("Regenerate"))
        {
            r.Regenerate();

            if(!Application.isPlaying) EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
