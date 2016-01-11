using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Globals : MonoBehaviour {

    public static Globals instance;

    public MySceneManager sceneManager;
    public GlobalAudioLevel globalAudioLevel;
    public PauseGame pauseGame;
    public MenuManager menuManager;

	// Use this for initialization
	void Start ()
    {
        //DontDestroyOnLoad(gameObject);

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
