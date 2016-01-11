using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Transform target;
	public int maxSpeed = 50;

	private float speed = 1f;
	private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		rigidbody.AddForce (transform.forward*maxSpeed*speed);
		//transform.rotation
		//Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);


		Quaternion targetRot = Quaternion.LookRotation (transform.forward, Vector3.up);
		if (Physics.Raycast (transform.position, transform.forward, 2000) ||
			Physics.Raycast (transform.position + transform.right * 2, transform.forward, 2000)) {
			targetRot = Quaternion.LookRotation (transform.up, Vector3.up);

	
			Debug.Log ("Hit");
		}
		else if(Vector3.Distance(transform.position,target.position) < 400)
			targetRot = Quaternion.LookRotation (target.position - transform.position, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, Time.deltaTime * speed);

		
	}
}
