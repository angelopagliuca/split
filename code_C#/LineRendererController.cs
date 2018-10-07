using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour {

	public GameObject lightPlayer;
	public GameObject darkPlayer;
	private LineRenderer lr;

	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {
		Vector3[] positions = new Vector3[3];
		positions[0] = lightPlayer.transform.position;
		positions[1] = (lightPlayer.transform.position + darkPlayer.transform.position) / 2;
		positions[2] = darkPlayer.transform.position;
		lr.SetPositions(positions);
	}
}
