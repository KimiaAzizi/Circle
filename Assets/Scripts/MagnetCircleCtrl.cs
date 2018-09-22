using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCircleCtrl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("PinkCircle")){
			GameCtrl.instance.UpdateScoreCount ();
			GameCtrl.instance.UpdateCircleCount ();
			SFXCtrl.instance.PinkCircle (other.transform.position);
			GameCtrl.instance.circlesList.Remove(other.gameObject);
			Destroy (other.gameObject);
		}else if(other.gameObject.CompareTag("PinkStarCircle")){
			GameCtrl.instance.UpdateScoreCount ();
			GameCtrl.instance.UpdateStarCount ();
			SFXCtrl.instance.StarCircle (other.transform.position);
			Destroy (other.gameObject);
		}else if(other.gameObject.CompareTag("MagnetCircle")){
			GameCtrl.instance.UpdateScoreCount ();
			GameCtrl.instance.UpdateMagnetCount ();
			Destroy (other.gameObject);
		}else if(other.gameObject.CompareTag("FastSpeedCircle")){
			GameCtrl.instance.UpdateScoreCount ();
			GameCtrl.instance.UpdateFastSpeedCount ();
			SFXCtrl.instance.StarCircle (other.transform.position);
			Destroy (other.gameObject);
		}
	}
}
