using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public GameObject bullet;

	private float shootTime = 0;
	private ArrayList bulletSources = new ArrayList();
	// Use this for initialization
	void Start () {
		shootTime = 0;
		foreach(Transform child in transform){
			if (child.tag == "Gun") {
				child.GetComponent<ParticleSystem> ().playbackSpeed = 1.0f;
				bulletSources.Add (child);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 target = transform.GetComponentInChildren<Camera> ().ScreenToWorldPoint (Input.mousePosition + Vector3.forward * 300);
		//Debug.DrawLine (transform.position, target, Color.white);

		if (Input.GetButton ("Fire1") &&  shootTime < 0) {
			shootTime = 0.2f;
			foreach (Transform bs in bulletSources) {
				bs.GetComponent<ParticleSystem> ().Emit (2);
				/*


				GameObject bul = Instantiate (bullet, bs.position, bs.rotation) as GameObject;

				bul.GetComponent<Rigidbody> ().velocity = transform.GetComponent<Rigidbody> ().velocity;
				bul.GetComponent<Rigidbody> ().angularVelocity = transform.GetComponent<Rigidbody> ().angularVelocity;

				bul.GetComponent<Rigidbody> ().AddForce (transform.forward * 100, ForceMode.Impulse);


				Destroy (bul, 2);*/
			}
		}
		shootTime-=Time.deltaTime;
	}
}
