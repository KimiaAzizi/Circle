using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioCtrl : MonoBehaviour {

	public static AudioCtrl instance;
	public GameAudio gameAudio;
	[Tooltip("Use to on/off the audio of game from inspector")]
	public static bool soundOn=true;
	public GameObject bgMusic;
	public bool bgMusicOn;

	public void Awake(){
		if(instance==null){
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {

	}
	void Updata(){
		if (soundOn) {
			Camera.main.GetComponent<AudioListener> ().enabled= true;
		
		} else if(!soundOn) {
			Camera.main.GetComponent<AudioListener> ().enabled = false;
//			if (GameCtrl.instance.isSavingCir) {
//				GameCtrl.instance.magicSavingCircle.GetComponent<AudioSource> ().mute = true;
//			} else {
//				GameCtrl.instance.magicSavingCircle.GetComponent<AudioSource> ().mute = false;
//
//			}
		}
	}

	public void GameOver(Vector3 pos){
		if(soundOn){
			AudioSource.PlayClipAtPoint (gameAudio.gameOver,pos);
		}
	}


	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (soundOn) {
			Camera.main.GetComponent<AudioListener> ().enabled = true;
		} else if(!soundOn) {
			Camera.main.GetComponent<AudioListener> ().enabled = false;
		}
	}

	void OnEnable(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
		
}
