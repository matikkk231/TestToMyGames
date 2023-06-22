using UnityEngine;

namespace Project.Scripts.Base.AdoioService.View
{
    public interface IAudioServiceView
    {
        void Play(AudioClip audioClip);

        void StopAll();
    }
}