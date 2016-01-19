using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OutroControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Globals.instance.sceneManager.SwitchToScene(0);
        }
	}
}
