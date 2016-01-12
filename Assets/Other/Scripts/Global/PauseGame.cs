using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    private bool isPaused = false;
    public Menu pauseMenu;
    public static PauseGame instance;

    // Use this for initialization
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Pause();

    }

    public void Pause()
    {
        AudioSource engineSound = GameObject.Find("Engine Sound").GetComponent<AudioSource>();

        if (isPaused == true)
        {
            isPaused = false;
            Globals.instance.menuManager.closeCurrentMenu();
            engineSound.Play();

            Time.timeScale = 1.0f;  // Should resume game
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0.0f;  // Should pause game

            Globals.instance.menuManager.ShowMenu(pauseMenu);
            engineSound.Pause();
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
