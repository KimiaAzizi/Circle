using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;//RSFB
using System.IO;

public class GameCtrl : MonoBehaviour {

	public static GameCtrl instance;
	public GameObject scoreTxt,timerTxt;
	public GameData data;
	public UI ui;
	public Transform[] pos;
	public GameObject[] circles;
	int randomPos,randomCir,posLenght,circlesLenght;
	public float cirInsTime,delay;
	public bool canTouch,isMagnet,canCreateMagnet,canCreateFastSpeedCircle,canCreateStarCircle,canCreateSavingCir;
	bool TimerOn;
	public float lifeTime,magnetInstantiateTime,speedInstantiateTime,starInstantiateTime,saveCirInstantiateTime;
	public float fastSpeedTime;
	public List<GameObject> circlesList;
	public bool isNormalSpeed,isGameOver,isFirstHelp,isSavingCir;
	public GameObject magnetCirSign,magnetCircle,fastSpeedCircle,starCircle,magicSavingCircle,magicSaveSign;
	string dataFilePath;
	BinaryFormatter bf;
	public int lifeTimeScore;
	public string timerString;
	public List<int> posList;



	void Awake(){
		if (instance == null) {
			instance = this;
		}
		circlesList = new List<GameObject> ();
		posList= new List<int> ();
		bf = new BinaryFormatter ();
		dataFilePath = Application.persistentDataPath + "/game.bat";
	}

	void Start () {
		isNormalSpeed = true;
		TimerOn = true;
		posLenght = pos.Length;
		circlesLenght = circles.Length;
		canTouch = true;
		ui.scoreText.text=": 0";
		ui.timerText.text=": 0";
		fastSpeedTime = 0.3f;
		canCreateMagnet = true;
		isGameOver = false;
		magnetInstantiateTime = Random.Range (13,15);
		speedInstantiateTime = Random.Range (8,13);
		starInstantiateTime = Random.Range (5,10);
		saveCirInstantiateTime = Random.Range (10,20);
	}
		
	void Update () {

		if(Input.GetKeyDown(KeyCode.Return)){
			ResetData ();
		}

		if (lifeTime < 1200 && TimerOn && isNormalSpeed) {
			UpdateTimerNormal ();
		}else if(lifeTime < 1200 && TimerOn && !isNormalSpeed){
			UpdateTimerFast();
		}

		cirInsTime -= Time.deltaTime;
		if (cirInsTime < 0 && isNormalSpeed) {
			ColorInstantiate ();
			cirInsTime = 0.3f;
		}else if(cirInsTime < 0 && !isNormalSpeed){
			fastSpeedTime -= Time.deltaTime;
			ColorInstantiate ();
			cirInsTime = 0.2f;
		}

		magnetInstantiateTime -= Time.deltaTime;
		if(magnetInstantiateTime<0){
			canCreateMagnet = true;
			InstantiateMagnet ();
			magnetInstantiateTime = Random.Range (14,16);
		}
		saveCirInstantiateTime -= Time.deltaTime;
		if(saveCirInstantiateTime<0){
			canCreateSavingCir = true;
			InstantiateSavingRing ();
			saveCirInstantiateTime = Random.Range (7,14);
		}
		speedInstantiateTime -= Time.deltaTime;
		if(speedInstantiateTime<0){
			canCreateFastSpeedCircle = true;
			InstantiateFastSpeedCircle ();
			speedInstantiateTime = Random.Range (5,10);
		}
		starInstantiateTime -= Time.deltaTime;
		if(starInstantiateTime<0){
			canCreateStarCircle = true;
			InstantiateStarCircle ();
			starInstantiateTime = Random.Range (2,7);
		}

		if (!canTouch && isGameOver){
			for(int i=0;i<circlesList.Count;i++){
				//circlesList [i].GetComponent<Collider2D> ().enabled=false;
				circlesList [i].gameObject.tag="Untagged";
			}
		}
		if(isMagnet){
			magnetCirSign.gameObject.SetActive (true);
			Invoke ("DisappearMagnet",3f);

		}
		if(isSavingCir){
			magicSaveSign.gameObject.SetActive (true);
			if(AudioCtrl.soundOn){
				magicSaveSign.gameObject.GetComponent<AudioSource> ().enabled = true;
			}
			Invoke ("DisappearSavingCircleSign",5f);

		}

		if(data.timer>data.bestTime){
			data.bestTime = data.timer;
			int second = (int)(data.bestTime % 60);
			int minutes = (int)(data.bestTime / 60)%60;
			int hours = (int)(data.bestTime / 3600)%24;
		    string timerString = string.Format("{0:0}:{1:00}:{2:00}",hours,minutes,second);
			ui.bestTimerText.text = ": " + timerString;
		}else if(data.timer<data.bestTime){
			int second = (int)(data.bestTime % 60);
			int minutes = (int)(data.bestTime / 60)%60;
			int hours = (int)(data.bestTime / 3600)%24;
			string timerString = string.Format("{0:0}:{1:00}:{2:00}",hours,minutes,second);
			ui.bestTimerText.text = ": " + timerString;
		}	
		if(lifeTimeScore>data.bestScore){
			data.bestScore = lifeTimeScore;
			ui.bestScoreText.text = ": " + data.bestScore;
		}	
			
	}

	void SaveData(){
		FileStream fs = new FileStream (dataFilePath , FileMode.Create);
		bf.Serialize (fs,data);
		fs.Close ();
	}

	void LoadData(){
		if (File.Exists (dataFilePath)) {
			FileStream fs = new FileStream (dataFilePath,FileMode.Open);
			data = (GameData)bf.Deserialize (fs);
			ui.scoreText.text = ": " + data.score;
			ui.circleText.text = ": " + data.circleCount;
			ui.bestScoreText.text = ": " + data.bestScore;
			ui.bestTimerText.text = ": " + data.bestTime;
			ui.timerText.text = ":" + data.timer;
			ui.starText.text = "" + data.starCount;
			ui.magnetText.text = "" + data.magnetCount;
			ui.fastSpeedText.text = "" + data.speedFastCount;
			fs.Close ();
		}
	}

	public void ResetData(){
		FileStream fs = new FileStream (dataFilePath , FileMode.Create);
		lifeTime = 0;
		data.score = 0;
		data.circleCount= 0;
		data.timer = 0;
		data.magnetCount = 0;
		data.starCount = 0;
		data.speedFastCount = 0;
		data.bestTime = 0;
		data.bestScore = 0;
		ui.scoreText.text = ": " +data.score ;
		ui.circleText.text = ": " + data.circleCount;
		ui.timerText.text = ": " +data.timer ;
		ui.bestScoreText.text = ": " +data.bestScore ;
		ui.bestTimerText.text = ": " +data.bestTime;
		ui.magnetText.text = " " +data.magnetCount;
		ui.starText.text = " " +data.starCount;
		ui.fastSpeedText.text = " " +data.speedFastCount;
		ui.finalScoreText.text = " " +data.score;
		ui.finalTimerText.text = ": " + data.timer;
		bf.Serialize (fs,data);
		fs.Close ();

	}

	void OnEnable(){
		LoadData ();
	}
	void OnDisable(){
		SaveData ();
	}

	void ColorInstantiate(){
		if (canTouch && isNormalSpeed) {
			randomPos = Random.Range (0, posLenght);

//			while(!posList.Contains (randomPos)){
//				if (posList.Contains (randomPos)) {
//					randomPos = Random.Range (0, posLenght);
//				} else {
//					posList.Add (randomPos);
//				}
//			}
		
			randomCir = Random.Range (0, circlesLenght);


			GameObject randomCircle =Instantiate (circles [randomCir], pos [randomPos].position, Quaternion.identity);
			circlesList.Add (randomCircle);
			if(lifeTime>=20 && lifeTime<=100){
			randomCircle.GetComponent<Rigidbody2D> ().gravityScale = 0.15f;
			}if(lifeTime>=100 && lifeTime<=500){
				randomCircle.GetComponent<Rigidbody2D> ().gravityScale = 0.2f;
			}
			else if(lifeTime>=500 && lifeTime<=1000){
				randomCircle.GetComponent<Rigidbody2D> ().gravityScale = 0.25f;
			}
		
		}else if(canTouch && !isNormalSpeed){
			if (fastSpeedTime > 0) {
				randomPos = Random.Range (0, posLenght);

//				while(!posList.Contains (randomPos)){
//					if (posList.Contains (randomPos)) {
//						randomPos = Random.Range (0, posLenght);
//					} else {
//						posList.Add (randomPos);
//					}
//				}

				randomCir = Random.Range (0, circlesLenght);

				GameObject randomCircle = Instantiate (circles [randomCir], pos [randomPos].position, Quaternion.identity);
				circlesList.Add (randomCircle);
				if(lifeTime>=20 && lifeTime<=100){
					randomCircle.GetComponent<Rigidbody2D> ().gravityScale = 0.2f;
				}if(lifeTime>=100 && lifeTime<=600){
					randomCircle.GetComponent<Rigidbody2D> ().gravityScale = 0.3f;
				}
				else if(lifeTime>=600 && lifeTime<=1000){
					randomCircle.GetComponent<Rigidbody2D> ().gravityScale = 0.5f;
				}

			} else
				isNormalSpeed = true;
		}
	}
	public void UpdateScoreCount(){
		lifeTimeScore +=1;
		ui.scoreText.text = ": " +lifeTimeScore;


	}
	public void UpdateCircleCount(){
		data.circleCount +=1;
		ui.circleText.text = ": " +data.circleCount;


	}


	public void MinesScoreCount(){
		
		if (lifeTimeScore <=10 && lifeTimeScore >0) {
			lifeTimeScore -= 1;
			ui.scoreText.text = ": " + lifeTimeScore;

		}else if (lifeTimeScore > 10) {
			lifeTimeScore -= 10;
			ui.scoreText.text = ": " + lifeTimeScore;

		}
		if (data.score > 10) {
			data.score -= 10;
		}else if (data.score <= 10 && data.score > 0) {
			data.score -= 1;
		}


	}

	public void UpdateStarCount(){

		data.starCount += 1;
		lifeTimeScore += 2;
		ui.scoreText.text = ": " + lifeTimeScore;
		ui.starText.text = " " + data.starCount;

	}

	public void UpdateMagnetCount(){

		data.magnetCount += 1;
		lifeTimeScore += 2;
		ui.scoreText.text = ": " + lifeTimeScore;
		ui.magnetText.text = " " + data.magnetCount;

	}

	public void UpdateFastSpeedCount(){

		data.speedFastCount += 1;
		lifeTimeScore += 2;
		ui.scoreText.text = ": " + lifeTimeScore;
		ui.fastSpeedText.text = " " + data.speedFastCount;

	}

	public void ActiveGameOverPanel(){
		TimerOn = false;
		canTouch = false;
		isMagnet = false;
		isGameOver = true;
		data.score += lifeTimeScore;
		DeactiveHUD ();
		data.timer = (int)lifeTime;
		ui.gameOverPanel.SetActive (true);
		ui.finalTimerText.text = ": " + timerString;
		ui.finalScoreText.text = "" + data.score;
		lifeTimeScore = 0;
		SaveData ();
		AudioCtrl.instance.bgMusic.SetActive (false);
		if(AudioCtrl.soundOn){
		AudioCtrl.instance.GameOver (gameObject.transform.position);
		}

	}
//
//	void ActiveLevelCompletePanel(){
//		TimerOn = false;
//		canTouch = false;
//		isMagnet = false;
//	}



	void DeactiveHUD(){
		ui.scoreText.text = "";
		ui.timerText.text = "";
		scoreTxt.SetActive (false);
		timerTxt.SetActive (false);
	}
	void UpdateTimerNormal(){

		lifeTime += Time.deltaTime;
		int second = (int)(lifeTime % 60);
		int minutes = (int)(lifeTime / 60)%60;
		int hours = (int)(lifeTime / 3600)%24;
	    timerString = string.Format("{0:0}:{1:00}:{2:00}",hours,minutes,second);
		ui.timerText.text = ": " + timerString;

	}
	void UpdateTimerFast(){
		lifeTime += Time.deltaTime*5;
		int second = (int)(lifeTime % 60);
		int minutes = (int)(lifeTime / 60)%60;
		int hours = (int)(lifeTime / 3600)%24;
		timerString = string.Format("{0:0}:{1:00}:{2:00}",hours,minutes,second);
		ui.timerText.text = ": " + timerString;
//			int i = (int)lifeTime++;
//			ui.timerText.text = ": " + i;

		}

	void DisappearMagnet(){
		magnetCirSign.SetActive (false);
		isMagnet = false;
	}
	void DisappearSavingCircleSign(){
		magicSaveSign.SetActive (false);
		isSavingCir = false;
	}

	public void InstantiateMagnet(){
		if (canCreateMagnet && !isGameOver) {
			Instantiate (magnetCircle, pos [randomPos].position, Quaternion.identity);
			canCreateMagnet = false;
		}
	}
	public void InstantiateSavingRing(){
		if (canCreateSavingCir && !isGameOver) {
			Instantiate (magicSavingCircle, pos [randomPos].position, Quaternion.identity);
				canCreateSavingCir = false;
			}
	}
	public void InstantiateFastSpeedCircle(){
		if (canCreateFastSpeedCircle && !isGameOver) {
			Instantiate (fastSpeedCircle, pos [randomPos].position, Quaternion.identity);
				canCreateFastSpeedCircle = false;
			}
	}
	public void InstantiateStarCircle(){
		if (canCreateStarCircle && !isGameOver) {
			Instantiate (starCircle, pos [randomPos].position, Quaternion.identity);
				canCreateStarCircle = false;
			}
	}



}


	

