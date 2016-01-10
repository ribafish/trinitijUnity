using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.AddForce (transform.forward*20);
	}
}
