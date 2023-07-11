using UnityEngine;

namespace JamForge.Audio
{
    public interface IAudioHandle
    {
        bool IsPlaying { get; }

        void Stop();

        void Pause();

        void Resume();
    }

    public interface IGetAudioClip
    {
        AudioClip GetClip();
    }

    public interface IAudioBase
    {
        float Volume { get; }

        float Pitch { get; }
    }

    public interface IAudioMusic : IAudioBase, IGetAudioClip { }

    public interface IAudioSoundEffect : IAudioBase, IGetAudioClip { }

    public interface IAudioService : IAudioBase
    {
        public void Play(IAudioSoundEffect sfx);

        public void Play(IAudioSoundEffect sfx, Vector3 position);

        public IAudioHandle Play(IAudioMusic music, string channel = null);

        public IAudioHandle CrossFade(IAudioMusic music, string channel = null);

        public void SetGlobalVolume(float volume);

        public void SetGlobalPitch(float pitch);

        public void StopAll();

        public void PauseAll();

        public void ResumeAll();
    }
}
