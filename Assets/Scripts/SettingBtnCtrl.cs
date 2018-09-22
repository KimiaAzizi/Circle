using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingBtnCtrl : MonoBehaviour {
	private GameObject anotherBtn;
	public bool canShow,canHide;
	public static bool isShow;
	public Sprite purpleCir,yellowCir,purpleSettingTxt,yellowSettingTxt;
	// Use this for initialization
	void Start () {
		canShow = true;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ShowItems(){
		if(canShow){
			ChangeColor ();
			anotherBtn = GameObject.Find ("AnotherBtns") as GameObject;
			foreach (Transform child in anotherBtn.transform) {
				child.gameObject.SetActive (true);
				child.gameObject.GetComponent<BtnMoveCtrl> ().isMoved = true;

			}
			canShow = false;

		}else if(!canShow){
			anotherBtn = GameObject.Find ("AnotherBtns") as GameObject;
			foreach (Transform child in anotherBtn.transform) {
				child.gameObject.GetComponent<BtnMoveCtrl> ().isMoved = false;
			}
				
			Invoke ("ChangeColor",0.6f);
		}
	}

	public void ChangeColor(){
		if (canShow) {
			gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = purpleCir;
			gameObject.GetComponent<Image> ().sprite = purpleSettingTxt;
		}else if(!canShow){
			gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = yellowCir;
			gameObject.GetComponent<Image> ().sprite= yellowSettingTxt;
			canShow = true;

		}
	}
}
