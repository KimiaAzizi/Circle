using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCtrl : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("PinkCircle")) {
			if(!GameCtrl.instance.isSavingCir){
				GameCtrl.instance.ActiveGameOverPanel ();

			}

		} else if (other.gameObject.CompareTag ("BlueCircle")) {
			GameCtrl.instance.circlesList.Remove(other.gameObject);
			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("PurpleCircle")) {
			GameCtrl.instance.circlesList.Remove(other.gameObject);
			Destroy (other.gameObject);
		
		} 
	}
}
