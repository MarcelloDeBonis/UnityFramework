using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class SoundManager : Singleton<SoundManager>
{
        #region Variables and properties

        [SerializeField] private GameObject self;

        #region BackGroundSceneMusic

        private string sceneName = new string("");
        
        [SerializeField] private bool sceneSounds=false;
        private bool activeSceneMusic = false;
        
        [SerializeField] private List<string> scenesNamesList;
        [SerializeField] private List<AudioClip> sceneAudioSourcesList;
        private Dictionary<string, AudioClip> dictinaryScenesSounds = new Dictionary<string, AudioClip>();
        private AudioSource sceneAudioSource;
        
        #endregion

        #region Musics and effects

        private List<AudioSource> musicSources= new List<AudioSource>();
        private List<AudioSource> effectSources = new List<AudioSource>();
        private List<AudioSource> musicLoopSources = new List<AudioSource>();
        private List<AudioSource> effectLoopSources = new List<AudioSource>();
        
        private AudioSource singleSource;

        #endregion

        #region Volumes

        [Range(0.0f, 1.0f)]
        [SerializeField] private float mainVolume = 0.5f;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float effectsVolume = 0.5f;
        
        [Range(0.0f, 1.0f)]
        [SerializeField] private float sceneAudioSourceVolume=0.5f;

        #endregion

        [SerializeField] private bool multipleSounds;
    #endregion
        
        #region Monobehaviour

        protected override void Awake()
        {
            base.Awake();

            if (!multipleSounds)
            {
                singleSource = self.AddComponent<AudioSource>();
            }

            if (sceneSounds)
            {
                for (int i=0 ; i<sceneAudioSourcesList.Count ; i++)
                {
                    dictinaryScenesSounds.Add(scenesNamesList[i] , sceneAudioSourcesList[i]);
                }

                CheckNewScene();
            }
            
            SetVolumesByPlayerPrefs();
        }

        private void Update()
        { 
            CheckNewScene();
           CheckMusicPlaying();
           SetVolumesByEditor();
        }

        #endregion

        #region Methods

        public void SetVolumesByPlayerPrefs()
        {
            if (PlayerPrefs.HasKey("mainVolume"))
            {
                mainVolume = PlayerPrefs.GetFloat("mainVolume");
            }

            if(PlayerPrefs.HasKey("effectsVolume"))
            {
                effectsVolume = PlayerPrefs.GetFloat("effectsVolume");
            }

            if (PlayerPrefs.HasKey("SceneAudioSourceVolume"))
            {
                sceneAudioSourceVolume = PlayerPrefs.GetFloat("SceneAudioSourceVolume");
            }
        }

        public void MuteSounds()
        {
            PlayerPrefs.SetFloat("mainVolume", mainVolume);
            mainVolume = 0;
        }

        public void MuteEffect()
        {
            
            PlayerPrefs.SetFloat("effectVolume", effectsVolume);
            effectsVolume = 0;
        }

        public void MuteSceneSound()
        {
            
            PlayerPrefs.SetFloat("SceneAudioSourceVolume", sceneAudioSourceVolume);
            sceneAudioSourceVolume = 0;
        }
        
        public void UnMuteSounds()
        {
            mainVolume = PlayerPrefs.GetFloat("mainVolume");
        }
        
        public void UnMuteEffect()
        {
            effectsVolume = PlayerPrefs.GetFloat("effectVolume");
        }

        public void UnMuteSceneSound()
        {
            sceneAudioSourceVolume = PlayerPrefs.GetFloat("SceneAudioSourceVolume");
        }
        
        public void SaveVolumesIntoPlayerPrefs()
        {
            PlayerPrefs.SetFloat("mainVolume", mainVolume);
            PlayerPrefs.SetFloat("effectVolume", effectsVolume);
            PlayerPrefs.SetFloat("SceneAudioSourceVolume", sceneAudioSourceVolume);
        }

        private void CheckNewScene()
        {
            if (sceneName != SceneManager.GetActiveScene().name)
            {
                sceneName = SceneManager.GetActiveScene().name;
                activeSceneMusic = false;
                
                foreach (string scene in dictinaryScenesSounds.Keys)
                {
                    if (sceneName == scene)
                    {
                        activeSceneMusic = true;
                    }
                }

                if (activeSceneMusic)
                {
                    Destroy(sceneAudioSource);
                    sceneAudioSource = new AudioSource();
                    sceneAudioSource = self.AddComponent<AudioSource>();
                    sceneAudioSource.clip=dictinaryScenesSounds[sceneName];
                    sceneAudioSource.volume = sceneAudioSourceVolume;
                    sceneAudioSource.Play();
                }
                else
                {
                    Destroy(sceneAudioSource);
                }
            }
        }

        private void SetVolumesByEditor()
        {
            for (int i = 0; i < musicSources.Count; i++)
            {
                musicSources[i].volume = mainVolume;
            }

            for (int i = 0; i < musicLoopSources.Count; i++)
            {
                musicLoopSources[i].volume = mainVolume;
            }
            
            for (int i = 0; i < effectSources.Count; i++)
            {
                effectSources[i].volume = effectsVolume;
            }

            for (int i = 0; i < effectLoopSources.Count; i++)
            {
                effectLoopSources[i].volume = effectsVolume;
            }
            
            if (sceneSounds && (activeSceneMusic))
            {
                sceneAudioSource.volume = sceneAudioSourceVolume;
            }

        }

        private void CheckMusicPlaying()
        {
            if (multipleSounds)
            {
                CheckFinishedSounds();
                CheckFinishedEffects();
            }
        }
        
        private void CheckFinishedSounds()
        {
            if (musicSources.Count != 0)
            {
                for (int i = 0; i < musicSources.Count; i++)
                {
                    if (!musicSources[i].isPlaying)
                    {
                        Destroy(musicSources[i]);
                        musicSources.Remove(musicSources[i]);
                    }
                }
            }

            if (musicLoopSources.Count != 0)
            {
                for (int i = 0; i < musicLoopSources.Count; i++)
                {
                    if (!musicLoopSources[i].isPlaying)
                    {
                        musicLoopSources[i].Play();
                    }
                }
            }
        }

        private void CheckFinishedEffects()
        {
            if (effectSources.Count != 0)
            {
                for (int i = 0; i < effectSources.Count; i++)
                {
                    if (!effectSources[i].isPlaying)
                    {
                        Destroy(effectSources[i]);
                        effectSources.Remove(effectSources[i]);
                    }
                }
            }
            
            if (effectLoopSources.Count != 0)
            {
                for (int i = 0; i < effectLoopSources.Count; i++)
                {
                    if (!effectLoopSources[i].isPlaying)
                    {
                        effectLoopSources[i].Play();
                    }
                }
            }
        }

        public void PlaySoundWithLoop(AudioClip clip)
    {
        if (multipleSounds)
        {
                

            AudioSource audioSource = self.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = mainVolume;
            audioSource.Play();
            musicLoopSources.Add(audioSource);
                
        }
        else
        {
            singleSource.Stop();
            singleSource.clip = clip;
            singleSource.volume = mainVolume;
            singleSource.Play();
        }
    }

    public void PlayEffectWithLoop(AudioClip clip)
    {
        if (multipleSounds)
        {
                
            AudioSource audioSource = self.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = effectsVolume;
            audioSource.Play();
            effectLoopSources.Add(audioSource);
                
        }
        else
        {
            singleSource.Stop();
            singleSource.clip = clip;
            singleSource.volume = effectsVolume;
            singleSource.Play();
        }
    }

    public void StopSoundWithLoop(AudioClip clip)
    {
        if (multipleSounds)
        {
            bool foundedInList = false;
            foreach (AudioSource source in musicLoopSources)
            {
                if (source.clip == clip)
                {
                    source.Stop();
                    Debug.Log(clip.name +" Stopped!");
                    foundedInList = true;
                    Destroy(source);
                    musicLoopSources.Remove(source);
                    break;
                }
            }

            if (!foundedInList)
            {
                Debug.Log("Didn't founded in list");
            }
        }
            
        else
        {
            if (singleSource.clip == clip)
            {
                singleSource.Stop();
                Debug.Log(clip.name +" Stopped!");
            }
            else
            {
                Debug.Log("Incorrect Clip");
            }
        }
    }

    public void StopEffectWithLoop(AudioClip clip)
    {
        if (multipleSounds)
        {
            bool foundedInList = false;
            foreach (AudioSource source in effectLoopSources)
            {
                if (source.clip == clip)
                {
                    source.Stop();
                    Debug.Log(clip.name +" Stopped!");
                    foundedInList = true;
                    Destroy(source);
                    effectLoopSources.Remove(source);
                    break;
                }
            }

            if (!foundedInList)
            {
                Debug.Log("Didn't founded in list");
            }
        }
        else
        {
            if (singleSource.clip == clip)
            {
                singleSource.Stop();
                Debug.Log(clip.name +" Stopped!");
            }
            else
            {
                Debug.Log("Incorrect clip");
            }
        }
    }
    
    public void PlaySound(AudioClip clip)
    {
        if (multipleSounds)
        {
                

            AudioSource audioSource = self.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = mainVolume;
            audioSource.Play();
            musicSources.Add(audioSource);
                
        }
        else
        {
            singleSource.Stop();
            singleSource.clip = clip;
            singleSource.volume = mainVolume;
            singleSource.Play();
        }
            
    }
    
        public void PlayEffect(AudioClip clip)
        {
            if (multipleSounds)
            {
                
                AudioSource audioSource = self.AddComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.volume = effectsVolume;
                    audioSource.Play();
                    effectSources.Add(audioSource);
                
            }
            else
            {
                singleSource.Stop();
                singleSource.clip = clip;
                singleSource.volume = effectsVolume;
                singleSource.Play();
            }
        }

        public void StopSound(AudioClip clip)
        {
            if (multipleSounds)
            {
                bool foundedInList = false;
                foreach (AudioSource source in musicSources)
                {
                    if (source.clip == clip)
                    {
                        source.Stop();
                        Debug.Log(clip.name +" Stopped!");
                        foundedInList = true;
                    }
                }

                if (!foundedInList)
                {
                    Debug.Log("Didn't founded in list");
                }
            }
            
            else
            {
                if (singleSource.clip == clip)
                {
                    singleSource.Stop();
                    Debug.Log(clip.name +" Stopped!");
                }
                else
                {
                    Debug.Log("Incorrect Clip");
                }
            }
        }

        public void StopEffect(AudioClip clip)
        {
            if (multipleSounds)
            {
                bool foundedInList = false;
                foreach (AudioSource source in effectSources)
                {
                    if (source.clip == clip)
                    {
                        source.Stop();
                        Debug.Log(clip.name +" Stopped!");
                        foundedInList = true;
                    }
                }

                if (!foundedInList)
                {
                    Debug.Log("Didn't founded in list");
                }
            }
            else
            {
                if (singleSource.clip == clip)
                {
                    singleSource.Stop();
                    Debug.Log(clip.name +" Stopped!");
                }
                else
                {
                    Debug.Log("Incorrect clip");
                }
            }
        }
        
        public void SetMainVolume(float volume)
        {
            mainVolume = Mathf.Clamp(volume, 0, 1);
        }

        public void SetEffectsVolume(float volume)
        {
            effectsVolume = Mathf.Clamp(volume, 0, 1);
        }

        public void SetBackgroundSoundVolume(float volume)
        {
            sceneAudioSourceVolume = Mathf.Clamp(volume, 0, 1);
        }

        public bool MainVolumeMuted()
        {
            return mainVolume == 0;
        }
        public bool EffectsVolumeMuted()
        {
            return effectsVolume == 0;
        }

        #endregion

}

