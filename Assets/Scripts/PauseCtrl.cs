using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseCtrl : MonoBehaviour {
	public static bool gameIsPaused =false;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if (gameIsPaused) {
				Resume ();

			} else {
				if(!GameCtrl.instance.isGameOver && !GameCtrl.instance.isFirstHelp){
				Pause ();

				}
			}
			
		}

	}
	public void Resume(){
		GameCtrl.instance.canTouch = true;
		if (AudioCtrl.soundOn) {
			Camera.main.GetComponent<AudioListener> ().enabled = true;
		}
		GameCtrl.instance.ui.pausePanel.SetActive (false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}
	public void Pause(){
		GameCtrl.instance.canTouch = false;
		Camera.main.GetComponent<AudioListener> ().enabled = false;
		GameCtrl.instance.ui.pausePanel.SetActive (true);
		Time.timeScale = 0f;
		gameIsPaused = true;

	}

	public void LoadMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Menu");
	}

	void OnApplicationPause(){
		if(!GameCtrl.instance.isGameOver && !GameCtrl.instance.isFirstHelp){
			Pause ();

		}

	}
}
