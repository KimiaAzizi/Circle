using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour {
	public SFX sfx;
	public static  SFXCtrl instance;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
			
	}
		
	public void StarCircle(Vector3 pos){

		Instantiate (sfx.starPickup, pos, Quaternion.identity);
	}
	public void PinkCircle(Vector3 pos){

		Instantiate (sfx.pinkCirclePickup, pos, Quaternion.identity);
	}
	public void PurpleCircle(Vector3 pos){

		Instantiate (sfx.purpleCirclePickup, pos, Quaternion.identity);
	}
	public void BlueCircle(Vector3 pos){

		Instantiate (sfx.blueCirclePickup, pos, Quaternion.identity);
	}
}
