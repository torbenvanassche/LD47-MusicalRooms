using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioManager)), RequireComponent(typeof(SceneManagement))]
public class Manager : Singleton<Manager>
{
    public new AudioManager audio = null;
    public RoomManager rooms = null;
    public BGM bgmPlayer = null;
    
    [HideInInspector] public Room currentRoom = null;
    [HideInInspector] public new Camera camera = null;

    public Player player = null;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Reset()
    {
        audio = GetComponent<AudioManager>();
        rooms = GetComponent<RoomManager>();
        player = FindObjectOfType<Player>();
    }
}
