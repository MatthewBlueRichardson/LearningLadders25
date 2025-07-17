using UnityEngine;

namespace LearningLadders.Audio
{
    [CreateAssetMenu(fileName = "Audio Clip", menuName = "LearningLadders/Audio Clip")]
    public class AudioClipSO : ScriptableObject
    {
        public AudioClip AudioClip;
        [Range(0f, 1f)] public float volume = 1f;
    }
}