using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(SpriteAnimator))]
public class PlayerAnimator : SerializedMonoBehaviour
{
    private SpriteAnimator _animator = null;
    private SpriteAnimator _shadowAnimator = null;
    private Player _player = null;

    [SerializeField, OnValueChanged(nameof(AnimationChange))]
    private AnimationState currentAnimation = AnimationState.Idle;
    
    public enum AnimationState
    {
        Idle,
        WalkLeft,
        WalkRight,
        WalkUp,
        WalkDown
    }

    private void AnimationChange()
    {
        SetAnimation(currentAnimation);
    }

    public Dictionary<AnimationState, AnimationContainer> animations = new Dictionary<AnimationState, AnimationContainer>();

    public void SetAnimation(AnimationState state)
    {
        _animator.sprites = animations[state];
        _shadowAnimator.sprites = animations[state];
    }
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            _player = GetComponent<Player>();
            return _player;
        });
        
        _animator = GetComponent<SpriteAnimator>();
        if (!_player.shadow.TryGetComponent(out _shadowAnimator))
        {
            _shadowAnimator = _player.shadow.AddComponent<SpriteAnimator>();
        }

        SetAnimation(currentAnimation);


    }
}
