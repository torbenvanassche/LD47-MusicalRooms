using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    public static InputSettings Controls;

    [HideInInspector] public Rigidbody2D rig = null;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
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
