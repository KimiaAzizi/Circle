using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMoveCtrl : MonoBehaviour {
	public float speed;
	public Transform targetPos;
	public Transform originalPos;
	public bool isMoved;


	// Use this for initialization
	void Start () {
		isMoved = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (isMoved) {
			transform.position = Vector2.Lerp (transform.position, targetPos.position, speed * Time.deltaTime);

		} else if(!isMoved){

			isMoved = false;
			transform.position = Vector2.Lerp (transform.position, originalPos.position, speed * Time.deltaTime);
			Invoke ("deactive",0.6f);
		
		}

	}
	void deactive(){
		gameObject.SetActive (false);



	}

}
