using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    private Vector2 _moveDirection;
    private Vector2 _moveForce;
    
    [SerializeField] private float _movementSpeed = 10f;

    private Quaternion _lastRotation;
    private Vector2 _outDir = Vector3.zero;

    private Player _player = null;

    IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _player = GetComponent<Player>();
            return _player;
        });
        
        Player.Controls.Player.Move.performed += context =>
        {
            moveInput = context.ReadValue<Vector2>();
        };
        Player.Controls.Player.Move.canceled += context => moveInput = Vector2.zero;

        Player.Controls.Player.Interact.performed += context =>
        {
            Debug.Log(context);
        };
    }
    
    public void Update()
    {
        if (_player.rig)
        {
            _moveDirection = moveInput;
            _outDir = _moveDirection.normalized * _movementSpeed;
        }
    }

    public void FixedUpdate()
    {
        _player.rig.AddForce(_outDir);
    }
}
