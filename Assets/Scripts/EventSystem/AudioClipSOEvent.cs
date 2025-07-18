using UnityEngine;
using LearningLadders.EventSystem;
using LearningLadders.Audio;

namespace LearningLadders.EventSystem
{
    [CreateAssetMenu(fileName = "AudioClipSO Event", menuName = "LearningLadders/Events/AudioClipSO Event")]
    public class AudioClipSOEvent : AbstractEvent<AudioClipSO> {}
}
