using UnityEngine;
using System.Collections;

public class AsteroidFieldCreator : MonoBehaviour {

    public Vector3 areaSize = Vector3.zero;
    public GameObject[] asteroids = null;
    public int[] asteroidsAmount = null;
    public Vector2[] asteroidsSizeRange = null;

	void Start () {
        float width = areaSize.x;
        float height = areaSize.y;
        float depth = areaSize.z;

        if (asteroids != null && asteroidsAmount != null && asteroidsSizeRange != null)
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
                    asteroid.transform.rotation = Random.rotation;
                    asteroid.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
                    asteroid.transform.parent = transform;
                }
            }
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
