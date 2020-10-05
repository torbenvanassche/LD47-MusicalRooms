using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageAnimator : MonoBehaviour
{
    public AnimationContainer sprites;
    private new List<Sprite> animation = new List<Sprite>();
    private bool firstLoop = true;

    private Image ren = null;

    private float currentTime = 0;
    private int currentIndex = 0;

    private void Start()
    {
        animation.AddRange(sprites.data);
        
        ren = GetComponent<Image>();
        if (sprites)
        {
            ren.sprite = sprites.data[currentIndex];   
        }
    }

    private void Update()
    {
        if (!sprites) return;
        
        currentTime += Time.deltaTime;

        if (currentTime >= sprites.animationSpeed)
        {
            ++currentIndex;
            currentTime = 0;

            if (currentIndex >= animation.Count)
            {
                currentIndex = 0;
                if (firstLoop)
                {
                    animation.RemoveRange(0, 7);
                    firstLoop = false;   
                }
            }

            ren.sprite = animation[currentIndex];
        }
    }
}
