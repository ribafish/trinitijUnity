using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public int forceRadius = 100;

	// Use this for initialization
	void Start () {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, forceRadius);
		foreach (Collider c in hitColliders) {
			float force = (1.0f - Vector3.Distance (c.attachedRigidbody.position, transform.position) / forceRadius);
			c.attachedRigidbody.AddForce ((c.attachedRigidbody.position-transform.position)*force, ForceMode.Impulse);
			Damage d = c.GetComponent<Damage> ();
			// TODO: Add wait of distance
			if (d != null)
				d.applayDamage ((int)(force*30));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
