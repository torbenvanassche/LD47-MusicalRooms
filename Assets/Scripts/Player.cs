using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    public static InputSettings Controls;

    [HideInInspector] public Rigidbody rig = null;
    [HideInInspector] public CapsuleCollider collider = null;
    
    public GameObject shadow;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        Controls = new InputSettings();
        collider = GetComponent<CapsuleCollider>();
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
