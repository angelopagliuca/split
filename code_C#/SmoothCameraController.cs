using UnityEngine;
using System.Collections;

public class SmoothCameraController : MonoBehaviour {

	public GameObject player;

	private float dampTime = 0.5f;
	private Vector3 velocity = Vector3.zero;

	void FixedUpdate () 
	{
		if (player.transform)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(player.transform.position);
			Vector3 delta = player.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.25f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			if (transform.position.x <= 15f) {
				float wall_max_left = 15f;
				transform.position = new Vector3 (wall_max_left, transform.position.y, transform.position.z);
			}
			if (transform.position.x >= 44f) {
				float wall_max_right = 44f;
				transform.position = new Vector3 (wall_max_right, transform.position.y, transform.position.z);
			}
		}
	}
}