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
    
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] public List<AudioFileSettings> walkSound = null;
    private AudioSource footstepSource;
    private bool muteFootsteps = true;

    private IEnumerable GetAudioFiles()
    {
        return Manager.Instance.audio ? Manager.Instance.audio.audioContainer.data.Values : null;
    }
    
    public enum AnimationState
    {
        Idle,
        WalkLeft,
        WalkRight,
        WalkUp,
        WalkDown
    }

    public AnimationState CurrentAnimation
    {
        get => currentAnimation;
        set
        {
            currentAnimation = value;
            SetAnimation(value);
        }
    }

    private void AnimationChange()
    {
        SetAnimation(CurrentAnimation);
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

        footstepSource = GetComponent<AudioSource>();
        
        SetAnimation(CurrentAnimation);

        Player.Controls.Player.Move.performed += context =>
        {
            StartCoroutine(Footsteps());
            muteFootsteps = false;
            
            var input = context.ReadValue<Vector2>();
            if (input.y > 0)
            {
                CurrentAnimation = AnimationState.WalkUp;
            }

            if (input.y < 0)
            {
                CurrentAnimation = AnimationState.WalkDown;
            }
            if (input.x < 0)
            {
                CurrentAnimation = AnimationState.WalkRight;
            }
            if (input.x > 0)
            {
                CurrentAnimation = AnimationState.WalkLeft;
            }
        };
        
        Player.Controls.Player.Move.canceled += context =>
        {
            CurrentAnimation = AnimationState.Idle;
            muteFootsteps = true;
        };
    }
    
    void PlaySound(AudioFileSettings audio)
    {
        if (footstepSource)
        {
            footstepSource.clip = audio.clip;
            footstepSource.volume = audio.Volume;
            footstepSource.pitch = audio.Pitch;
            footstepSource.priority = audio.Priority;
            footstepSource.loop = audio.Loop;
            footstepSource.mute = muteFootsteps;
            footstepSource.outputAudioMixerGroup = audio.mixerGroup;

            footstepSource.Play();   
        }
    }

    IEnumerator Footsteps()
    {
        var lastIndex = -1;

        while (true)
        {
            if (!footstepSource.isPlaying)
            {
                var currentIndex = Random.Range(0, walkSound.Count);

                if (lastIndex != currentIndex)
                {
                    PlaySound(walkSound[currentIndex]);
                    lastIndex = currentIndex;
                    continue;
                }
            }
            
            var delay = 10 * Time.deltaTime;
            if (GetComponent<PlayerMovement>().sprinting) delay *= GetComponent<PlayerMovement>().sprintModifier;
            yield return new WaitForSeconds(delay);
        }
    }
}
