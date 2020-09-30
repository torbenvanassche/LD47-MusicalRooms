using System;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(AudioManager))]
public class Manager : Singleton<Manager>
{
    [NonSerialized] public AudioManager _audio = null;
    
    private void Awake()
    {
        _audio = GetComponent<AudioManager>();
    }
}
