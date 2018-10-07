using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

	public Button HomeButton;
	public Button ReplayButton;
	private bool isPaused;

	// Use this for initialization
	void Start () {
		HomeButton.gameObject.SetActive(false);
		ReplayButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (!isPaused) {
				Time.timeScale = 0;
				HomeButton.gameObject.SetActive (true);
				ReplayButton.gameObject.SetActive (true);
			} else {
				Time.timeScale = 1;
				HomeButton.gameObject.SetActive (false);
				ReplayButton.gameObject.SetActive (false);
			}
			isPaused = !isPaused;
		}
	}

	public void GoHome() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main_Menu");
	}

	public void ReplayLevel() {
		string currentScene = SceneManager.GetActiveScene ().name;
		Time.timeScale = 1;
		SceneManager.LoadScene (currentScene);
	}
}
