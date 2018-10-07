using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager GM = null;

	public bool musicTurnOn = true;

	public int maxLevel;

	void Awake() {
		if (GM == null) {
			GM = this;
			GM.maxLevel = 1;
		} else if (GM != this) {
			Destroy(gameObject);
		}
			
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (musicTurnOn) {
			string currentScene = SceneManager.GetActiveScene ().name;
			if (currentScene == "Main_Menu") {
				SoundManager.S.PlayGameMusic ();
			} else if (currentScene == "Level1" || currentScene == "Level2" || currentScene == "Level3" || currentScene == "Level4") {
				//SoundManager.S.PlayBackgroundMusic ();
			}
			musicTurnOn = false;
		}


	}


}
