using UnityEngine;
using System.Collections;

public class AsteroidInitiate : MonoBehaviour
{
    public int speedRange = 0;
    public int speedRotation = 0;

    Vector3 asteroidMove;
    float asteroidSpeed;
    float astroidRoation;

    void Start()
    {
        asteroidSpeed = Random.value * Random.Range(0, speedRange);
        astroidRoation = Random.value * Random.Range(0, speedRotation);
        int randNum = Random.Range(0,100);

        switch(randNum)
        {
            case 1:
                asteroidMove = new Vector3(1, 0, 0);
                break;
            case 2:
                asteroidMove = new Vector3(0, 1, 0);
                break;
            case 3:
                asteroidMove = new Vector3(0, 0, 1);
                break;
            /*case 4:
                asteroidMove = new Vector3(0, 1, 1);
                break;
            case 5:
                asteroidMove = new Vector3(1, 1, 0);
                break;
            case 6:
                asteroidMove = new Vector3(1, 0, 1);
                break;
            case 7:
                asteroidMove = Vector3.one;
                break;*/
            default:
                asteroidMove = Vector3.zero;
                break;
        }

        transform.rotation = Random.rotation;
    }

    void FixedUpdate()
    {
        transform.position += asteroidMove * asteroidSpeed;
        float x = transform.rotation.x;
        float y = transform.rotation.y;
        float z = transform.rotation.z;
        float w = transform.rotation.w;
        x = x + x * astroidRoation;
        y = y + y * astroidRoation;
        z = z + z * astroidRoation;
        transform.rotation.Set(x, y, z, w);
    }
}