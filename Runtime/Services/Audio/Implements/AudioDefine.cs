using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JamForge.Audio
{
    public enum LoopType
    {
        RoundRobin,
        Random
    }

    [CreateAssetMenu(fileName = "New AudioDefine", menuName = "JamForge/Create AudioDefine")]
    public class AudioDefine : ScriptableObject, IAudioSound
    {
        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] clips;

        [Header("Audio Settings")]
        [SerializeField] private bool loop;
        [SerializeField] private bool overrideVolume;
        [SerializeField] private bool overridePitch;
        [SerializeField] private LoopType loopType;

        [Header("Audio Values")]
        [SerializeField] private float crossFade = 0.1f;
        [SerializeField] private float volume = 1f;
        [SerializeField] private float pitchMin = 1f;
        [SerializeField] private float pitchMax = 1f;

        public bool Loop => loop;
        public float Volume => volume;
        public float Pitch => Random.Range(pitchMin, pitchMax);
        public float CrossFade => crossFade;
        public bool OverrideVolume => overrideVolume;
        public bool OverridePitch => overridePitch;

        private int ClipIndex { get; set; }

        public AudioClip GetClip()
        {
            if (clips.Length == 0) { return null; }

            switch (loopType)
            {
                case LoopType.RoundRobin:
                    ClipIndex = (ClipIndex + 1) % clips.Length;
                    break;
                case LoopType.Random:
                    ClipIndex = Random.Range(0, clips.Length);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            return clips[ClipIndex];
        }
    }
}
