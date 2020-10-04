using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public AnimationContainer sprites;
    private SpriteRenderer ren = null;

    private float currentTime = 0;
    private int currentIndex = 0;

    private void Start()
    {
        ren = GetComponent<SpriteRenderer>();
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

            if (currentIndex >= sprites.data.Count) currentIndex = 0;

            ren.sprite = sprites.data[currentIndex];
        }
    }
}
