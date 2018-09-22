using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftGarbageCtrl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (GameCtrl.instance.circlesList.Contains(other.gameObject)) {
			GameCtrl.instance.circlesList.Remove (other.gameObject);
			Destroy (other.gameObject);
		} else {
			Destroy (other.gameObject);
		}

	}
}
