using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public GameObject destructEnemy;
	public GameObject explosion;

	private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = transform.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnParticleCollision(GameObject other) {
		//Rigidbody body = other.GetComponent<Rigidbody>();
		GameObject obj = Instantiate (destructEnemy, transform.position, transform.rotation) as GameObject;

		GameObject expl = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		expl.GetComponent<ParticleSystem> ().Play ();

		// Explosion force
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 200);
		foreach (Collider c in hitColliders) {
			c.attachedRigidbody.AddForce ((c.attachedRigidbody.position-transform.position)*Vector3.Distance(c.attachedRigidbody.position,transform.position)/200f, ForceMode.Impulse);
		}

		foreach(Rigidbody rb in obj.GetComponentsInChildren<Rigidbody>()){
			rb.velocity = rigidbody.velocity;
			rb.angularVelocity = rigidbody.angularVelocity;
			rb.AddForce (Random.insideUnitSphere*20, ForceMode.Impulse);
			//rb.AddExplosionForce (10f, transform.position, 10f);
			//StartCoroutine("componentDestroy",rb.gameObject);
			rb.gameObject.AddComponent<DelayDestroy>();
			rb.gameObject.GetComponent<DelayDestroy> ().explosion = explosion;
		}
		Destroy (gameObject);
		Destroy (expl, 3f);
		//Destroy (obj, Random.value+0.5f);

	}


}
