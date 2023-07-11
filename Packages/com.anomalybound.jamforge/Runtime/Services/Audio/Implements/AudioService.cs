using System.Collections.Generic;
using UnityEngine;

namespace JamForge.Audio
{
    public class AudioHandle : IAudioHandle
    {
        public bool IsPlaying { get; set; }

        public AudioSource AudioSource { get; }

        public AudioHandle(AudioSource audioSource)
        {
            AudioSource = audioSource;
        }

        public void Stop() { }

        public void Pause() { }

        public void Resume() { }
    }

    public class AudioService : IAudioService
    {
        public float Volume { get; private set; }

        public float Pitch { get; private set; }

        private List<AudioHandle> Handles { get; } = new();

        private AudioSource _sfxSource;
        private readonly Dictionary<string, AudioSource> _musicSource = new();

        private readonly Transform _root;
        private const string DefaultMusicChannel = "GeneralMusic";

        private AudioSource CreateAudioSource(string name)
        {
            var audioSource = new GameObject(name).AddComponent<AudioSource>();
            audioSource.transform.SetParent(_root, false);
            return audioSource;
        }

        public AudioService()
        {
            _root = new GameObject("JamForge AudioService").transform;
            _sfxSource = CreateAudioSource("SoundEffects");

            var musicSource = CreateAudioSource(DefaultMusicChannel);
            _musicSource.TryAdd(DefaultMusicChannel, musicSource);
        }

        public void Play(IAudioSoundEffect sfx) { }

        public void Play(IAudioSoundEffect sfx, Vector3 position) { }

        public IAudioHandle Play(IAudioMusic music, string channel = null)
        {
            throw new System.NotImplementedException();
        }

        public IAudioHandle CrossFade(IAudioMusic music, string channel = null)
        {
            throw new System.NotImplementedException();
        }

        public void SetGlobalVolume(float volume)
        {
            Volume = volume;
        }

        public void SetGlobalPitch(float pitch)
        {
            Pitch = pitch;
        }

        public void StopAll()
        {
            for (var i = 0; i < Handles.Count; i++)
            {
                Handles[i].Stop();
            }
        }

        public void PauseAll()
        {
            for (var i = 0; i < Handles.Count; i++)
            {
                Handles[i].Pause();
            }
        }

        public void ResumeAll()
        {
            for (var i = 0; i < Handles.Count; i++)
            {
                Handles[i].Resume();
            }
        }
    }
}
