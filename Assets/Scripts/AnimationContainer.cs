using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Container/Animation", fileName = "Animation")]
public class AnimationContainer : SerializedScriptableObject
{
    public List<Sprite> data = new List<Sprite>();
    public float animationSpeed = 0.15f;
}
