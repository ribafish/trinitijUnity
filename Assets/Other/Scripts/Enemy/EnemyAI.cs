using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Transform target;
	public int maxSpeed = 50;
	public float raydist = 700;

	private float speed = 1f;
	private Rigidbody rigidbody;
    private HitHealthShield igralecZivljenja;
	private ArrayList bulletSources = new ArrayList();
	private Vector3 hitvec;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		hitvec = Vector3.zero;

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

		 


		//hitvec = Vector3.zero;
		/*Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1000);
		foreach (Collider c in hitColliders) {
			if (c.attachedRigidbody.gameObject.tag != "Player") {
				//Vector3 rigforce = (transform.position - c.attachedRigidbody.position)/1000;
				Vector3 rigforce = (new Vector3 (1000, 1000, 1000) - (transform.position - c.attachedRigidbody.position))/100 * c.bounds.size.magnitude/200;
				Debug.Log (c.bounds.size.magnitude);
				hitvec += rigforce * Time.deltaTime;
			}
			hitvec /= hitColliders.Length;
		}*/

		
		if (Physics.SphereCast(transform.position, 25, transform.forward, out hit, raydist)) {
			
			if (hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Player") {
				shoot ();
			} else {
				Debug.Log (hit.point);
				//hitvec = (new Vector3 (1000, 1000, 1000) - (transform.position - hit.point));
				hitvec += ((new Vector3 (raydist, raydist, raydist) - (transform.position - hit.point))/raydist)*2;
			}
			//igralecZivljenja.Hit(1);    //ustreli gralca (trenutno vsak frame, bolje dati timeout)
		}

		//if (Physics.Raycast (transform.position, transform.forward, out hit, 1000)) {
		/*
		if (Physics.Raycast (transform.position, transform.forward, out hit, 1000)) {
			if (hit.rigidbody.gameObject.tag == "Player") {
				shoot ();
			} else {
				Debug.Log ("Hit rotation");
				//hitvec = (new Vector3 (1000, 1000, 1000) - (transform.position - hit.point));
				hitvec += (new Vector3 (1000, 1000, 1000) - (transform.position - hit.point))/50;
			}
			//igralecZivljenja.Hit(1);    //ustreli gralca (trenutno vsak frame, bolje dati timeout)
		}
		*/
		if(hitvec.sqrMagnitude > 0){
			rigidbody.velocity = Vector3.Slerp(rigidbody.velocity, transform.forward * 20, Time.deltaTime*2);
		}
		speed = Mathf.Clamp (1f / Mathf.Log (hitvec.sqrMagnitude+1), 0.5f, 1f);
		//Debug.Log (speed);

		//if (!hitRotation) {
		float distance = Vector3.Distance (target.position, transform.position);
		//speed = Mathf.Clamp01 (distance / 200f);
		Vector3 targetVec = (target.position - transform.position).normalized * 10; //Mathf.Clamp(1000 - Vector3.Distance(target.position, transform.position), 2, 200);

		Quaternion targetRotTarg = Quaternion.LookRotation (hitvec + targetVec, Vector3.up);
		hitvec = hitvec / 1.2f;
		//Debug.Log (hitvec.magnitude);
		if (distance < 50 || Vector3.Angle((target.position - transform.position), transform.forward) < 3)
				shoot ();

		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotTarg, Time.deltaTime * speed + 0.3f);

		//}

		shootTime-=Time.deltaTime;
	}

	private float shootTime = 0;
	void shoot(){
		//Debug.Log ("Shoot");
		if (shootTime < 0) {
			shootTime = 0.5f;
			foreach (Transform bs in bulletSources)
				bs.GetComponent<ParticleSystem> ().Emit (1);
		}
	}
}
