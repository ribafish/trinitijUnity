using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadPlot : MonoBehaviour {

    public string plotScene;

    public Scene newscene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton ("Cancel"))
        {
            if (plotScene != null)
                SceneManager.LoadScene(plotScene);
            else
                Debug.LogError("Can't find plot scene");
        }	
	}
}
