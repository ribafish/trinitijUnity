using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip newMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        GameObject go = GameObject.Find("GameMusic");
        go.GetComponent<AudioSource>().clip = newMusic;
        go.GetComponent<AudioSource>().Play();
    }
}
