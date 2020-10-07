using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveInput;
    private Vector3 moveDirection;
    private Vector3 moveForce;
    private Vector3 outDir;
    private Player player = null;
    private GameObject target = null;
    public bool sprinting = false;
    
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] public float sprintModifier = 1.5f;
    
    [SerializeField, Space] private Transform spawnPosition = null;
    
    private IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            player = GetComponent<Player>();
            return player;
        });
        
        Player.Controls.Player.Move.performed += context =>
        {
            var input = context.ReadValue<Vector2>();
            moveInput = new Vector3(input.x, 0, input.y);
        };
        Player.Controls.Player.Move.canceled += context =>
        {
            moveInput = Vector3.zero;
            player.rig.velocity = moveInput;
        };

        Player.Controls.Player.Sprint.performed += context =>
        {
            sprinting = true;
        };

        Player.Controls.Player.Sprint.canceled += context =>
        {
            sprinting = false;
        };

        if (spawnPosition)
        {
            Move(spawnPosition.position);
        }
        
        target = new GameObject();
    }
    
    public void Update()
    {
        if (player.rig)
        {
            moveDirection = Manager.Instance.camera.transform.rotation * moveInput;
            moveDirection.y = 0;

            outDir = moveDirection.normalized * movementSpeed;
            if (sprinting) outDir *= sprintModifier;
            

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
        player.rig.AddForce(outDir);
    }

    private void Move(Vector3 position)
    {
        var pos = position;
        pos.y += GetComponent<SpriteRenderer>().size.y / 2;
        transform.position = pos;
    }
}
