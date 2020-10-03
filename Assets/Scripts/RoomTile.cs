using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class RoomTile : SerializedScriptableObject
{
   public Sprite tile;
   public UnityEvent triggered;
}
