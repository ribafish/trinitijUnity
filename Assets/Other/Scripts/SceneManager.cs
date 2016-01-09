﻿using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    static SceneManager Instance;

	// Use this for initialization
	void Start ()
    {
	    if (Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }
	}
	
	public void SwitchToScene (int sceneNum)
    {
        Application.LoadLevel(sceneNum);
    }

    public void SwitchToScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
