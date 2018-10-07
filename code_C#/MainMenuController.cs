using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public void Play() {
		SceneManager.LoadScene(1);
	}

	public void Controls() {
		SceneManager.LoadScene(2);
	}

	public void Quit() {
		Application.Quit();
	}
}
