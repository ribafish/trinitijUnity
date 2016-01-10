using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    static MySceneManager Instance;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            //GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    public void SwitchToScene(int sceneNum)
    {
        //Application.LoadLevel(sceneNum);
        SceneManager.LoadScene(sceneNum);
    }

    public void SwitchToScene(string sceneName)
    {
        //Application.LoadLevel(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
