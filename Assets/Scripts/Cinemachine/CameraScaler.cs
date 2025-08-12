using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private CinemachineCamera cineCam;
    [SerializeField] private float zoomIncrementSize = 1f;
    [SerializeField] private float zoomDuration = 0.5f;
    [SerializeField] private float minSize = 7f;
    [SerializeField] private float maxSize = 15f;

    private int previousHeight = 0;

    private Tween currentTween;

    public bool ZoomOutStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cineCam = GetComponent<CinemachineCamera>();
        previousHeight = 0;

        if (ZoomOutStart)
        {
            cineCam.Lens.OrthographicSize = 3.5f;
            Invoke("ZoomOutFromStart", 0.2f);
        }
    }

    // The connection event passes in the height of the last stacked object.
    public void SetZoomByHeight(int _height)
    {
        // Height can't be less than zero!
        if (_height >= 0) 
        {
            UpdateHeight(_height);
        }
    }

    // This function uses DoTween to smoothly update the camera lens size based on the difference in height.
    public void UpdateHeight(int newHeight)
    {
        int difference = newHeight - previousHeight;

        // If there's a difference in height...
        if (difference > 0)
        {
            // Calculate new target size based on delta
            float targetSize = cineCam.Lens.OrthographicSize + difference * zoomIncrementSize;

            // Clamp target size to min and max
            targetSize = Mathf.Clamp(targetSize, minSize, maxSize);

            // Kill tween - Stops weird cutting effect happening with overlapping the zoom animation.
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill();
            }

            // Tween the orthographic size smoothly to the new target size
            currentTween = DOTween.To(() =>
            {
                return cineCam.Lens.OrthographicSize; // This is what we want to change!
            },
            x =>
            {
                var lens = cineCam.Lens;
                lens.OrthographicSize = x; // This is what we want the value to be!
                cineCam.Lens = lens;
            },
            targetSize,
            zoomDuration);
        }

        // Update previousHeight.
        previousHeight = newHeight;
    }

    public void ZoomOutFromStart()
    {
        // Kill tween - Stops weird cutting effect happening with overlapping the zoom animation.
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        // Tween the orthographic size smoothly out to the minimum lens size.
        currentTween = DOTween.To(() =>
        {
            return cineCam.Lens.OrthographicSize; // This is what we want to change!
        },
        x =>
        {
            var lens = cineCam.Lens;
            lens.OrthographicSize = x; // This is what we want the value to be!
            cineCam.Lens = lens;
        },
        minSize,
        1.5f);
    }
}
