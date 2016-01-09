using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalAudioLevel : MonoBehaviour {

    [Range(0.0f, 1.0f)] public static float musicVolume = 0.5f;
    [Range(0.0f, 1.0f)] public static float efxVolume = 0.5f;
    public AudioSource musicAudioSource;
    public AudioSource efxAudioSource;

    // Use this for initialization
    void Start () {
        musicAudioSource.volume = musicVolume;
        efxAudioSource.volume = efxVolume;
	}
	
	// Update is called once per frame
	void Update () {
        //musicVolume = musicAudioSource.volume;
	}

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        musicAudioSource.volume = musicVolume;
    }

    public void SetEfxVolume(float newVolume)
    {
        efxVolume = newVolume;
        efxAudioSource.volume = efxVolume;
    }
    
}
