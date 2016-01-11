using UnityEngine;
using System.Collections;

public class DelayDestroy : MonoBehaviour {
	public GameObject explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (delayDestroy (0.5f, 3f));
	}

	IEnumerator delayDestroy(float min,float max){
		yield return new WaitForSeconds(Random.value*(max-min)+min);
		GameObject expl = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		expl.GetComponent<ParticleSystem> ().Play ();
		Destroy (gameObject);
		Destroy (expl, 3f);
	}
}
