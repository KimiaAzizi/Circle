using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtnCtrl : MonoBehaviour {
	public Sprite soundOnSprite;
	public Sprite soundOffSprite;


	// Use this for initialization
	void Start () {
		if(AudioCtrl.soundOn){
			gameObject.GetComponent<Image> ().sprite = soundOnSprite;
		}else if(!AudioCtrl.soundOn){
			gameObject.GetComponent<Image> ().sprite = soundOffSprite;

		}
	}

	public void OnandOffGameAudio(){
		AudioCtrl.soundOn = !AudioCtrl.soundOn;

		if(AudioCtrl.soundOn){
			gameObject.GetComponent<Image> ().sprite = soundOnSprite;
		}else if(!AudioCtrl.soundOn){
			gameObject.GetComponent<Image> ().sprite = soundOffSprite;
		}
	}


}
