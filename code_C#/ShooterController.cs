using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {

	public float speed;
	//public Vector3 angle;
	public GameObject projectile;
	public float respawn;
	private float timer;
	private Vector3 pos;
	private float rot;
	private bool controlled;

	private Quaternion originalRotation;
	public float ogRot;
	//public Vector3 direction;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		originalRotation = transform.rotation;
		timer = respawn;
	}
	
	// Update is called once per frame
	void Update () {
		rot = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z);

		if (!controlled) {
			if (rot != ogRot) {
				transform.rotation = originalRotation;
			}
		}
		timer -= Time.deltaTime;
		if (timer <= 0) {
			GameObject shot = Instantiate (projectile, pos, Quaternion.identity);
			Rigidbody2D shotRbody = shot.gameObject.GetComponent<Rigidbody2D> ();
			shotRbody.velocity = new Vector2 (Mathf.Cos(rot) * speed, Mathf.Sin(rot) * speed);
			timer = respawn;
		}
	}

	public void TriggerControl() {
		controlled = !controlled;
	}

	public void RotateControlledEnemy (float rotBy) {
		transform.Rotate(0f, 0f, rotBy);
		rot = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z);
	}
}
