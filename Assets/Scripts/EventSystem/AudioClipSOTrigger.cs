using UnityEngine;
using LearningLadders.Audio;

namespace LearningLadders.EventSystem
{
    public class AudioClipSOEventTrigger : AbstractEventTrigger<AudioClipSO>
    {
        [SerializeField] private AudioClipSO audioClip;
        public void Trigger() => eventToTrigger.Invoke(audioClip);
    }
}