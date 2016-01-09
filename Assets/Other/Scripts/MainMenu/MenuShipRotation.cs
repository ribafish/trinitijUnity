using UnityEngine;
using System.Collections;

public class MenuShipRotation : MonoBehaviour {

    [Range(0, 20)] public int rotationSpeed = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Earth rotation
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }

    public void SetRotationSpeed(int speed)
    {
        rotationSpeed = speed;
    }
}