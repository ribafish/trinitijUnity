using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsteroidPusher : MonoBehaviour {

    public Vector2 areaSize = Vector2.zero;
    public GameObject[] asteroids = null;
    public int[] asteroidsAmount = null;
    public Vector2[] asteroidsSizeRange = null;
    public GameObject playerObj = null;
    public Text distanceText = null;

    float distance = 14000;

    float timerSpwan = 10;

	void Start () {
        float width = areaSize.x;
        float height = areaSize.y;

        if (asteroids != null && asteroidsAmount != null && asteroidsSizeRange != null && playerObj != null && distanceText != null)
        {
            Vector3 randomPosition = Vector3.zero;
            float minSize = 0;
            float maxSize = 0;

            for (int i = 0; i < asteroids.Length; i++)
            {
                minSize = asteroidsSizeRange[i].x;
                maxSize = asteroidsSizeRange[i].y;

                for (int j = 0; j < asteroidsAmount[i]; j++)
                {
                    float x = Random.Range(0, width / 2) * Mathf.Pow(-1, Random.Range(1, 3));
                    float y = Random.Range(0, height / 2) * Mathf.Pow(-1, Random.Range(1, 3));

                    randomPosition = new Vector3(x, y, 0);

                    GameObject asteroid = Instantiate<GameObject>(asteroids[i]);
                    asteroid.transform.position = transform.position + randomPosition;
                    asteroid.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
                    asteroid.GetComponent<AsteroidInitiate>().setOverTimeDestroy(playerObj.transform.position.z);
                    asteroid.name = "PUSHER";
                }
            }

            distanceText.text = Mathf.RoundToInt(distance) + "m";
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, transform.position.z);

        if ((transform.position.z - playerObj.transform.position.z) < 2000)
        {
            transform.position += Vector3.forward * 30 * Time.deltaTime;
        }

        if(transform.position.z > 14000)
        {
            Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex + 1);
        }

        distanceText.text = Mathf.RoundToInt(distance - transform.position.z) + "m";
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 10));
    }
}
