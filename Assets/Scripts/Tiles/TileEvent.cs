using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileEvent : MonoBehaviour
{
    public UnityEvent action = null;
    public bool completed = false;

    public void SetCompleted()
    {
        completed = true;
    }
}
