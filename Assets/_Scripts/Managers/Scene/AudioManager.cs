using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Pistol,
        Lazer
    }

    [System.Serializable]
    public class Sound
    {
        public SoundType soundType;
        public AudioClip audioClip;

        [Range(0f, 1f)]
        public float volume = 0.2f;

        [HideInInspector]
        public AudioSource audioSource;
    }

    [System.Serializable]
    public class SoundsOfTypeList
    {
        public SoundType listSoundType;
        public Sound[] allSoundsOfType;
    }

    //Singleton
    public static AudioManager Instance;

    public SoundsOfTypeList[] allSounds;

    private Dictionary<SoundType, SoundsOfTypeList> _soundListsDictionary = new Dictionary<SoundType, SoundsOfTypeList>();

    private void Awake() 
    {
        Instance = this;

        foreach(SoundsOfTypeList soundsOfTypeList in allSounds)
        {
            _soundListsDictionary[soundsOfTypeList.listSoundType] = soundsOfTypeList;
        }

        Debug.LogWarning(_soundListsDictionary);
        Debug.LogWarning(allSounds);    
    }

    public void PlaySound(SoundType soundType)
    {
        if (!_soundListsDictionary.TryGetValue(soundType, out SoundsOfTypeList soundsOfTypeList))
        {
            Debug.LogWarning($"Sound type {soundType} not found");
            return;
        }

        Debug.Log(soundsOfTypeList);
        int randomSoundIndex = Random.Range(0, soundsOfTypeList.allSoundsOfType.Length);

        GameObject soundObject = new GameObject($"Sound_{soundType}");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        audioSource.clip = soundsOfTypeList.allSoundsOfType[randomSoundIndex].audioClip;
        audioSource.volume = soundsOfTypeList.allSoundsOfType[randomSoundIndex].volume;

        audioSource.Play();

        Destroy(soundObject, soundsOfTypeList.allSoundsOfType[randomSoundIndex].audioClip.length);
    }

}
