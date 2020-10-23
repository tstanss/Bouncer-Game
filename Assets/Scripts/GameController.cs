using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject gamePanel;
	public GameObject scoreAlert;

	public Text scoreText;
	public Text bestScore;
	public Text currentScore;
	public Text howToPlay;

	int score = 0;

	//"HowToPlay" method is called after 7 deconds and GameObject "Canvas" is set inactive.
	void Start () {
		Invoke ("HowToPlay", 7);
	}
	
	public void GameOver(){
		Invoke ("ShowPanel", 1);
	}

	private void HowToPlay(){
		howToPlay.gameObject.SetActive (false);
	}

	public void ShowPanel(){
		scoreText.gameObject.SetActive (false); // Score text is disabled and "Game Over" panel is shown.
		// GetInt returns value "Best" if it exixts. Else it will return 0. 
		if (score > PlayerPrefs.GetInt("Best", 0))
		{
			PlayerPrefs.SetInt ("Best", score); // Sets the value of the preference by "Best". Set score to "Best".
			scoreAlert.SetActive (true);
		}	
			
		bestScore.text = "Best Score: " + PlayerPrefs.GetInt ("Best", 0).ToString();
		currentScore.text = "Score: " + score.ToString ();
			
		gamePanel.SetActive (true);
	}

	public void RestartGame(){
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void ScoreCount(){
		score++;
		scoreText.text = score.ToString ();
	}

}
