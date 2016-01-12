using UnityEngine;
using System.Collections;

public class HyperJumpOut : MonoBehaviour {

    float timerOutJump = 6;
    bool jumDone = false;

	void Start () {
        Camera.main.fieldOfView = 160.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!jumDone)
        {
            if (timerOutJump == 6)
                Camera.main.fieldOfView = 160.0f;

            if (timerOutJump > 0)
            {
                timerOutJump -= 1 * Time.deltaTime;
                Camera.main.fieldOfView -= 18 * Time.deltaTime;
            }
            else
            {
                Camera.main.fieldOfView = 60.0f;
                jumDone = true;
                Destroy(gameObject);
            }
        }
	}
}
