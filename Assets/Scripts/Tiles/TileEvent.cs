using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TileEvent : MonoBehaviour
{
    [SerializeField, ValueDropdown(nameof(GetAudioFiles))] public AudioFileSettings audioFile = null;
    public float highlightDuration = 1;

    public UnityEvent action = new UnityEvent();
    [HideInInspector] public bool completed = false;
    private Texture2D defaultTexture;
    [HideInInspector] public Texture2D activeTexture;
    [HideInInspector] public bool interactable = true;

    private IEnumerable GetAudioFiles()
    {
        return Manager.Instance.audio ? Manager.Instance.audio.audioContainer.data.Values : null;
    }

    public void Start()
    {
        action.AddListener(() =>
        {
            if (audioFile && Manager.Instance.audio)
            {
                Manager.Instance.audio.PlaySound(audioFile);
                interactable = false;
            }
        });
        
        action.AddListener(() =>
        {
            //highlight the object briefly
            if (activeTexture) ChangeMaterial(activeTexture);
            
            //Check if the order is correct
            Manager.Instance.currentRoom.ValidateOrder(this);
            
            //Check if the system has completed
            Manager.Instance.currentRoom.IsCompleted();
        });

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
        interactable = true;
        yield return null;
    }
}
