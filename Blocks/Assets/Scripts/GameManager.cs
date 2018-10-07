using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int currentScore;
	public int highScore;

	private int currentLevel=1;
	public int unlockedLevel;

	public float startTime;
	public string currentTime;

	public GUISkin skin;
	public Rect timerRect;

	public Color warningColorTime;
	public Color defaultColorTime;

	public int tokenCount;
	private int totalTokenCount;

	public GameObject tokenParent;
	private bool showWinScreen = false;

	public int winScreenWidth;
	public int winScreenHeight;

	private bool completed= false; 

	void Update()
	{
		if(!completed)
		{
			startTime -= Time.deltaTime;
			currentTime = string.Format ("{0:0.0}", startTime);

			if (startTime <=0) 
			{
				startTime = 0;
				Destroy (gameObject);
				SceneManager.LoadScene ("World_Select");
			}
		}
	}

	void Start()
	{
		totalTokenCount = tokenParent.transform.childCount;

		if (PlayerPrefs.GetInt ("Level Completed") > 0) {
			currentLevel = PlayerPrefs.GetInt ("Level Completed");
		} else {
			currentLevel = 1;
		}

	}

	public void CompleteLevel()
	{
		showWinScreen = true;
		completed = true;
	}

	void LoadNextLevel()
	{
		Time.timeScale = 1f;
		if (currentLevel <= 3) {

			currentLevel += 1;
			SaveGame ();
			SceneManager.LoadScene (currentLevel);

		} else {
			print ("You Win Boi");
		}
	}

	void SaveGame()
	{
		PlayerPrefs.SetInt ("Level Completed" , currentLevel);
		PlayerPrefs.SetInt ("Level " + currentLevel.ToString() + " Score", currentScore);
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if (startTime < 5f) {
			skin.GetStyle ("Timer").normal.textColor = warningColorTime;
		} else {
			skin.GetStyle ("Timer").normal.textColor = defaultColorTime;
		}

		GUI.Label (timerRect, currentTime , skin.GetStyle("Timer"));
		GUI.Label (new Rect (30, 80, 200, 120), tokenCount.ToString() + "/" + totalTokenCount.ToString ());

		if (showWinScreen) 
		{
			Rect winScreen = new Rect ((Screen.width-Screen.width*.5f) / 2, (Screen.height-Screen.height*.5f) / 2,Screen.width*0.5f ,Screen.height*0.5f);
			GUI.Box (winScreen , "");

			currentScore = tokenCount * (int)startTime;

			if(GUI.Button (new Rect(winScreen.x + winScreenWidth + 115 , winScreen.y + winScreenHeight - 60 , 150 , 40) , "CONTINUE"))
			{
				LoadNextLevel();
			}

			if(GUI.Button (new Rect(winScreen.x +75 , winScreen.y + winScreenHeight - 60 , 150 , 40) , "QUIT"))
			{
				SceneManager.LoadScene ("Main_Menu");
				Time.timeScale = 1f;
			}

			GUI.Label (new Rect(winScreen.x +200 , winScreen.y + 20 , 300 , 50),"Completed Level : "+currentLevel.ToString());
			GUI.Label (new Rect(winScreen.x +200 , winScreen.y + 50 , 300 , 50),"Your score of this round is : "+currentScore.ToString());
		}
	}


}
