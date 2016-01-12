using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsteroidPusher : MonoBehaviour {

    public float minDistance = 1000;
    public Vector3 areaSize = Vector3.zero;
    public GameObject[] asteroids = null;
    public int[] asteroidsAmount = null;
    public Vector2[] asteroidsSizeRange = null;
    public GameObject playerObj = null;
    public Text distanceText = null;


    public float distance = 6000;
    float width = 0;
    float height = 0;
    float depth = 0;

    public float setSpawnTime = 30;
    public float timerTillSpawn = 0;
    float jumpTime = 6;

	void Start () {
        width = areaSize.x;
        height = areaSize.y;
        depth = areaSize.z;

        distance = distance + transform.position.z;

        if (asteroids != null && asteroidsAmount != null && asteroidsSizeRange != null && playerObj != null && distanceText != null)
        {
            generateNewPushers();

            distanceText.text = Mathf.RoundToInt(distance) + "m";
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, transform.position.z);

        if ((transform.position.z - playerObj.transform.position.z) < minDistance)
        {
            transform.position += Vector3.forward * 200 * Time.deltaTime;
        }

        if (transform.position.z > distance)
        {
            if (jumpTime > 0)
            {
                distanceText.text = "Engaging jump in " + jumpTime + "s";
            }
            else
            {
                distanceText.text = "Jumping!";
            }

            if(jumpTime < 0)
            {
                Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex + 1);
            }

            jumpTime -= 1 * Time.deltaTime;
            Camera.main.fieldOfView += 18 * Time.deltaTime;
        }
        else
        {
            if (timerTillSpawn <= 0)
            {
                generateNewPushers();
                timerTillSpawn = setSpawnTime;
            }
            else
            {
                timerTillSpawn -= 1 * Time.deltaTime;
            }

            distanceText.text = Mathf.RoundToInt(distance - transform.position.z) + "m";
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 10));
    }

    void generateNewPushers()
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
                float z = Random.Range(0, depth / 2) * Mathf.Pow(-1, Random.Range(1, 3));

                randomPosition = new Vector3(x, y, z);

                GameObject asteroid = Instantiate<GameObject>(asteroids[i]);
                asteroid.transform.position = transform.position + randomPosition;
                asteroid.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
                //asteroid.transform.parent = transform;
                asteroid.GetComponent<AsteroidPusherInitiate>().setOverTimeDestroy(playerObj.transform.position.z);
                asteroid.name = "PUSHER";
            }
        }
    }
}
