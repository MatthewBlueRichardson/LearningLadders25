using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [SerializeField] private float globalShakeForce = 1f;
    [SerializeField] private CinemachineImpulseListener impulseListener;
    
    private CinemachineImpulseDefinition impulseDefinition;
    
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }

    public void ScreenShakeFromProfile(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {
        // Apply settings
        SetupScreenShakeSettings(profile, impulseSource);

        // Shake screen
        impulseSource.GenerateImpulseWithForce(profile.impactForce);
    }

    private void SetupScreenShakeSettings(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
    {      
        // Change the impulse source settings
        impulseDefinition = impulseSource.ImpulseDefinition;
        impulseDefinition.ImpulseDuration = profile.impulseTime;
        impulseSource.DefaultVelocity = profile.defaultVelocity;
        impulseDefinition.CustomImpulseShape = profile.impulseCurve;

        // Change the impulse listener settings
        impulseListener.ReactionSettings.AmplitudeGain = profile.listenerAmplitude;
        impulseListener.ReactionSettings.FrequencyGain = profile.listenerFrequency;
        impulseListener.ReactionSettings.Duration = profile.listenerDuration;
    }
}
