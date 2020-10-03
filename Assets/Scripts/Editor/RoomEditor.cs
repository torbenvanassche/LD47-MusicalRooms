using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
        
        r.noteAmount = EditorGUILayout.IntSlider(r.noteAmount, 0, count);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset"))
        {
            r.tiles.Clear();
            
            foreach (var tileEntity in r.tileEntities)
            {
                DestroyImmediate(tileEntity);
            }

            r.tileEntities.Clear();
            
            v = r.size;
            count = v.x * v.y;
            while (count > r.tiles.Count)
            {
                r.tiles.Add(null);
            }
        }
        
        if (GUILayout.Button("Populate"))
        {
            if (r.prefab)
            {
                var emptySprites = r.tiles.Where(x => x == null).ToArray();

                for (var index = 0; index < r.tiles.Count; index++)
                {
                    if (!r.tiles[index])
                    {
                        r.tiles[index] = r.prefab.GetComponent<MeshRenderer>().sharedMaterial.mainTexture as Texture2D;
                    }
                }
            }
        }
        
        GUILayout.EndHorizontal();


        var width = Screen.width - 50;
        
        for (var i = 0; i < count; i++)
        {
            if (i % v.x == 0) GUILayout.BeginHorizontal();

            r.tiles[i] = EditorGUILayout.ObjectField(r.tiles[i], typeof(Sprite), false,
                GUILayout.Height(Mathf.Floor(width / (float)v.x)), GUILayout.Width(Mathf.Floor(width / (float)v.x))) as Texture2D;
            
            if (i % v.x == v.x - 1) GUILayout.EndHorizontal();
        }
        
        if (GUILayout.Button("Regenerate"))
        {
            Regenerate();
        }
    }

    private void Randomize()
    {
        while (true)
        {
            var randomIndex = Random.Range(0, r.tileEntities.Count);

            if (!r.tileEntities[randomIndex]) continue;
            
            var tile = r.tileEntities[randomIndex].GetComponent<TileEvent>();

            if (!tile)
            {
                r.tileEntities[randomIndex].AddComponent<TileEvent>();
                r.tileEntities[randomIndex].name = "EventTile";
                r.tileEntities[randomIndex].GetComponent<MeshRenderer>().sharedMaterial =
                    AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/EventTile.mat");
            }
            else
            {
                continue;
            }

            break;
        }
    }

    private void Regenerate()
    {
        if (r.tiles.All(x => x == null))
        {
            return;
        }
        
        foreach (var tileEntity in r.tileEntities.Where(tileEntity => tileEntity))
        {
            DestroyImmediate(tileEntity);
        }

        r.tileEntities.Clear();

        for (var index = 0; index < r.tiles.Count; index++)
        {
            var rTile = r.tiles[index];

            if (!rTile) continue;

            var gO = Instantiate(r.prefab);
            var ren = gO.GetComponent<MeshRenderer>();

            gO.transform.SetParent(r.transform);
            gO.transform.localScale = Vector3.one;
            gO.transform.Translate(ren.bounds.size.x * (index % v.x), -ren.bounds.size.z * (index / v.x), 0);
            gO.transform.LookAt(gO.transform.position + Vector3.down);
            gO.name = "Ground";

            var collider = gO.GetComponent<BoxCollider>();
            collider.center = new Vector3(collider.center.x, collider.center.y, 0.05f); 
            collider.size = new Vector3(collider.size.x, collider.size.y, 0.1f); 
            

            r.tileEntities.Add(gO);
        }
        
        for (var i = 0; i < r.noteAmount; i++)
        {
            Randomize();
        }

        if (!Application.isPlaying)
        {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());   
        }
    }
}
