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
        GameObject engine = GameObject.Find("Engine Sound");
        AudioSource engineSound = null;

        if (engine != null)
            engineSound = engine.GetComponent<AudioSource>();

        if (isPaused == true)
        {
            isPaused = false;
            Globals.instance.menuManager.closeCurrentMenu();
            if(engineSound != null)
                engineSound.Play();

            Time.timeScale = 1.0f;  // Should resume game
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0.0f;  // Should pause game

            Globals.instance.menuManager.ShowMenu(pauseMenu);
            if (engineSound != null)
                engineSound.Pause();
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
