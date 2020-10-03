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

            _player.rig.MovePosition(transform.position + moveInput);
            if (Physics.Raycast(transform.position, Vector3.down, out var hit))
            { 
                Move(hit.transform.position);
            }
        };
        Player.Controls.Player.Move.canceled += context => moveInput = Vector3.zero;

        if (spawnPosition)
        {
            Move(spawnPosition.position);
        }
    }

    private void Move(Vector3 position)
    {
        var pos = position;
        pos.y += GetComponent<SpriteRenderer>().size.y / 2;
        transform.position = pos;
    }
}
