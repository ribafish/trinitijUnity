using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
	public float speed;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Generate a plane that intersects the transform's position with an 
		Plane playerPlane = new Plane(transform.forward, transform.position+transform.forward*50);
		//Debug.Log (transform.forward*200);
	
		
		
		
		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		//   then find the point along that ray that meets that distance.  This will be the point
		//   to look at.

		float hitdist = 0.0f;
		Debug.Log (playerPlane.Raycast (ray, out hitdist));
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist)) 
		{


			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint(hitdist);


			Vector3 normal = Vector3.Slerp(transform.up, Vector3.up, speed * Time.deltaTime);

			
			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, normal);

			// Smoothly rotate towards 
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		}
	}
}
