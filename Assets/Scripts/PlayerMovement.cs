using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveInput;
    private Vector3 _moveDirection;
    private Vector3 _moveForce;
    
    [SerializeField] private float _movementSpeed = 10f;
    private Vector3 _outDir = Vector3.zero;

    private Player _player = null;

    [SerializeField] private Transform spawnPosition = null;

    IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _player = GetComponent<Player>();
            return _player;
        });
        
        Player.Controls.Player.Move.performed += context =>
        {
            var input = context.ReadValue<Vector2>();
            moveInput = new Vector3(input.x, 0, input.y);
        };
        Player.Controls.Player.Move.canceled += context => moveInput = Vector3.zero;

        if (spawnPosition)
        {
            var pos = spawnPosition.position;
            pos.y += GetComponent<SpriteRenderer>().size.y / 2;
            transform.position = pos;
        }
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
