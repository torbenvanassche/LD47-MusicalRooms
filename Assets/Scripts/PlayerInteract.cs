using System;
using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Player _player = null;
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _player = GetComponent<Player>();
            return _player;
        });

        Player.Controls.Player.Interact.performed += context =>
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hit))
            {
                if (hit.transform.gameObject.TryGetComponent<TileEvent>(out var tileEvent) && tileEvent.interactable)
                {
                    tileEvent.action.Invoke();
                }
            }
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position - transform.up);
    }
}
