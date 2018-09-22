using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/// <summary>
/// Group All User interface elements together for GameCtr.
/// </summary>
[Serializable]
public class UI {
	[Header("Text")]
	public Text scoreText;
	public Text circleText;
	public Text finalScoreText;
	public Text bestScoreText;
	public Text timerText;
	public Text finalTimerText;
	public Text bestTimerText;
	public Text starText;
	public Text magnetText;
	public Text fastSpeedText;

	public GameObject gameOverPanel;
	public GameObject firstHelpPanel;
	public GameObject pausePanel;

}
