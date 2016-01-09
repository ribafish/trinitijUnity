using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Globals : MonoBehaviour {

    static Globals instance;

    public MySceneManager sceneManager;
    public GlobalAudioLevel globalAudioLevel;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
	}
}
