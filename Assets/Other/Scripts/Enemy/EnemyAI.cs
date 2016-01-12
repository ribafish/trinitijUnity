using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Transform target;
	public int maxSpeed = 50;

	private float speed = 1f;
	private Rigidbody rigidbody;
    private HitHealthShield igralecZivljenja;
	private ArrayList bulletSources = new ArrayList();

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();

        //nalozimo skripto, ki omogoca streljanje na igralca
        igralecZivljenja = GameObject.Find("HealthShieldBars").GetComponent<HitHealthShield>();
		foreach(Transform child in transform){
			if (child.tag == "Gun") {
				child.GetComponent<ParticleSystem> ().playbackSpeed = 1.0f;
				bulletSources.Add (child);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		rigidbody.AddForce (transform.forward*maxSpeed*speed);
		//transform.rotation
		//Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
		RaycastHit hit;
		//Quaternion targetRot = Quaternion.LookRotation (transform.forward, Vector3.up);
		if (Physics.Raycast (transform.position, transform.forward, out hit, 2000)) {
			if (hit.rigidbody.gameObject.tag == "Player") {
				shoot ();
			} else {
				Debug.Log ("Hit rotation");
				Quaternion targetRot = Quaternion.LookRotation (transform.right, Vector3.up);
				transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * speed * hit.distance/2000);
			}
			//igralecZivljenja.Hit(1);    //ustreli gralca (trenutno vsak frame, bolje dati timeout)
		}

		float distance = Vector3.Distance (target.position, transform.position);
		speed = Mathf.Clamp01 (distance / 200f);

		Quaternion targetRotTarg = Quaternion.LookRotation (target.position - transform.position, Vector3.up);


		if (distance < 50 || Quaternion.Angle(targetRotTarg, transform.rotation) < 3)
			shoot ();

		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotTarg, Time.deltaTime * speed);

		

		shootTime-=Time.deltaTime;
	}

	private float shootTime = 0;
	void shoot(){
		Debug.Log ("Shoot");
		if (shootTime < 0) {
			shootTime = 0.5f;
			foreach (Transform bs in bulletSources)
				bs.GetComponent<ParticleSystem> ().Emit (1);
		}
	}
}
