using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	private GameObject projectile;
	private string playerName;
	private string otherName;

	// Use this for initialization
	void Start () {
		projectile = gameObject;
		if (this.tag == "DarkProjectile") {
			playerName = "PlayerLight";
			otherName = "PlayerDark";
		} else if (this.tag == "LightProjectile") {
			playerName = "PlayerDark";
			otherName = "PlayerLight";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag != "DarkProjectile" && col.gameObject.tag != "LightProjectile"
			&& col.gameObject.tag != "DarkEnemy" && col.gameObject.tag != "LightEnemy" && col.gameObject.name != otherName) {
			Destroy (projectile);	
		} 
		if (col.gameObject.name == playerName) {
			col.gameObject.GetComponent<PlayerController> ().GetHit ();
		}
	}
}
