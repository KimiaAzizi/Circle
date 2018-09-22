using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCtrl : MonoBehaviour {
	Vector3 hitDefaultPos;
	// Use this for initialization
	void Start () {
		hitDefaultPos = GameObject.FindGameObjectWithTag ("HitDefaultPosition").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameCtrl.instance.isMagnet) {
			if (gameObject.CompareTag ("PinkCircle") || gameObject.CompareTag ("MagnetCircle") || gameObject.CompareTag ("PinkStarCircle") || gameObject.CompareTag ("FastSpeedCircle")) {
				transform.position = Vector3.Lerp (transform.position, GameCtrl.instance.magnetCirSign.transform.position, 0.02f);
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero);
			if (hit != null && hit.collider != null && GameCtrl.instance.canTouch) {
				if (hit.collider.gameObject.CompareTag ("PinkCircle") ) {
					hit.collider.gameObject.SetActive (false);
					GameCtrl.instance.UpdateScoreCount ();
					GameCtrl.instance.UpdateCircleCount ();
					SFXCtrl.instance.PinkCircle (hit.collider.gameObject.transform.position);
					hit.transform.position = hitDefaultPos;
				} else if (hit.collider.gameObject.CompareTag ("BlueCircle") ) {
					hit.collider.gameObject.SetActive (false);
					GameCtrl.instance.MinesScoreCount ();
					SFXCtrl.instance.BlueCircle (hit.collider.gameObject.transform.position);
					hit.transform.position = hitDefaultPos;
				} else if (hit.collider.gameObject.CompareTag ("PurpleCircle") ) {
					hit.collider.gameObject.SetActive (false);
					GameCtrl.instance.MinesScoreCount ();
					SFXCtrl.instance.PurpleCircle (hit.collider.gameObject.transform.position);
					hit.transform.position = hitDefaultPos;
				} else if (hit.collider.gameObject.CompareTag ("PinkStarCircle") ) {
					hit.collider.gameObject.SetActive (false);
					GameCtrl.instance.UpdateStarCount ();
					SFXCtrl.instance.StarCircle (hit.collider.gameObject.transform.position);
					hit.transform.position = hitDefaultPos;
				} else if (hit.collider.gameObject.CompareTag ("FastSpeedCircle") ) {
					Destroy (hit.collider.gameObject);
					GameCtrl.instance.isNormalSpeed = false;
					GameCtrl.instance.fastSpeedTime = 0.3f;
					GameCtrl.instance.UpdateFastSpeedCount ();
					hit.transform.position = hitDefaultPos;
				} else if (hit.collider.gameObject.CompareTag ("MagnetCircle") ) {
					Destroy (hit.collider.gameObject);
					GameCtrl.instance.isMagnet = true;
					GameCtrl.instance.UpdateMagnetCount ();
					//hit.transform.position = hitDefaultPos;
				} else if (hit.collider.gameObject.CompareTag ("SavingCircle") ) {
					Destroy (hit.collider.gameObject);
					GameCtrl.instance.isSavingCir = true;
					GameCtrl.instance.UpdateMagnetCount ();
					hit.transform.position = hitDefaultPos;
				} else {
					hit.transform.position = hitDefaultPos;
				}
			}
		
		}
	}

	public void OnMouseDown(){
//		if (gameObject.CompareTag ("PinkCircle")) {
//			gameObject.SetActive (false);
//			GameCtrl.instance.UpdateScoreCount ();
//			GameCtrl.instance.UpdateCircleCount ();
//			SFXCtrl.instance.PinkCircle (transform.position);
//
//		}else if (gameObject.CompareTag ("BlueCircle")) {
//			gameObject.SetActive (false);
//			GameCtrl.instance.MinesScoreCount ();
//			SFXCtrl.instance.BlueCircle (transform.position);
//
//		}else if (gameObject.CompareTag ("PurpleCircle")) {
//			gameObject.SetActive (false);
//			GameCtrl.instance.MinesScoreCount ();
//			SFXCtrl.instance.PurpleCircle (transform.position);
//
//		}else if(gameObject.CompareTag ("PinkStarCircle")) {
//			if(GameCtrl.instance.canTouch){
//			gameObject.SetActive (false);
//			GameCtrl.instance.UpdateStarCount ();
//			SFXCtrl.instance.StarCircle (transform.position);
//			}
//
//
//		}  else if(gameObject.CompareTag ("FastSpeedCircle")) {
//			if(GameCtrl.instance.canTouch){
//			Destroy (gameObject);
//			GameCtrl.instance.isNormalSpeed=false;
//			GameCtrl.instance.fastSpeedTime=0.3f;
//			GameCtrl.instance.UpdateFastSpeedCount ();
//			}
//
//		}  else if (gameObject.CompareTag ("MagnetCircle")) {
//			if (GameCtrl.instance.canTouch) {
//				Destroy (gameObject);
//				GameCtrl.instance.isMagnet = true;
//				GameCtrl.instance.UpdateMagnetCount ();
//			}
//		} else if (gameObject.CompareTag ("SavingCircle")) {
//			if (GameCtrl.instance.canTouch) {
//				Destroy (gameObject);
//				GameCtrl.instance.isSavingCir = true;
//				GameCtrl.instance.UpdateMagnetCount ();
//			}
//		} 
	}
}
