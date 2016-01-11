using UnityEditor;
using UnityEngine;
using System.Collections;

public class AsteroidCreator : MonoBehaviour
{
    public float areaSize = 0.0f;
    public GameObject[] asteroids = null;
    public int[] asteroidsAmount = null;
    public Vector2[] asteroidsSizeRange = null;

    public enum RandomChoice {INSIDE_SPHERE, ON_SPEHERE};
    public RandomChoice randomChoice;

    void Start ()
    {
        if (areaSize > 0 && asteroids != null && asteroidsAmount != null)
        {
            Vector3 randomPosition = Vector3.zero;
            float minSize = 0;
            float maxSize = 0;

            for(int i = 0; i < asteroids.Length; i++)
            {
                minSize = asteroidsSizeRange[i].x;
                maxSize = asteroidsSizeRange[i].y;

                for(int j = 0; j < asteroidsAmount[i]; j++)
                {
                    switch (randomChoice)
                    {
                        case RandomChoice.INSIDE_SPHERE:
                            randomPosition = Random.insideUnitSphere * areaSize;
                            break;
                        case RandomChoice.ON_SPEHERE:
                            randomPosition = Random.onUnitSphere * areaSize;
                            break;
                    }

                    GameObject asteroid = Instantiate<GameObject>(asteroids[i]);
                    asteroid.transform.position = transform.position + randomPosition;
                    asteroid.transform.rotation = Random.rotation;
                    asteroid.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
                }
            }
        }
    }

    //Za lazjo vizualizacijo in izbire

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, areaSize);
    }
}