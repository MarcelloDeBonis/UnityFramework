using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
        private List<AudioSource> musicSources= new List<AudioSource>();
        private List<AudioSource> effectSources = new List<AudioSource>();
        
        private AudioSource singleSource = new AudioSource();
        
        private float mainVolume = 0.5f;
        private float effectsVolume = 0.5f;

        [SerializeField] private bool multipleSounds;

        #region Monobehaviour

        private void Update()
        {
            if (multipleSounds && musicSources.Count!=0)
            {
                for (int i = 0; i < musicSources.Count; i++)
                {
                    if (!musicSources[i].isPlaying)
                    {
                        musicSources.Remove(musicSources[i]);
                    }
                }
            }
           
        }

        #endregion
        
        public void PlaySound(AudioClip clip)
        {
            if (multipleSounds)
            {
                if (musicSources.Count != 0)
                {
                    AudioSource audioSource = new AudioSource();
                    audioSource.clip = clip;
                    audioSource.volume = mainVolume;
                    singleSource.Play();
                    musicSources.Add(singleSource);
                }
            }
            else
            {
                singleSource.clip = clip;
                singleSource.volume = mainVolume;
                singleSource.Play();
            }
            
        }

        public void PlayEffect(AudioClip clip)
        {
            if (multipleSounds)
            {
                if (effectSources.Count != 0)
                {
                    AudioSource audioSource = new AudioSource();
                    audioSource.clip = clip;
                    audioSource.volume = effectsVolume;
                    singleSource.Play();
                    effectSources.Add(singleSource);
                }
            }
            else
            {
                singleSource.clip = clip;
                singleSource.volume = effectsVolume;
                singleSource.Play();
            }
        }

        public void SetMainVolume(float volume)
        {
            mainVolume = volume;
        }

        public void SetEffectsVolume(float volume)
        {
            effectsVolume = volume;
        }

}
