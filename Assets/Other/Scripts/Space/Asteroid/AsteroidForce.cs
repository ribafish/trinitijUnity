using UnityEngine;
using System.Collections;

public class AsteroidForce : MonoBehaviour {

	// Use this for initialization
	private ParticleSystem psys;
	private ParticleSystem.Particle[] particles;
	private GameObject[] planets;
	void Start () {
		psys = GetComponent<ParticleSystem> ();
		particles = new ParticleSystem.Particle[50000];
		planets = GameObject.FindGameObjectsWithTag("Planet");
	}
	
	// Update is called once per frame
	void Update () {
		int len=psys.GetParticles(particles);
		
		for (int i=0; i < len; i++) {
			ParticleSystem.Particle p = particles[i];

			Vector3 direction = (planets[0].transform.position - p.position);
			//direction.Normalize();
			particles[i].velocity = p.velocity + direction*0.00009f;
			//p.velocity = p.velocity;
			
			
		}
		
		psys.SetParticles(particles,len);
	}
}
