using System;
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
    
    private Quaternion _lastRotation;
    [SerializeField] private float _rotationSpeed = 5f;

    private GameObject target = null;

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
        Player.Controls.Player.Move.canceled += context =>
        {
            moveInput = Vector3.zero;
            _player.rig.velocity = moveInput;
        };

        if (spawnPosition)
        {
            Move(spawnPosition.position);
        }
        
        target = new GameObject();
    }
    
    public void Update()
    {
        if (_player.rig)
        {
            _moveDirection = Manager.Instance.camera.transform.rotation * moveInput;
            _moveDirection.y = 0;

            _outDir = _moveDirection.normalized;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, _lastRotation, Time.deltaTime * _rotationSpeed);

            //add move speed
            _outDir *= _movementSpeed;

            if (target)
            {
                var cameraPosition = Manager.Instance.camera.transform.position;
                cameraPosition.y = transform.position.y;
                target.transform.position = cameraPosition;
            
                transform.LookAt(target.transform);
            }
        }
    }

    public void FixedUpdate()
    {
        _player.rig.AddForce(_outDir);
    }

    private void Move(Vector3 position)
    {
        var pos = position;
        pos.y += GetComponent<SpriteRenderer>().size.y / 2;
        transform.position = pos;
    }
}
