using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverPanelCtrl : MonoBehaviour {

	public void PlayAgain(){

		GameCtrl.instance.isFirstHelp=true;
		SceneManager.LoadScene ("Level1");


	}


}
