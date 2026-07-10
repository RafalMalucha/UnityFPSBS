using System.Collections.Generic;
using UnityEngine;

public class SoundscapeManager : MonoBehaviour
{
    public enum SoundscapeType
    {
        Ambient,
        Arena
    }

    [System.Serializable]
    public class Soundscape
    {
        public SoundscapeType soundscapeType;
        public AudioClip audioClip;

        [Range(0f, 1f)]
        public float volume = 0.05f;

        [HideInInspector]
        public AudioSource audioSource;
    }

    [System.Serializable]
    public class SoundscapesOfTypeList
    {
        public SoundscapeType listSoundscapeType;
        public Soundscape[] allSoundscapesOfType;
    }

    //Singleton
    public static SoundscapeManager Instance;

    public SoundscapesOfTypeList[] allSoundscapes;

    private Dictionary<SoundscapeType, SoundscapesOfTypeList> _soundscapeListsDictionary = new Dictionary<SoundscapeType, SoundscapesOfTypeList>();

    public SoundscapeType currentSoundscape = 0;


    void Start()
    {
        Instance = this;

        foreach(SoundscapesOfTypeList soundscapesOfTypeList in allSoundscapes)
        {
            _soundscapeListsDictionary[soundscapesOfTypeList.listSoundscapeType] = soundscapesOfTypeList;
        }

        SoundscapeManager.Instance.PlaySoundscape(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundscape(SoundscapeType currentSoundscape)
    {
        Destroy(GameObject.Find("Soundscape_Ambient"));
        Destroy(GameObject.Find("Soundscape_Arena"));

        if (!_soundscapeListsDictionary.TryGetValue(currentSoundscape, out SoundscapesOfTypeList soundscapesOfTypeList))
        {
            Debug.LogWarning($"Soundscape type {currentSoundscape} not found");
            return;
        }

        GameObject soundObject = new GameObject($"Soundscape_{currentSoundscape}");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        audioSource.clip = soundscapesOfTypeList.allSoundscapesOfType[0].audioClip;
        audioSource.volume = soundscapesOfTypeList.allSoundscapesOfType[0].volume;

        audioSource.Play();

        Destroy(soundObject, soundscapesOfTypeList.allSoundscapesOfType[0].audioClip.length);
    }
}
