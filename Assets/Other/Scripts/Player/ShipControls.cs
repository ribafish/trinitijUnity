using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.Audio;

public class ShipControls : MonoBehaviour,Damage{
	public float mouseSensibility = 3;
	public Transform shipCamera;
	public float thrustMax = 100;
    [Range(0.2f, 1.0f)] public float engineVolumeMax = 0.5f;
    public AudioSource engineSound;
	[Range(10, 45)] public int maxLeanAngle = 40;
	public GameObject sparkles;
	public GameObject healthGUI;
	public GameObject explosion;
	//public GameObject cursor;
	public RectTransform cursor;

	private float thrust = 0;
	private Quaternion oldRotation;
	private Vector3 cameraPosDiff;
	private Vector3 cameraDistance = new Vector3 (0, 0, 0);
	private Rigidbody rigidBody;
	private ArrayList busters = new ArrayList ();
	private float horizontalmove=0f;
	private float oldhorizontalmove=0f;
	private float mouseAngle = 0f;
	private float oldMouseAngle = 0f;
	private float speedPercent = 0f;


    private HitHealthShield igralecZivljenja;

	// Use this for initialization
	void Start () {
		cameraPosDiff = shipCamera.position - transform.position;
		rigidBody = transform.GetComponent<Rigidbody> ();
		foreach (Transform c in transform) {
			if (c.tag == "Buster") {
				busters.Add (c.GetComponent<ParticleSystem>());
			}
		}
        engineSound.volume = 0.0f;

        //nalozimo skripto, ki omogoca streljanje na igralca
        igralecZivljenja = GameObject.Find("HealthShieldBars").GetComponent<HitHealthShield>();

		Cursor.visible = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Globals.instance.pauseGame.IsPaused())
        {
			
			Vector3 speed = transform.InverseTransformDirection(rigidBody.velocity);
			speedPercent = (speed.z / thrustMax);


			thrust = Mathf.Clamp01(thrust + Input.GetAxis ("Thrust")/100f);



			oldhorizontalmove = horizontalmove;
			horizontalmove = Mathf.LerpAngle (horizontalmove, Input.GetAxis ("Lean"), Time.deltaTime * 5);
			// Move ship horizontally
			transform.position += transform.right * horizontalmove;

            engineSound.volume = thrust * engineVolumeMax;
            engineSound.pitch = thrust + 0.4f;


			// Thrust force
			rigidBody.AddRelativeForce(Vector3.forward * thrustMax * thrust);
            

            /* CAMERA EFECTS */
           


			// Ship rotation on horizontal move
			shipCamera.rotation = Quaternion.LookRotation (transform.forward, Quaternion.AngleAxis (oldhorizontalmove*maxLeanAngle + oldMouseAngle, transform.forward) * transform.up);
			transform.rotation = Quaternion.LookRotation (transform.forward, Quaternion.AngleAxis (-horizontalmove*maxLeanAngle - mouseAngle, transform.forward) * shipCamera.transform.up);




			//shipCamera.position = transform.position + cameraPosDiff;
			//shipCamera.LookAt (transform.position);

			// Speed distance
			cameraDistance = Vector3.Lerp(cameraDistance, new Vector3(0, 0, -20 * speedPercent), 2);

			//transform.position += new Vector3(Mathf.PerlinNoise(transform.position.z / thrustMax * 10, 0) * speedPercent, Mathf.PerlinNoise(transform.position.z / thrustMax * 10, 20) * speedPercent, 0);

            VignetteAndChromaticAberration vcm = shipCamera.GetComponent<VignetteAndChromaticAberration>();
            vcm.intensity = 10 * speedPercent;
            vcm.blur = 2 * speedPercent;
            //////////////////////////////////////////////////
			/// 

			////////////////
			/// THRUSTERS///
			/// ////////////

			foreach (ParticleSystem ps in busters)
			{
				ps.Emit((int)(10 * thrust));
				ps.startSpeed = 2 * thrust;
			}


            // Generate a plane that intersects the transform's position with an 
			Plane playerPlane = new Plane(transform.forward, transform.position + transform.forward * 50);
            //Debug.Log (transform.forward*200);

            // Generate a ray from the cursor position
			//Debug.Log(Input.GetAxis ("Horizontal"));
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(Input.GetAxis ("Horizontal")*Screen.width,Input.GetAxis ("Vertical")*Screen.height));
            

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

				Vector3 pos = Camera.main.WorldToScreenPoint((shipCamera.position + transform.forward * 200) + rigidBody.velocity/2);
				//cursor.transform.localPosition = new Vector3 (pos.x, pos.y, 5);
				//shipCamera.GetComponentInChildren<Image>() = new Rect (pos, new Vector3 (20, 20));
				//GUI.DrawTexture(new Rect(pos,new Vector2(20,20)), cursor);
				cursor.anchoredPosition = pos;


                // Determine the target rotation.  This is the rotation if the transform looks at the target point
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, transform.up);
                targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, mouseSensibility * Time.deltaTime * (speedPercent + 0.05f));
                //Debug.Log (Vector3.Angle(targetPoint - transform.position,transform.forward));

                Quaternion rotDirection = Quaternion.Inverse(transform.rotation) * targetRotation;
				oldMouseAngle = mouseAngle;
				mouseAngle = Mathf.LerpAngle (mouseAngle, rotDirection.eulerAngles.y * 10, Time.deltaTime*5);
				
                // Smoothly rotate towards
                transform.rotation = targetRotation;
				//shipCamera.rotation = Quaternion.Slerp(shipCamera.rotation, Quaternion.AngleAxis(rotDirection.eulerAngles.z, shipCamera.right), 1);


            }

        }
    }

	void LateUpdate(){
		Vector3 cameraNoise = new Vector3 (Mathf.PerlinNoise(Time.time*10,0),Mathf.PerlinNoise(Time.time*10,0)) * speedPercent;
		shipCamera.position = transform.TransformPoint (cameraPosDiff + cameraDistance + cameraNoise);
		//shipCamera.position = Vector3.Slerp(shipCamera.position, transform.position, 0.5f);

	}
    
	void OnCollisionEnter (Collision col)
	{
		GameObject spark = Instantiate (sparkles, col.contacts [0].point, Quaternion.identity) as GameObject;
		spark.GetComponent<ParticleSystem> ().Play ();
		Destroy (spark, 2f);
		if(healthGUI!=null)
            applayDamage(10);
			//healthGUI.GetComponentInChildren<HitHealthShield> ().Hit ((int)col.impulse.magnitude*5);
		//applayDamage ((int)col.impulse.sqrMagnitude);
		/*if(col.gameObject.name == "prop_powerCube")
		{
			Destroy(col.gameObject);
		}*/

        //Debug.Log("Player hit: " + other.gameObject.name);
        if (col.gameObject.name.Contains("Meteorid_"))
        {
            igralecZivljenja.Hit(50);
        }
        if (col.gameObject.name.Contains("Asteroid"))
        {
            igralecZivljenja.Hit(50);
        }
	}

	void OnParticleCollision(GameObject other) {
		applayDamage (10);
	}

	public void kill(){
		GameObject expl = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		expl.GetComponent<ParticleSystem> ().Play ();
		Destroy (expl, 5f);
		Destroy (gameObject);
	}

	public void applayDamage(int amount){
		if(healthGUI!=null)
			healthGUI.GetComponentInChildren<HitHealthShield> ().Hit (amount);
	}
}
