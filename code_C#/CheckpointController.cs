using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public SpriteRenderer sr;
	public GameObject otherCheckpoint; 
	public int numTouch = 0;

	public Vector3 PlayerTouch() {
		if (numTouch == 0) {
			numTouch = 1;
			sr.sprite = sprite2;
			otherCheckpoint.GetComponent<CheckpointController>().numTouch = 1;
			otherCheckpoint.GetComponent<CheckpointController>().sr.sprite = otherCheckpoint.GetComponent<CheckpointController>().sprite2;
			return new Vector3(-3000.0f, 0.0f, 0.0f);
		} else if (numTouch == 1) {
			numTouch = 2;
			sr.sprite = sprite3;
			otherCheckpoint.GetComponent<CheckpointController>().numTouch = 2;
			otherCheckpoint.GetComponent<CheckpointController>().sr.sprite = otherCheckpoint.GetComponent<CheckpointController>().sprite3;
			return new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.0f);
		}
		return new Vector3(-3000.0f, 0.0f, 0.0f);
	}

}
