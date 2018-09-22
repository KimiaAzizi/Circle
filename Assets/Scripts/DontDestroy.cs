using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	void Awake(){
		
		GameObject[] objs=GameObject.FindGameObjectsWithTag("bgMusic");
		if(objs.Length>1)
			Destroy (this.gameObject);
		DontDestroyOnLoad (this.gameObject);
	}
	void Update(){
		if(!AudioCtrl.soundOn){
			gameObject.GetComponent<AudioSource> ().enabled = false;
			//Camera.main.GetComponent<AudioListener> ().enabled=false;
		}else if(AudioCtrl.soundOn){
			gameObject.GetComponent<AudioSource> ().enabled = true;
			//Camera.main.GetComponent<AudioListener> ().enabled=true;
		}
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex == 1) {
			gameObject.GetComponent<AudioSource> ().mute = true;
		} else {
			gameObject.GetComponent<AudioSource> ().mute = false;
		}
	}
}
