using JamForge;
using JamForge.Audio;
using UnityEngine;

public class AudioExample : MonoBehaviour
{
    [SerializeField] private AudioDefine audioDefine;
    [SerializeField] private AudioDefine audioDefineEffect;

    private IAudioHandle _audioHandle;
    
    private void Start()
    {
        _audioHandle = Jam.Audio.Play(audioDefine);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jam.Audio.PlayOneShot(audioDefineEffect);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_audioHandle.IsPlaying)
            {
                _audioHandle.Pause();   
            }
            else
            {
                _audioHandle.Resume();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _audioHandle.Stop();
        }
    }
}
