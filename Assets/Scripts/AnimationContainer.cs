using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Animation", fileName = "Animation"), Serializable]
public class AnimationContainer : ScriptableObject
{
    public List<Sprite> data = new List<Sprite>();
    public float animationSpeed = 0.15f;
}
