using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioManager)), RequireComponent(typeof(SceneManagement))]
public class Manager : Singleton<Manager>
{
    public AudioManager _audio = null;
    public RoomManager rooms = null;
    
    [HideInInspector] public Room CurrentRoom = null;
    [HideInInspector] public Camera camera = null;

    public Player player = null;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Reset()
    {
        _audio = GetComponent<AudioManager>();
        rooms = GetComponent<RoomManager>();
        player = FindObjectOfType<Player>();
    }
}
