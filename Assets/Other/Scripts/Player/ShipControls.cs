using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Audio;

public class ShipControls : MonoBehaviour {
	public float mouseSensibility = 3;
	public Transform shipCamera;
	public float thrustMax = 100;
    [Range(0.2f, 1.0f)] public float engineVolumeMax = 0.5f;
    public AudioSource engineSound;

	private float thrust = 0;
	private Quaternion oldRotation;
	private Vector3 oldPosition;
	private Rigidbody rigidBody;
	private ArrayList busters = new ArrayList ();

	// Use this for initialization
	void Start () {
		oldRotation = shipCamera.localRotation;
		oldPosition = shipCamera.localPosition;
		rigidBody = transform.GetComponent<Rigidbody> ();
		foreach (Transform c in transform) {
			if (c.tag == "Buster") {
				busters.Add (c.GetComponent<ParticleSystem>());
			}
		}
        engineSound.volume = 0.0f;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Globals.instance.pauseGame.IsPaused())
        {
            if (Input.GetButton("ThrustUp"))
            {
                thrust = Mathf.Clamp01(thrust + 0.01f);
            }
            if (Input.GetButton("ThrustDown"))
            {
                thrust = Mathf.Clamp01(thrust - 0.01f);
            }
            engineSound.volume = thrust * engineVolumeMax;
            engineSound.pitch = thrust / 2 + 0.5f;



            rigidBody.AddRelativeForce(Vector3.forward * thrustMax * thrust);
            Vector3 speed = transform.InverseTransformDirection(rigidBody.velocity);

            /* CAMERA EFECTS */
            float speedPercent = (speed.z / thrustMax);
            shipCamera.localPosition = Vector3.Lerp(shipCamera.localPosition, oldPosition + new Vector3(0, 0, -10 * speedPercent), 2);

            shipCamera.localPosition += new Vector3(Mathf.PerlinNoise(transform.localPosition.z / thrustMax * 10, 0) * speedPercent, Mathf.PerlinNoise(transform.localPosition.z / thrustMax * 10, 20) * speedPercent, 0);


            foreach (ParticleSystem ps in busters)
            {
                ps.Emit((int)(10 * thrust));
                //ps.startSpeed = 30 * thrust;
            }

            VignetteAndChromaticAberration vcm = shipCamera.GetComponent<VignetteAndChromaticAberration>();
            NoiseAndScratches nsc = shipCamera.GetComponent<NoiseAndScratches>();
            vcm.intensity = 10 * speedPercent;
            vcm.blur = 2 * speedPercent;
            nsc.grainIntensityMax = speedPercent * 0.2f;
            nsc.scratchIntensityMax = speedPercent * 0.2f;
            // Camera effects


            // Generate a plane that intersects the transform's position with an 
            Plane playerPlane = new Plane(transform.forward, transform.position + transform.forward * 50);
            //Debug.Log (transform.forward*200);

            // Generate a ray from the cursor position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Determine the point where the cursor ray intersects the plane.
            // This will be the point that the object must look towards to be looking at the mouse.
            // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
            //   then find the point along that ray that meets that distance.  This will be the point
            //   to look at.

            float hitdist = 0.0f;
            //Debug.Log (playerPlane.Raycast (ray, out hitdist));
            // If the ray is parallel to the plane, Raycast will return false.
            if (playerPlane.Raycast(ray, out hitdist))
            {


                // Get the point along the ray that hits the calculated distance.
                Vector3 targetPoint = ray.GetPoint(hitdist);


                //Vector3 normal = Vector3.Slerp(transform.up, Vector3.up, speed * Time.deltaTime);


                // Determine the target rotation.  This is the rotation if the transform looks at the target point
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, transform.up);
                targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, mouseSensibility * Time.deltaTime * (speedPercent + 0.05f));
                //Debug.Log (Vector3.Angle(targetPoint - transform.position,transform.forward));

                Quaternion rotDirection = Quaternion.Inverse(transform.rotation) * targetRotation;

                // Smoothly rotate towards
                transform.rotation = targetRotation;
                shipCamera.localRotation = Quaternion.Slerp(shipCamera.localRotation, Quaternion.Euler(oldRotation.eulerAngles.x + rotDirection.eulerAngles.x, oldRotation.eulerAngles.y, oldRotation.eulerAngles.z + rotDirection.eulerAngles.y * 4), 1);


            }
        }
    }
}
