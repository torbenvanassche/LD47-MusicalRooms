using System;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioManager)), RequireComponent(typeof(SceneManagement))]
public class Manager : Singleton<Manager>
{
    public AudioManager _audio = null;
    public Room CurrentRoom = null;
    [HideInInspector] public Camera camera = null;

    public Player player = null;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Reset()
    {
        _audio = GetComponent<AudioManager>();
    }
}
