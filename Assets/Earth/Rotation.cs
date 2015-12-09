using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	 public Material[] materials;
	public Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

		// Cloud animation
		float offset = Time.time * 0.0005f;
		rend.materials[2].SetTextureOffset("_MainTex", new Vector2(offset, 0));

		// Earth rotation
		transform.Rotate(Vector3.down * Time.deltaTime);
	}
}
