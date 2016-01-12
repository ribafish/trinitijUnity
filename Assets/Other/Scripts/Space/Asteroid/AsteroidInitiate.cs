using UnityEngine;
using System.Collections;

public class AsteroidInitiate : MonoBehaviour
{
    public int speedRange = 0;
    public int speedRotation = 0;

    Vector3 asteroidMove;
    float asteroidSpeed;
    float astroidRoation;

    public bool destroyOverTime = false;
    public float destroyDistance = 0;
    bool reset = false;

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
        /*float x = transform.rotation.x;
        float y = transform.rotation.y;
        float z = transform.rotation.z;
        float w = transform.rotation.w;
        x = x + x * astroidRoation;
        y = y + y * astroidRoation;
        z = z + z * astroidRoation;
        transform.rotation.Set(x, y, z, w);*/

        if (destroyOverTime)
        {
            if(!reset)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                asteroidMove = Vector3.forward;
                asteroidSpeed = 100;
                reset = true;
            }
            transform.position -= new Vector3(0, 0, asteroidSpeed * Time.deltaTime);
            //Debug.Log(transform.position);

            if (transform.position.z < destroyDistance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += asteroidMove * asteroidSpeed;
        }
    }

    public void setOverTimeDestroy(float targetZDistance)
    {
        destroyOverTime = true;
        destroyDistance = targetZDistance;
    }
}