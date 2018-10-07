using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

	public SpriteRenderer SpriteR;
	public string GateColor;
	public Sprite open_white;
	public Sprite open_black;
	public bool bothNext;
	public bool whiteNext;
	public bool blackNext;
	public bool gateOpen;

	public void OpenGates () {
		gameObject.tag = "OpenGate";
		//print ("OpenGates");
	}

	public void ChangeGates(int player) {
		if (player == 1) {
			SpriteR.sprite = open_white;
			//print ("ChangedGates" + GateColor);
		} else {
			SpriteR.sprite = open_black;
			//print ("ChangedGates" + GateColor);
		}
	}

}
