using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

    static Globals instance;

    public SceneManager sceneManager;
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
