using UnityEngine;

public class Restart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Manager.Instance.bgmPlayer.Queue();
        }
    }
}
