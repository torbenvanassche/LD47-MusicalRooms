using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    public static InputSettings Controls;

    [HideInInspector] public Rigidbody rig = null;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        Controls = new InputSettings();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
}
