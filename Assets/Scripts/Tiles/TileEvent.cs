using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TileEvent : MonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] private AudioFileSettings _audioFile = null;
    public float highlightDuration = 1;

    public UnityEvent action = new UnityEvent();
    [HideInInspector] public bool completed = false;
    private Texture2D defaultTexture;

    private IEnumerable GetAudioFiles()
    {
        return Manager.Instance.audio ? Manager.Instance.audio.audioContainer.data.Values : null;
    }

    public void ValidateEvent()
    {
        var todoEvents = Manager.Instance.currentRoom.order;
        if (todoEvents.Count != 0 && todoEvents[0].GetComponent<TileEvent>() == this)
        {
            completed = true;
            todoEvents.Remove(gameObject);
            return;
        }

        completed = false;
    }

    private void Start()
    {
        action.AddListener(() =>
        {
            if (_audioFile && Manager.Instance.audio)
            {
                Manager.Instance.audio.PlaySound(_audioFile);
            }
        });
        
        action.AddListener(ValidateEvent);

        defaultTexture = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
    }

    public void ChangeMaterial(Texture2D tex)
    {
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = tex;
        if (highlightDuration != 0)
        {
            StartCoroutine(nameof(ResetMaterial), highlightDuration);   
        }
    }

    private IEnumerator ResetMaterial(float f)
    {
        yield return new WaitForSeconds(f);
        GetComponent<MeshRenderer>().material.mainTexture = defaultTexture;
        yield return null;
    }
}
