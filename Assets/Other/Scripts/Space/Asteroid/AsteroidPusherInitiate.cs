using UnityEngine;
using System.Collections;

public class AsteroidPusherInitiate : MonoBehaviour {
    public Vector3 asteroidMove;
    public float asteroidSpeed;
    public bool destroyOverTime = false;
    public float destroyDistance = 0;

    void Start()
    {
        asteroidSpeed = 100;
        transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        asteroidMove = Vector3.forward;
    }

    void FixedUpdate()
    {
        if (destroyOverTime)
        {
            transform.position -= new Vector3(0, 0, asteroidSpeed * Time.deltaTime);
            //Debug.Log(transform.position);

            if (transform.position.z < destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    public void setOverTimeDestroy(float targetZDistance)
    {
        destroyOverTime = true;
        destroyDistance = targetZDistance;
        Destroy(gameObject, 20);
    }
}
