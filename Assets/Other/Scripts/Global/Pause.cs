using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
    private bool isPaused = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Cancel"))
            PauseGame();
	
	}

    void PauseGame()
    {
        if (isPaused == true)
        {
            isPaused = false;
            Time.timeScale = 1.0f;  // Should resume game
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0.0f;  // Should pause game
        }
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
