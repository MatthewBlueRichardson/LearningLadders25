using LearningLadders.EventSystem;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;
    [Header("Allow Screen Shake Effect")]
    public bool DoScreenShake = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public void Shake(ScreenShakeProfile shakeProfile)
    {
        if(DoScreenShake)
        {
            CameraShakeManager.instance.ScreenShakeFromProfile(shakeProfile, impulseSource); // Shake camera.
            Debug.Log("Shake!");
        }
    }
}
