using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] frames; // An array of all the sprite frames for the animation.
    public float frameRate = 0.5f; // The speed between each sprite frames.

    private SpriteRenderer spriteRenderer;
    private int frameIndex;
    private float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime; // Time since last frame.

        if (timer >= frameRate)
        {
            frameIndex = (frameIndex + 1) % frames.Length; // Cycles through each sprite frame and loops ('%' returns 0 at end).
            spriteRenderer.sprite = frames[frameIndex]; // Set sprite to current sprite frame.
            timer = 0f; // Reset and start counting time from this frame.
        }
    }
}
