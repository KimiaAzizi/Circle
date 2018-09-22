using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstHelpPanelCtrl : MonoBehaviour {
	public GameObject bgEffect;
	public void ClosePanel(){
		FindObjectOfType<GameCtrl> ().enabled = true;
		bgEffect.SetActive (false);
		GameCtrl.instance.ui.firstHelpPanel.SetActive (false);

		if (AudioCtrl.soundOn) {
			AudioCtrl.instance.bgMusic.SetActive (true);
		}
		GameCtrl.instance.isFirstHelp = false;

	}


}
