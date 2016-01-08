using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroControl : MonoBehaviour {

	public Transform header = null;
	public Transform text = null;
	public float speed = 30;
	public float distance = 100;

	// Use this for initialization
	void Start () {
		if (header != null && text != null) {
			Text size = text.GetComponent<Text>();
			text.position = header.position;
			text.position -= text.up * (size.preferredHeight/2 + distance);
			Debug.Log(size.preferredHeight/2);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (header != null && text != null) {
			header.position += header.up * speed * Time.deltaTime;
			text.position += header.up * speed * Time.deltaTime;
		}
	}
}
