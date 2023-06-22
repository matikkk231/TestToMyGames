using System;
using System.Collections.Generic;
using Project.Scripts.Base.AdoioService.View;
using UnityEngine;

namespace Project.Scripts.Base.AudioService.View
{
    public class AudioServiceView : MonoBehaviour, IAudioServiceView
    {
        private List<AudioSource> _activeSources;
        private List<AudioSource> _cachedSources;

        public void Awake()
        {
            _activeSources = new List<AudioSource>();
            _cachedSources = new List<AudioSource>();
        }

        private void FixedUpdate()
        {
            foreach (var source in _activeSources)
            {
                if (!source.isPlaying)
                {
                    _activeSources.Remove(source);
                    _cachedSources.Add(source);
                    break;
                }
            }
        }

        public void Play(AudioClip audioClip)
        {
            var source = getAudioSource();
            source.clip = audioClip;
            source.Play();
        }

        public void StopAll()
        {
            foreach (var activeSource in _activeSources)
            {
                activeSource.Stop();
            }
        }

        private AudioSource getAudioSource()
        {
            AudioSource audioSource;
            if (_cachedSources.Count != 0)
            {
                audioSource = _cachedSources[0];
                _cachedSources.Remove(audioSource);
                _activeSources.Add(audioSource);
                return audioSource;
            }

            audioSource = Instantiate(new GameObject()).AddComponent<AudioSource>();
            _activeSources.Add(audioSource);
            return audioSource;
        }
    }
}