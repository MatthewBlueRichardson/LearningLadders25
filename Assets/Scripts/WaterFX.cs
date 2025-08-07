using DG.Tweening;
using LearningLadders.Audio;
using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterFX : MonoBehaviour
{
    [Header("Post Processing Volume")]
    [SerializeField] private Volume waterVolume;

    [Header("Audio")]
    [SerializeField] private AudioClipSO waterImpactSFX;

    [Header("Events")]
    [SerializeField] private BoolEvent onLowPassEnabledEvent;
    [SerializeField] private AudioClipSOEvent onPlaySfxEvent;


    private Tween currentTween;

    private void Start()
    {
        if (waterVolume != null)
        {
            waterVolume.weight = 0.0f;
        }

        onLowPassEnabledEvent.Invoke(false);
    }

    // Upon entering the water...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enable post processing
            TweenVolumeVFX(1);

            // Enable low pass filter on audio manager
            onLowPassEnabledEvent.Invoke(true);

            // Play SFX
            onPlaySfxEvent.Invoke(waterImpactSFX);
        }
    }

    // Upon exiting the water...
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Disable post processing
            TweenVolumeVFX(0);

            // Disable low pass filter on audio manager
            onLowPassEnabledEvent.Invoke(false);
        }
    }

    private void TweenVolumeVFX(float targetWeight)
    {
        if (waterVolume == null) { return; }

        targetWeight = Mathf.Clamp01(targetWeight);

        // Kill tween - Stops weird cutting effect happening with overlapping the zoom animation.
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        // Tween the orthographic size smoothly out to the minimum lens size.
        currentTween = DOTween.To(() =>
        {
            return waterVolume.weight; // This is what we want to change!
        },
        x =>
        {
            waterVolume.weight = x; // This is what we want the value to be!
        },
        targetWeight,
        0.5f);
    }
}
