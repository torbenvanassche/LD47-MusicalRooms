using UnityEngine;

public class OrientShadow : MonoBehaviour
{
    private Player _player;

    private Vector3 offset = Vector3.zero;

    void Start()
    {
        offset.y -= Manager.Instance.player.renderer.bounds.size.y / 2;
        offset.z += Manager.Instance.player.renderer.bounds.size.x / 2;
    }
    
    void Update()
    {
        transform.position = Manager.Instance.player.transform.position + offset;
    }
}
