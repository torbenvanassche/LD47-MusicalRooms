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
            if (Physics.Raycast(transform.position, transform.forward, out var hit))
            {
                if (hit.transform.gameObject.TryGetComponent<TileEvent>(out var tileEvent))
                {
                    tileEvent.action.Invoke();
                    Manager.Instance.CurrentRoom.IsCompleted();
                }
            }
        };
    }
}
