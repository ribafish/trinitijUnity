﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other) {
		Rigidbody body = other.GetComponent<Rigidbody>();
		Debug.Log ("Collided");
	}
}
