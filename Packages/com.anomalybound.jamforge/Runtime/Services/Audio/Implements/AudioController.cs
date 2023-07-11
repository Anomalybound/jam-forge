﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Scripting;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace JamForge.Audio
{
    [Preserve]
    public class AudioController : IAudioController, ITickable
    {
        public float Volume { get; private set; } = 1f;

        public float Pitch { get; private set; } = 1f;

        private List<AudioHandle> Handles { get; } = new();

        private readonly Dictionary<string, AudioSource> _soundSources = new();

        private readonly Transform _root;
        private readonly IObjectPool<AudioSource> _audioSourcePool;
        private const string DefaultSoundChannel = "GeneralSound";
        private const string DefaultMusicChannel = "GeneralMusic";

        public AudioController()
        {
            _audioSourcePool = new ObjectPool<AudioSource>(
                CreateAudioSource,
                audioSource => audioSource.gameObject.SetActive(true),
                audioSource => audioSource.gameObject.SetActive(false),
                audioSource => Object.Destroy(audioSource.gameObject),
                true,
                50
            );

            _root = new GameObject("JamForge AudioService").transform;
            _soundSources.TryAdd(DefaultSoundChannel, CreateAudioSource(DefaultSoundChannel));
            _soundSources.TryAdd(DefaultMusicChannel, CreateAudioSource(DefaultMusicChannel));
        }

        public void Tick()
        {
            for (var i = Handles.Count - 1; i >= 0; i--)
            {
                Handles[i].Tick();

                if (!Handles[i].IsValid)
                {
                    Handles.RemoveAt(i);
                }
            }
        }

        public void PlayOneShot(IAudioSound sound)
        {
            if (!_soundSources.TryGetValue(DefaultSoundChannel, out var audioSource))
            {
                audioSource = CreateAudioSource(DefaultSoundChannel);
                _soundSources.TryAdd(DefaultSoundChannel, audioSource);
            }

            var audioClip = sound.GetClip();
            if (!audioClip)
            {
                throw new Exception($"AudioClip is null. Sound: {sound}");
            }

            var volume = Volume;
            if (sound.OverrideVolume)
            {
                volume = sound.Volume;
            }

            if (sound.OverridePitch)
            {
                audioSource.pitch = sound.Pitch;
            }

            audioSource.PlayOneShot(audioClip, volume);
            audioSource.pitch = Pitch;
        }

        public void PlayOneShot(IAudioSound sound, Vector3 position)
        {
            var audioClip = sound.GetClip();
            if (!audioClip)
            {
                throw new Exception($"AudioClip is null. Sound: {sound}");
            }

            var volume = Volume;
            if (sound.OverrideVolume)
            {
                volume = sound.Volume;
            }

            if (sound.OverridePitch)
            {
                InternalLog.W($"Pitch is not supported in PlayOneShot with position. Sound: {sound}");
            }

            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }

        public IAudioHandle Play(IAudioSound sound, string channel = null)
        {
            channel ??= DefaultMusicChannel;

            if (!_soundSources.TryGetValue(channel, out var audioSource))
            {
                audioSource = CreateAudioSource(channel);
                _soundSources.TryAdd(channel, audioSource);
            }

            var audioClip = sound.GetClip();
            if (!audioClip)
            {
                throw new Exception($"AudioClip is null. Sound: {sound}");
            }

            return new AudioHandle(audioSource, sound, _audioSourcePool).AddTo(Handles);
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

        private AudioSource CreateAudioSource()
        {
            var audioSource = new GameObject("New AudioSource").AddComponent<AudioSource>();
            audioSource.transform.SetParent(_root, false);
            return audioSource;
        }

        private AudioSource CreateAudioSource(string name)
        {
            var audioSource = _audioSourcePool.Get();
            if (!audioSource) { throw new Exception("Failed to get AudioSource from the pool."); }

            audioSource.name = name;
            audioSource.pitch = Pitch;
            audioSource.volume = Volume;
            return audioSource;
        }
    }
}
