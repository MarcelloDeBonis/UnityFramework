using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
        [SerializeField] private AudioSource musicSource;
        
        private float mainVolume = 1.0f;
        private float effectsVolume = 1.0f;

        public void PlaySound(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.volume = mainVolume;
            musicSource.Play();
        }

        public void PlayEffect(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.volume = effectsVolume;
            musicSource.Play();
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
