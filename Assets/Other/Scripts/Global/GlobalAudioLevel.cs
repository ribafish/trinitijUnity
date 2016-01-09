using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class GlobalAudioLevel : MonoBehaviour {

    [Range(0.0f, 1.0f)] public static float musicVolume = 0.0f;
    [Range(0.0f, 1.0f)] public static float efxVolume = 0.0f;
    public AudioMixer mainAudioMixer;

    // Use this for initialization
    void Start () {
        mainAudioMixer.SetFloat("musicVolume" ,musicVolume);
        mainAudioMixer.SetFloat("efxVolume", efxVolume);
    }
	
	// Update is called once per frame
	void Update () {
        //musicVolume = musicAudioSource.volume;
	}

    public void SetMusicVolume(float newVolume)
    {
        if (newVolume <= -40) musicVolume = -100;
        else musicVolume = newVolume;
        mainAudioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetEfxVolume(float newVolume)
    {
        if (newVolume <= -40) efxVolume = -100;
        else efxVolume = newVolume;
        mainAudioMixer.SetFloat("efxVolume", efxVolume);
    }
    
}
